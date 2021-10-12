using QuanLyKho.ViewModel;
using QuanLyPhongKham.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyPhongKham.ViewModel
{
    public class MedicineViewModel : BaseViewModel
    {
        private ObservableCollection<THUOC> _ListMedicine;
        public ObservableCollection<THUOC> ListMedicine { get => _ListMedicine; set { _ListMedicine = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUKHAM> _ListNote;
        public ObservableCollection<PHIEUKHAM> ListNote { get => _ListNote; set { _ListNote = value; OnPropertyChanged(); } }

        private ObservableCollection<DONTHUOC> _ListNoteMedicine;
        public ObservableCollection<DONTHUOC> ListNoteMedicine { get => _ListNoteMedicine; set { _ListNoteMedicine = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUNHAP> _ListImportMedicine;
        public ObservableCollection<PHIEUNHAP> ListImportMedicine { get => _ListImportMedicine; set { _ListImportMedicine = value; OnPropertyChanged(); } }

        private ObservableCollection<CHITIETPHIEUNHAP> _ListDetailMedicine;
        public ObservableCollection<CHITIETPHIEUNHAP> ListDetailMedicine { get => _ListDetailMedicine; set { _ListDetailMedicine = value; OnPropertyChanged(); } }

        private string _TotalPrice;
        public string TotalPrice { get => _TotalPrice; set { _TotalPrice = value; OnPropertyChanged(); } }

        private PHIEUKHAM _SelectedItem_PHIEUKHAM;
        public PHIEUKHAM SelectedItem_PHIEUKHAM
        {
            get => _SelectedItem_PHIEUKHAM;
            set
            {
                _SelectedItem_PHIEUKHAM = value;
                OnPropertyChanged();
                if (SelectedItem_PHIEUKHAM != null)
                {
                    LoadListNoteMedicine(SelectedItem_PHIEUKHAM);
                    TotalPrice = DataProvider.Instant.DB.HOADONs.Where(x => x.MaPhieuKham == SelectedItem_PHIEUKHAM.MaPhieuKham).SingleOrDefault().TienThuoc.ToString();
                }
            }
        }
        private PHIEUNHAP _SelectedItem_PHIEUNHAP;
        public PHIEUNHAP SelectedItem_PHIEUNHAP
        {
            get => _SelectedItem_PHIEUNHAP;
            set
            {
                _SelectedItem_PHIEUNHAP = value;
                OnPropertyChanged();
                if (SelectedItem_PHIEUNHAP != null)
                    LoadListDetailMedicine(SelectedItem_PHIEUNHAP);
            }
        }
        public ICommand LoadedCommand { get; set; }
        public ICommand ListStockCommand { get; set; }
        public ICommand ExportBillCommand { get; set; }
        public ICommand GetMedicineCommand { get; set; }
        public ICommand CancelMedicineCommand { get; set; }
        public ICommand Note_ListViewLoadedCommand { get; set; }
        public ICommand Medicine_ListViewCommand { get; set; }
        public ICommand AddMedicineCommand { get; set; }
        public MedicineViewModel()
        {
            AddMedicineCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                AddMedicineWindow wd = new AddMedicineWindow();
                AddMedicineViewModel vm = new AddMedicineViewModel();
                wd.DataContext = vm;
                wd.ShowDialog();
                ListImportMedicine = new ObservableCollection<PHIEUNHAP>(DataProvider.Instant.DB.PHIEUNHAPs);
                LoadListStock();
                p.ShowDialog();
            });

            LoadListStock();
            LoadListNote();
            ListImportMedicine = new ObservableCollection<PHIEUNHAP>(DataProvider.Instant.DB.PHIEUNHAPs);

            Note_ListViewLoadedCommand = new RelayCommand<System.Windows.Controls.TextBox>((p) => { return true; }, (p) =>
            {
                string NoteName = p.Text;
                ListNote = new ObservableCollection<PHIEUKHAM>(DataProvider.Instant.DB.PHIEUKHAMs.Where(x => x.MaPhieuKham.ToString().Contains(NoteName)));
            });

            Medicine_ListViewCommand = new RelayCommand<System.Windows.Controls.TextBox>((p) => { return true; }, (p) =>
            {
                string MedicineName = p.Text;
                ListMedicine = new ObservableCollection<THUOC>(DataProvider.Instant.DB.THUOCs.Where(x => x.DaXoa == false && x.TenThuoc.Contains(MedicineName)));
            });

            GetMedicineCommand = new RelayCommand<object>((p) => {
                if (SelectedItem_PHIEUKHAM == null)
                    return false;
                if (TotalPrice != "0") return false;
                return ListNoteMedicine.Count > 0;
            }, (p) =>
            {
                double? total = 0;
                bool check = true;
                foreach (DONTHUOC medicine in ListNoteMedicine)
                {
                    THUOC temp = DataProvider.Instant.DB.THUOCs.Where(x => x.MaThuoc == medicine.MaThuoc).SingleOrDefault();
                    if ((medicine.SoLuongKe <= temp.SoLuongTon) && (medicine.SoLuongBan != medicine.SoLuongKe)) // Bán được
                    {
                        medicine.SoLuongBan = medicine.SoLuongKe;
                        medicine.DonGia = temp.DonGia;
                        medicine.ThanhTien = temp.DonGia * medicine.SoLuongBan;
                        temp.SoLuongTon -= medicine.SoLuongBan;
                    }
                    if ((medicine.SoLuongKe > temp.SoLuongTon) && (medicine.SoLuongBan != medicine.SoLuongKe)) // Không đủ thuốc để bán
                    {
                        check = false;
                    }
                    total += medicine.ThanhTien;
                }
                DataProvider.Instant.DB.SaveChanges();
                HOADON hd = DataProvider.Instant.DB.HOADONs.Where(x => x.MaPhieuKham == SelectedItem_PHIEUKHAM.MaPhieuKham).SingleOrDefault();
                hd.TienThuoc = total;
                TotalPrice = total.ToString();
                hd.TongTien = hd.TienThuoc + hd.TienDichVu + hd.TienKhamBenh;
                DataProvider.Instant.DB.SaveChanges();

                DateTime t = DateTime.Now;

                foreach (DONTHUOC tmp in ListNoteMedicine)
                {
                    if (tmp.SoLuongBan > 0)
                    {
                        if (DataProvider.Instant.DB.BAOCAOSUDUNGTHUOCs.Where(x => x.Nam == t.Year && x.Thang == t.Month && x.MaThuoc == tmp.MaThuoc).ToList().Count == 0)
                        {
                            // Chưa tồn tại
                            BAOCAOSUDUNGTHUOC rp = new BAOCAOSUDUNGTHUOC() { Nam = t.Year, Thang = t.Month, MaThuoc = tmp.MaThuoc, SoLuong = tmp.SoLuongBan, SoLanDung = 1 };
                            DataProvider.Instant.DB.BAOCAOSUDUNGTHUOCs.Add(rp);
                        }
                        else
                        {
                            //Đã tồn tại
                            BAOCAOSUDUNGTHUOC rpMedicine = DataProvider.Instant.DB.BAOCAOSUDUNGTHUOCs.Where(x => x.Nam == t.Year && x.Thang == t.Month && x.MaThuoc == tmp.MaThuoc).SingleOrDefault();
                            rpMedicine.SoLuong += tmp.SoLuongBan;
                            rpMedicine.SoLanDung += 1;
                        }
                    }
                }
                DataProvider.Instant.DB.SaveChanges();

                BAOCAODOANHTHUTHANG rpM = DataProvider.Instant.DB.BAOCAODOANHTHUTHANGs.Where(x => x.Nam == t.Year && x.Thang == t.Month).SingleOrDefault();
                rpM.DoanhThu += total;
                CHITIETBAOCAODOANHTHUTHANG rpMDetail = DataProvider.Instant.DB.CHITIETBAOCAODOANHTHUTHANGs.Where(x => x.MaBaoCaoDoanhThuThang == rpM.MaBaoCaoDoanhThuThang && x.Ngay == t.Day).SingleOrDefault();
                rpMDetail.DoanhThu += total;

                int lastDay = DateTime.DaysInMonth(t.Year, t.Month);
                for(int i=1; i<=lastDay; i++)
                {
                    CHITIETBAOCAODOANHTHUTHANG dayOfMonth = DataProvider.Instant.DB.CHITIETBAOCAODOANHTHUTHANGs.Where(x => x.MaBaoCaoDoanhThuThang == rpM.MaBaoCaoDoanhThuThang && x.Ngay == i).SingleOrDefault();
                    dayOfMonth.TyLe = dayOfMonth.DoanhThu * 1.0 / rpM.DoanhThu;
                }
                DataProvider.Instant.DB.SaveChanges();

                LoadListNoteMedicine(SelectedItem_PHIEUKHAM);
                LoadListStock();
                if (check)
                {
                    System.Windows.MessageBox.Show("Lấy thuốc thành công!");
                }
                else
                {
                    System.Windows.MessageBox.Show("Không đủ thuốc cung cấp!");
                }
            });

            CancelMedicineCommand = new RelayCommand<object>((p) => {
                if (SelectedItem_PHIEUKHAM == null)
                    return false;
                if (TotalPrice == "0") return false;
                return ListNoteMedicine.Count != 0;
            }, (p) =>
            {
                DateTime t = DateTime.Now;

                foreach (DONTHUOC tmp in ListNoteMedicine)
                {
                    if (tmp.SoLuongBan > 0)
                    {
                        BAOCAOSUDUNGTHUOC rp = DataProvider.Instant.DB.BAOCAOSUDUNGTHUOCs.Where(x => x.Nam == t.Year && x.Thang == t.Month && x.MaThuoc == tmp.MaThuoc).SingleOrDefault();
                        rp.SoLuong -= tmp.SoLuongBan;
                        rp.SoLanDung -= 1;
                    }
                }
                DataProvider.Instant.DB.SaveChanges();

                foreach (DONTHUOC medicine in ListNoteMedicine)
                {
                    THUOC temp = DataProvider.Instant.DB.THUOCs.Where(x => x.MaThuoc == medicine.MaThuoc).SingleOrDefault();
                    temp.SoLuongTon += medicine.SoLuongBan;
                    medicine.SoLuongBan = 0;
                    medicine.DonGia = 0;
                    medicine.ThanhTien = 0;
                }
                DataProvider.Instant.DB.SaveChanges();

                HOADON hd = DataProvider.Instant.DB.HOADONs.Where(x => x.MaPhieuKham == SelectedItem_PHIEUKHAM.MaPhieuKham).SingleOrDefault();
                hd.TienThuoc = 0;
                TotalPrice = "0";
                hd.TongTien = hd.TienThuoc + hd.TienDichVu + hd.TienKhamBenh;
                DataProvider.Instant.DB.SaveChanges();

                BAOCAODOANHTHUTHANG rpM = DataProvider.Instant.DB.BAOCAODOANHTHUTHANGs.Where(x => x.Nam == t.Year && x.Thang == t.Month).SingleOrDefault();
                rpM.DoanhThu -= hd.TienThuoc;
                CHITIETBAOCAODOANHTHUTHANG rpMDetail = DataProvider.Instant.DB.CHITIETBAOCAODOANHTHUTHANGs.Where(x => x.MaBaoCaoDoanhThuThang == rpM.MaBaoCaoDoanhThuThang && x.Ngay == t.Day).SingleOrDefault();
                rpMDetail.DoanhThu -= hd.TienThuoc;

                int lastDay = DateTime.DaysInMonth(t.Year, t.Month);
                for (int i = 1; i <= lastDay; i++)
                {
                    CHITIETBAOCAODOANHTHUTHANG dayOfMonth = DataProvider.Instant.DB.CHITIETBAOCAODOANHTHUTHANGs.Where(x => x.MaBaoCaoDoanhThuThang == rpM.MaBaoCaoDoanhThuThang && x.Ngay == i).SingleOrDefault();
                    dayOfMonth.TyLe = dayOfMonth.DoanhThu * 1.0 / rpM.DoanhThu;
                }
                DataProvider.Instant.DB.SaveChanges();

                LoadListNoteMedicine(SelectedItem_PHIEUKHAM);
                LoadListStock();
                System.Windows.MessageBox.Show("Bỏ thành công!");
            });

            ExportBillCommand = new RelayCommand<PHIEUKHAM>((p) => {
                HOADON hd = DataProvider.Instant.DB.HOADONs.Where(x => x.MaPhieuKham == p.MaPhieuKham).SingleOrDefault();
                if (hd.TienThuoc <= 0) return false;
                return true;
            }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Xác nhận xuất hóa đơn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    p.DaBanThuoc = true;
                    DataProvider.Instant.DB.SaveChanges();
                    LoadListNote();
                    System.Windows.MessageBox.Show("Xuất hóa đơn cho bệnh nhân " + p.BENHNHAN.TenBenhNhan + " thành công!");
                }
            });
        }
        void LoadListStock()
        {
            ListMedicine = new ObservableCollection<THUOC>(DataProvider.Instant.DB.THUOCs.Where(x => x.DaXoa == false));
        }
        void LoadListNoteMedicine(PHIEUKHAM p)
        {
            ListNoteMedicine = new ObservableCollection<DONTHUOC>(DataProvider.Instant.DB.DONTHUOCs.Where(x => x.MaPhieuKham == p.MaPhieuKham));
        }
        void LoadListDetailMedicine(PHIEUNHAP p)
        {
            ListDetailMedicine = new ObservableCollection<CHITIETPHIEUNHAP>(DataProvider.Instant.DB.CHITIETPHIEUNHAPs.Where(x => x.MaPhieuNhap == p.MaPhieuNhap));
        }

        void LoadListNote()
        {
            ListNote = new ObservableCollection<PHIEUKHAM>(DataProvider.Instant.DB.PHIEUKHAMs.Where(x => x.DaBanThuoc == false));
        }
    }
}
