using QuanLyKho.ViewModel;
using QuanLyPhongKham.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyPhongKham.ViewModel
{
    class AddMedicineViewModel : BaseViewModel
    {
        private string _Count;
        public string Count { get => _Count; set { _Count = value; OnPropertyChanged(); try { CountINT = int.Parse(Count); } catch { CountINT = 0; } } }

        private int _CountINT;
        public int CountINT { get => _CountINT; set { _CountINT = value; OnPropertyChanged(); } }

        private string _Price;
        public string Price { get => _Price; set { _Price = value; OnPropertyChanged(); try { PriceINT = int.Parse(Price); } catch { PriceINT = 0; } } }

        private int _PriceINT;
        public int PriceINT { get => _PriceINT; set { _PriceINT = value; OnPropertyChanged(); } }

        private THUOC _SelectedMedicine;
        public THUOC SelectedMedicine { get => _SelectedMedicine; set { _SelectedMedicine = value; OnPropertyChanged(); } }

        private String _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        private ObservableCollection<THUOC> _ListMedicine;
        public ObservableCollection<THUOC> ListMedicine { get => _ListMedicine; set { _ListMedicine = value; OnPropertyChanged(); } }

        private ObservableCollection<CHITIETPHIEUNHAP> _ListAddMedicine;
        public ObservableCollection<CHITIETPHIEUNHAP> ListAddMedicine { get => _ListAddMedicine; set { _ListAddMedicine = value; OnPropertyChanged(); } }
        public ICommand ExitCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public AddMedicineViewModel()
        {
            LoadListMedicine();
            ListAddMedicine = new ObservableCollection<CHITIETPHIEUNHAP>();

            ExitCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedMedicine == null) return false;
                if ((CountINT <= 0) || (PriceINT <= 0)) return false;
                return true;
            }, (p) =>
            {
                bool check = true; //không trùng
                foreach (CHITIETPHIEUNHAP detail in ListAddMedicine)
                {
                    if (detail.THUOC == SelectedMedicine)
                    {
                        check = false;//trùng
                        CHITIETPHIEUNHAP temp = new CHITIETPHIEUNHAP();
                        temp.THUOC = SelectedMedicine;
                        temp.SoLuong = detail.SoLuong + CountINT;
                        temp.GiaNhap = PriceINT;
                        temp.ThanhTien = temp.SoLuong * temp.GiaNhap;
                        ListAddMedicine.Remove(detail);
                        ListAddMedicine.Add(temp);
                        break;
                    }
                }
                if (check)
                {
                    CHITIETPHIEUNHAP temp = new CHITIETPHIEUNHAP();
                    temp.THUOC = SelectedMedicine;
                    temp.SoLuong = CountINT;
                    temp.GiaNhap = PriceINT;
                    temp.ThanhTien = temp.SoLuong * temp.GiaNhap;
                    ListAddMedicine.Add(temp);
                }
            });
            SaveCommand = new RelayCommand<Window>((p) =>
            {
                if (ListAddMedicine.Count <= 0) return false;
                return true;
            }, (p) =>
            {
                DateTime t = DateTime.Now;
                DataProvider.Instant.DB.PHIEUNHAPs.Add(new PHIEUNHAP() { NgayNhap = t, GhiChu = Note, TongTien = 0 });
                DataProvider.Instant.DB.SaveChanges();
                int id = DataProvider.Instant.DB.PHIEUNHAPs.DefaultIfEmpty().Max(r => r == null ? 0 : r.MaPhieuNhap);
                PHIEUNHAP pn = DataProvider.Instant.DB.PHIEUNHAPs.Where(x => x.MaPhieuNhap == id).FirstOrDefault();
                foreach (CHITIETPHIEUNHAP detail in ListAddMedicine)
                {
                    detail.MaPhieuNhap = id;
                    pn.TongTien += detail.ThanhTien;
                    DataProvider.Instant.DB.CHITIETPHIEUNHAPs.Add(detail);
                    THUOC medicine = DataProvider.Instant.DB.THUOCs.Where(x => x.MaThuoc == detail.MaThuoc).FirstOrDefault();
                    medicine.SoLuongTon += detail.SoLuong;
                }
                DataProvider.Instant.DB.SaveChanges();
                System.Windows.Forms.MessageBox.Show("Tổng giá trị phiếu nhập là " + pn.TongTien);
                p.Close();
            });
            DeleteCommand = new RelayCommand<CHITIETPHIEUNHAP>((p) =>
            {
                return true;
            }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Xác nhận xoá thuốc?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {

                    ListAddMedicine.Remove(p);
                }
            });

        }
        void LoadListMedicine()
        {
            ListMedicine = new ObservableCollection<THUOC>(DataProvider.Instant.DB.THUOCs.Where(x => x.DaXoa == false));
        }
    }
}
