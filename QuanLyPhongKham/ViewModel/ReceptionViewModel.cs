using QuanLyKho.ViewModel;
using QuanLyPhongKham.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyPhongKham.ViewModel
{
    class ReceptionViewModel : BaseViewModel
    {
        #region ThuocTinhTab1
        private ObservableCollection<BENHNHAN> _ListPatient;
        public ObservableCollection<BENHNHAN> ListPatient { get => _ListPatient; set { _ListPatient = value; OnPropertyChanged(); } }
        private BENHNHAN _SelectedPatient;
        public BENHNHAN SelectedPatient
        {
            get => _SelectedPatient;
            set
            {
                _SelectedPatient = value;
                OnPropertyChanged();
                if (SelectedPatient != null)
                {
                    NameOfPatientUpdate = SelectedPatient.TenBenhNhan;
                    BirthdayOfPatientUpdate = SelectedPatient.NgaySinh;
                    GenderOfPatientUpdate = SelectedPatient.GioiTinh;
                    PhoneOfPatientUpdate = SelectedPatient.SoDienThoai;
                    AddressOfPatientUpdate = SelectedPatient.DiaChi;
                }
                else
                {
                    NameOfPatientUpdate = GenderOfPatientUpdate = PhoneOfPatientUpdate = AddressOfPatientUpdate = null;
                    BirthdayOfPatientUpdate = null;
                }
            }
        }

        private int _CountPatient;
        public int CountPatient { get => _CountPatient; set { _CountPatient = value; OnPropertyChanged(); } }
        private string _NameOfPatientUpdate;
        public string NameOfPatientUpdate { get => _NameOfPatientUpdate; set { _NameOfPatientUpdate = value; OnPropertyChanged(); } }
        private Nullable<DateTime> _BirthdayOfPatientUpdate;
        public Nullable<DateTime> BirthdayOfPatientUpdate { get => _BirthdayOfPatientUpdate; set { _BirthdayOfPatientUpdate = value; OnPropertyChanged(); } }
        private string _GenderOfPatientUpdate;
        public String GenderOfPatientUpdate { get => _GenderOfPatientUpdate; set { _GenderOfPatientUpdate = value; OnPropertyChanged(); } }
        private string _PhoneOfPatientUpdate;
        public String PhoneOfPatientUpdate { get => _PhoneOfPatientUpdate; set { _PhoneOfPatientUpdate = value; OnPropertyChanged(); } }
        private string _AddressOfPatientUpdate;
        public String AddressOfPatientUpdate { get => _AddressOfPatientUpdate; set { _AddressOfPatientUpdate = value; OnPropertyChanged(); } }


        #endregion
        #region ThuocTinhTab2
        private ObservableCollection<HANGSUDUNGDICHVU> _ListServiceQueue;
        public ObservableCollection<HANGSUDUNGDICHVU> ListServiceQueue { get => _ListServiceQueue; set { _ListServiceQueue = value; OnPropertyChanged(); } }
        private ObservableCollection<HANGCHOKHAMBENH> _ListCuringQueue;
        public ObservableCollection<HANGCHOKHAMBENH> ListCuringQueue { get => _ListCuringQueue; set { _ListCuringQueue = value; OnPropertyChanged(); } }
        private ObservableCollection<HANGCHOTHANHTOAN> _ListPayQueue;
        public ObservableCollection<HANGCHOTHANHTOAN> ListPayQueue { get => _ListPayQueue; set { _ListPayQueue = value; OnPropertyChanged(); } }
        #endregion
        #region ThuocTinhTab3
        private ObservableCollection<PHIEUKHAM> _ListMedicalRecord;
        public ObservableCollection<PHIEUKHAM> ListMedicalRecord { get => _ListMedicalRecord; set { _ListMedicalRecord = value; OnPropertyChanged(); } }
        private PHIEUKHAM _SelectedMedicalRecord;
        public PHIEUKHAM SelectedMedicalRecord
        {
            get => _SelectedMedicalRecord;
            set
            {
                _SelectedMedicalRecord = value;
                OnPropertyChanged();
                if (SelectedMedicalRecord != null)
                {
                    ListServiceDetails = new ObservableCollection<CHITIETDICHVU>(DataProvider.Instant.DB.CHITIETDICHVUs.Where(x => x.MaPhieuKham == SelectedMedicalRecord.MaPhieuKham));
                    ListPrescription = new ObservableCollection<DONTHUOC>(DataProvider.Instant.DB.DONTHUOCs.Where(x => x.MaPhieuKham == SelectedMedicalRecord.MaPhieuKham));
                }
                else ListServiceDetails = null;
            }
        }
        private ObservableCollection<CHITIETDICHVU> _ListServiceDetails;
        public ObservableCollection<CHITIETDICHVU> ListServiceDetails { get => _ListServiceDetails; set { _ListServiceDetails = value; OnPropertyChanged(); } }
        private ObservableCollection<DONTHUOC> _ListPrescription;
        public ObservableCollection<DONTHUOC> ListPrescription { get => _ListPrescription; set { _ListPrescription = value; OnPropertyChanged(); } }
        #endregion
        #region ThuocTinhTab4
        private ObservableCollection<PHIEUKHAM> _ListReexamination;
        public ObservableCollection<PHIEUKHAM> ListReexamination { get => _ListReexamination; set { _ListReexamination = value; OnPropertyChanged(); } }
        private PHIEUKHAM _SelectedReexamination;
        public PHIEUKHAM SelectedReexamination { get => _SelectedReexamination; set { _SelectedReexamination = value; OnPropertyChanged(); if (SelectedReexamination != null) { ReexaminationDateUpdate = SelectedReexamination.NgayTaiKham.Value; } else ReexaminationDateUpdate = null; } }
        private Nullable<DateTime> _ReexaminationDateUpdate;
        public Nullable<DateTime> ReexaminationDateUpdate { get => _ReexaminationDateUpdate; set { _ReexaminationDateUpdate = value; OnPropertyChanged(); } }
        #endregion



        #region CommandTab1
        public ICommand UpdatePatientButtonCommand { get; set; }
        public ICommand LoadedReceptionPatient_ListViewCommand { get; set; }
        public ICommand AddNewPatientButtonCommand { get; set; }

        #endregion
        #region CommandTab2
        public ICommand PayCommand { get; set; }
        public ICommand ExpandButtonOfServiceQueueCommand { get; set; }
        public ICommand ExpandButtonOfCuringQueueCommand { get; set; }
        public ICommand ExpandButtonOfPayQueueCommand { get; set; }
        #endregion
        #region CommandTab3
        public ICommand AddNewMedicalRecordFromReexaminationButtonCommand { get; set; }
        #endregion
        #region CommandTab4
        public ICommand Reexamination_ListViewLoadedCommand { get; set; }
        public ICommand DisplayListReexaminationTodayCommand { get; set; }
        public ICommand UpdateReexaminationDateCommand { get; set; }
        #endregion

        public ICommand LoadedCommand { get; set; }


        public ReceptionViewModel()
        {
            ListPatient = new ObservableCollection<BENHNHAN>(DataProvider.Instant.DB.BENHNHANs.Where(x => (from p in DataProvider.Instant.DB.PHIEUKHAMs
                                                                                                           where p.NgayKham == DateTime.Today
                                                                                                           select p.MaBenhNhan).Distinct().Contains(x.MaBenhNhan)));
            ListServiceQueue = new ObservableCollection<HANGSUDUNGDICHVU>(DataProvider.Instant.DB.HANGSUDUNGDICHVUs);
            ListCuringQueue = new ObservableCollection<HANGCHOKHAMBENH>(DataProvider.Instant.DB.HANGCHOKHAMBENHs);
            ListPayQueue = new ObservableCollection<HANGCHOTHANHTOAN>(DataProvider.Instant.DB.HANGCHOTHANHTOANs);
            ListMedicalRecord = new ObservableCollection<PHIEUKHAM>(DataProvider.Instant.DB.PHIEUKHAMs);

            LoadedCommand = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text += "Nguyễn Đình Long";
                (p.FindName("CountReception_TextBlock") as TextBlock).Text = ListPatient.Count().ToString() + "/" + DataProvider.Instant.DB.THAMSOes.FirstOrDefault().SoLuongBenhNhanToiDaTrongNgay.ToString();
            });

            #region CommandTab1
            LoadedReceptionPatient_ListViewCommand = new RelayCommand<TabItem>((p) => { return true; }, (p) =>
            {
                string Name = (p.FindName("PatientSearch_Textbox") as System.Windows.Controls.TextBox).Text;
                ListPatient = new ObservableCollection<BENHNHAN>(DataProvider.Instant.DB.BENHNHANs.Where(x => (from p2 in DataProvider.Instant.DB.PHIEUKHAMs
                                                                                                               where p2.NgayKham == DateTime.Today
                                                                                                               select p2.MaBenhNhan).Distinct().Contains(x.MaBenhNhan) && x.TenBenhNhan.Contains(Name)));

            });
            AddNewPatientButtonCommand = new RelayCommand<TabItem>((p) =>
            {
                if (p == null)
                    return false;
                else
                {
                    if (string.IsNullOrEmpty((p.FindName("NameOfNewPatient_TextBox") as System.Windows.Controls.TextBox).Text) ||
                        string.IsNullOrEmpty((p.FindName("BirthdayOfNewPatient_DatePicker") as System.Windows.Controls.DatePicker).Text) ||
                        string.IsNullOrEmpty((p.FindName("GenderOfNewPatient_ComboBox") as System.Windows.Controls.ComboBox).Text) ||
                        string.IsNullOrEmpty((p.FindName("PhoneOfNewPatient_TextBox") as System.Windows.Controls.TextBox).Text) ||
                        string.IsNullOrEmpty((p.FindName("AddressOfNewPatient_TextBox") as System.Windows.Controls.TextBox).Text))
                        return false;
                    else if (ListPatient.Count >= DataProvider.Instant.DB.THAMSOes.FirstOrDefault().SoLuongBenhNhanToiDaTrongNgay)
                        return false;
                    return true;
                }
            }, (p) =>
            {
                (p.FindName("PatientSearch_Textbox") as System.Windows.Controls.TextBox).Text = "";
                string Name = (p.FindName("NameOfNewPatient_TextBox") as System.Windows.Controls.TextBox).Text;
                string Addresss = (p.FindName("AddressOfNewPatient_TextBox") as System.Windows.Controls.TextBox).Text;
                string Phone = (p.FindName("PhoneOfNewPatient_TextBox") as System.Windows.Controls.TextBox).Text;
                string Gender = (p.FindName("GenderOfNewPatient_ComboBox") as System.Windows.Controls.ComboBox).Text;
                DateTime Birthday = (p.FindName("BirthdayOfNewPatient_DatePicker") as DatePicker).SelectedDate.Value;
                var NewPatient = new BENHNHAN() { TenBenhNhan = Name, DiaChi = Addresss, SoDienThoai = Phone, GioiTinh = Gender, NgaySinh = Birthday };
                DataProvider.Instant.DB.BENHNHANs.Add(NewPatient);
                DataProvider.Instant.DB.SaveChanges();
                //Them PHIEUKHAM moi
                var BottomPatient = (from b in DataProvider.Instant.DB.BENHNHANs
                                     orderby b.MaBenhNhan descending
                                     select b).FirstOrDefault();
                DataProvider.Instant.DB.PHIEUKHAMs.Add(new PHIEUKHAM() { MaBenhNhan = BottomPatient.MaBenhNhan, MaLoaiBenh = 1, NgayKham = DateTime.Today, DaBanThuoc = false });
                DataProvider.Instant.DB.SaveChanges();
                ListPatient.Add(NewPatient);
                //Update HANGCHOKHAMBENH
                var BottomMedicalRecord = (from pk in DataProvider.Instant.DB.PHIEUKHAMs
                                           orderby pk.MaPhieuKham descending
                                           select pk).FirstOrDefault();
                var CuringQueue = new HANGCHOKHAMBENH() { MaPhieuKham = BottomMedicalRecord.MaPhieuKham };
                DataProvider.Instant.DB.HANGCHOKHAMBENHs.Add(CuringQueue);
                DataProvider.Instant.DB.SaveChanges();
                ListCuringQueue.Add(CuringQueue);
                //
                //Tao HOADON
                THAMSO thamSo = DataProvider.Instant.DB.THAMSOes.Take(1).FirstOrDefault();
                HOADON bill = new HOADON() { MaPhieuKham = BottomMedicalRecord.MaPhieuKham, TienKhamBenh = thamSo.TienKhamBenh, TienDichVu = 0, TienThuoc = 0, TongTien = thamSo.TienKhamBenh };
                DataProvider.Instant.DB.HOADONs.Add(bill);
                DataProvider.Instant.DB.SaveChanges();
                //Lap BAOCAODOANHTHU
                int year = DateTime.Today.Year;
                int month = DateTime.Today.Month;
                int day = DateTime.Today.Day;
                if (CheckIfProceedsReportExist(DateTime.Today) == false)
                {
                    CreateReportWholeYear(month, year);
                }
                var report = DataProvider.Instant.DB.BAOCAODOANHTHUTHANGs.Where(x => x.Nam == year && x.Thang == month).FirstOrDefault();
                report.DoanhThu += thamSo.TienKhamBenh;
                DataProvider.Instant.DB.SaveChanges();
                var reportDetails = DataProvider.Instant.DB.CHITIETBAOCAODOANHTHUTHANGs.Where(x => x.MaBaoCaoDoanhThuThang == report.MaBaoCaoDoanhThuThang);
                foreach (CHITIETBAOCAODOANHTHUTHANG item in reportDetails)
                {
                    if (item.Ngay == day)
                    {
                        item.DoanhThu += thamSo.TienKhamBenh;
                        item.SoBenhNhan += 1;
                    }
                    item.TyLe = item.DoanhThu / report.DoanhThu;
                }
                DataProvider.Instant.DB.SaveChanges();
                (p.FindName("NameOfNewPatient_TextBox") as System.Windows.Controls.TextBox).Text = "";
                (p.FindName("AddressOfNewPatient_TextBox") as System.Windows.Controls.TextBox).Text = "";
                (p.FindName("PhoneOfNewPatient_TextBox") as System.Windows.Controls.TextBox).Text = "";
                (p.FindName("GenderOfNewPatient_ComboBox") as System.Windows.Controls.ComboBox).Text = "";
                (p.FindName("BirthdayOfNewPatient_DatePicker") as DatePicker).Text = "";
                (p.FindName("CountReception_TextBlock") as TextBlock).Text = ListPatient.Count().ToString() + "/" + DataProvider.Instant.DB.THAMSOes.FirstOrDefault().SoLuongBenhNhanToiDaTrongNgay.ToString();
            });
            UpdatePatientButtonCommand = new RelayCommand<StackPanel>((p) =>
            {
                if (p == null)
                    return false;
                else
                {
                    if (string.IsNullOrEmpty((p.FindName("NameOfPatient_TextBox") as System.Windows.Controls.TextBox).Text) ||
                        string.IsNullOrEmpty((p.FindName("BirthdayOfPatient_DatePicker") as System.Windows.Controls.DatePicker).Text) ||
                        string.IsNullOrEmpty((p.FindName("GenderOfPatient_ComboBox") as System.Windows.Controls.ComboBox).Text) ||
                        string.IsNullOrEmpty((p.FindName("PhoneOfPatient_TextBox") as System.Windows.Controls.TextBox).Text) ||
                        string.IsNullOrEmpty((p.FindName("AddressOfPatient_TextBox") as System.Windows.Controls.TextBox).Text))
                        return false;
                    return true;

                }
            }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Thay đổi thông tin bệnh nhân?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        var BenhNhan = DataProvider.Instant.DB.BENHNHANs.Where(x => x.MaBenhNhan == SelectedPatient.MaBenhNhan).SingleOrDefault();
                        BenhNhan.TenBenhNhan = NameOfPatientUpdate;
                        BenhNhan.NgaySinh = BirthdayOfPatientUpdate;
                        BenhNhan.GioiTinh = GenderOfPatientUpdate;
                        BenhNhan.SoDienThoai = PhoneOfPatientUpdate;
                        BenhNhan.DiaChi = AddressOfPatientUpdate;
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show("Thông tin thay đổi không phù hợp!");
                        return;
                    }
                    DataProvider.Instant.DB.SaveChanges();
                    SelectedPatient.TenBenhNhan = NameOfPatientUpdate;
                    SelectedPatient.GioiTinh = GenderOfPatientUpdate;
                    SelectedPatient.NgaySinh = BirthdayOfPatientUpdate;
                    SelectedPatient.SoDienThoai = PhoneOfPatientUpdate;
                    SelectedPatient.DiaChi = AddressOfPatientUpdate;
                    System.Windows.Forms.MessageBox.Show("Thay đổi ngày tái khám thành công!");
                }
            });
            #endregion
            #region CommandTab2
            PayCommand = new RelayCommand<HANGCHOTHANHTOAN>((p) => { return true; }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Thanh toán cho bệnh nhân?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    //Update HANGCHODICHVU
                    HANGSUDUNGDICHVU SQ = new HANGSUDUNGDICHVU() { MaDichVu = p.MaDichVu, MaPhieuKham = p.MaPhieuKham };
                    DataProvider.Instant.DB.HANGSUDUNGDICHVUs.Add(SQ);
                    DataProvider.Instant.DB.SaveChanges();
                    //update CHITIETDICHVU
                    CHITIETDICHVU SerDetail = new CHITIETDICHVU() { MaDichVu = p.MaDichVu, MaPhieuKham = p.MaPhieuKham, DonGia = p.DICHVU.DonGia };
                    DataProvider.Instant.DB.CHITIETDICHVUs.Add(SerDetail);
                    DataProvider.Instant.DB.SaveChanges();
                    //Update HOADON
                    var bill = DataProvider.Instant.DB.HOADONs.Where(x => x.MaPhieuKham == p.MaPhieuKham).SingleOrDefault();
                    bill.TienDichVu = p.DICHVU.DonGia;
                    bill.TongTien += p.DICHVU.DonGia;
                    DataProvider.Instant.DB.SaveChanges();
                    //Xoa HangChoThanhToan
                    ListServiceQueue.Add(SQ);
                    DataProvider.Instant.DB.HANGCHOTHANHTOANs.Attach(p);
                    DataProvider.Instant.DB.HANGCHOTHANHTOANs.Remove(p);
                    DataProvider.Instant.DB.SaveChanges();
                    ListPayQueue.Remove(p);
                    //Update BAOCAODOANHTHUTHANG
                    int year = DateTime.Today.Year;
                    int month = DateTime.Today.Month;
                    int day = DateTime.Today.Day;
                    var report = DataProvider.Instant.DB.BAOCAODOANHTHUTHANGs.Where(x => x.Nam == year && x.Thang == month).FirstOrDefault();
                    report.DoanhThu += bill.TienDichVu;
                    DataProvider.Instant.DB.SaveChanges();
                    var reportDetails = DataProvider.Instant.DB.CHITIETBAOCAODOANHTHUTHANGs.Where(x => x.MaBaoCaoDoanhThuThang == report.MaBaoCaoDoanhThuThang);
                    foreach (CHITIETBAOCAODOANHTHUTHANG item in reportDetails)
                    {
                        if (item.Ngay == day)
                            item.DoanhThu += bill.TienDichVu;
                        item.TyLe = item.DoanhThu / report.DoanhThu;
                    }

                    DataProvider.Instant.DB.SaveChanges();
                    System.Windows.Forms.MessageBox.Show("Thanh toán dịch vụ thành công!");
                }
            });
            ExpandButtonOfServiceQueueCommand = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                if (p.ColumnDefinitions[0].Width.Value > 250)
                    return;
                p.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                p.ColumnDefinitions[1].Width = new GridLength(250, GridUnitType.Pixel);
                p.ColumnDefinitions[2].Width = new GridLength(250, GridUnitType.Pixel);
            });
            ExpandButtonOfCuringQueueCommand = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                if (p.ColumnDefinitions[2].Width.Value > 250)
                    return;
                p.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                p.ColumnDefinitions[0].Width = new GridLength(250, GridUnitType.Pixel);
                p.ColumnDefinitions[1].Width = new GridLength(250, GridUnitType.Pixel);
            });
            ExpandButtonOfPayQueueCommand = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                if (p.ColumnDefinitions[1].Width.Value > 250)
                    return;
                p.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                p.ColumnDefinitions[0].Width = new GridLength(250, GridUnitType.Pixel);
                p.ColumnDefinitions[2].Width = new GridLength(250, GridUnitType.Pixel);
            });
            #endregion
            #region CommandTab3
            AddNewMedicalRecordFromReexaminationButtonCommand = new RelayCommand<PHIEUKHAM>((p) => { return true; }, (p) =>
            {
                var PhieuKham = new PHIEUKHAM() { NgayKham = p.NgayTaiKham, MaBenhNhan = p.MaBenhNhan, MaLoaiBenh = 1 };
                DataProvider.Instant.DB.PHIEUKHAMs.Add(PhieuKham);
                DataProvider.Instant.DB.SaveChanges();
                ListMedicalRecord.Add(PhieuKham);
                System.Windows.Forms.MessageBox.Show("Tiếp nhận thành công!");
            });
            #endregion
            #region CommandTab4
            Reexamination_ListViewLoadedCommand = new RelayCommand<TabItem>((p) => { return true; }, (p) =>
            {
                DateTime StartDay = (p.FindName("ReexaminationStartDay_DatePicker") as DatePicker).SelectedDate.Value;
                DateTime FinishDay = (p.FindName("ReexaminationFinishDay_DatePicker") as DatePicker).SelectedDate.Value;
                string PatientName = (p.FindName("ReexaminationSearch_Textbox") as System.Windows.Controls.TextBox).Text;
                ListReexamination = new ObservableCollection<PHIEUKHAM>
                                    (DataProvider.Instant.DB.PHIEUKHAMs.Where(x => x.NgayTaiKham.Value >= StartDay && x.NgayTaiKham <= FinishDay && x.BENHNHAN.TenBenhNhan.Contains(PatientName)));

            });
            DisplayListReexaminationTodayCommand = new RelayCommand<TabItem>((p) => { return true; }, (p) =>
            {
                (p.FindName("ReexaminationStartDay_DatePicker") as DatePicker).SelectedDate = DateTime.Today;
                (p.FindName("ReexaminationFinishDay_DatePicker") as DatePicker).SelectedDate = DateTime.Today;
                (p.FindName("ReexaminationSearch_Textbox") as System.Windows.Controls.TextBox).Text = "";
            });
            UpdateReexaminationDateCommand = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Thay đổi ngày tái khám?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        var PhieuKham = DataProvider.Instant.DB.PHIEUKHAMs.Where(x => x.MaPhieuKham == SelectedReexamination.MaPhieuKham).SingleOrDefault();
                        PhieuKham.NgayTaiKham = ReexaminationDateUpdate;

                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show("Ngày chỉnh sửa không phù hợp!");
                        return;
                    }
                    DataProvider.Instant.DB.SaveChanges();
                    SelectedReexamination.NgayTaiKham = ReexaminationDateUpdate;
                    System.Windows.Forms.MessageBox.Show("Thay đổi ngày tái khám thành công!");
                }
            });
            #endregion


        }
        bool CheckIfProceedsReportExist(DateTime date)
        {
            int year = date.Year;
            var report = DataProvider.Instant.DB.BAOCAODOANHTHUTHANGs.Where(x => x.Nam == year);
            if (report.Count() > 0)
                return true;
            return false;
        }
        void CreateReportWholeYear(int month, int year)
        {
            for (int i = 1; i < 13; i++)
            {
                var report = new BAOCAODOANHTHUTHANG() { Thang = i, Nam = year, DoanhThu = 0 };
                DataProvider.Instant.DB.BAOCAODOANHTHUTHANGs.Add(report);
                DataProvider.Instant.DB.SaveChanges();
                BAOCAODOANHTHUTHANG SavedReport = DataProvider.Instant.DB.BAOCAODOANHTHUTHANGs.Where(x => x.Thang == i && x.Nam == year).FirstOrDefault();
                int NumberTotal = DateTime.DaysInMonth(year, i);
                for (int day = 1; day <= NumberTotal; day++)
                {
                    var ReportDetail = new CHITIETBAOCAODOANHTHUTHANG() { MaBaoCaoDoanhThuThang = SavedReport.MaBaoCaoDoanhThuThang, Ngay = day, SoBenhNhan = 0, DoanhThu = 0, TyLe = 0 };
                    DataProvider.Instant.DB.CHITIETBAOCAODOANHTHUTHANGs.Add(ReportDetail);
                    DataProvider.Instant.DB.SaveChanges();
                }
            }
        }
    }
}