using QuanLyKho.ViewModel;
using QuanLyPhongKham.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyPhongKham.ViewModel
{
    public class AdminViewModel: BaseViewModel
    {
        #region KhaiBaoTab1

        private ObservableCollection<NGUOIDUNG> _ListPersonnel;
        public ObservableCollection<NGUOIDUNG> ListPersonnel { get => _ListPersonnel; set { _ListPersonnel = value; OnPropertyChanged(); } }

        private NGUOIDUNG _SelectedPersonnel;
        public NGUOIDUNG SelectedPersonnel { get => _SelectedPersonnel; set
            {
                _SelectedPersonnel = value; OnPropertyChanged();
                if (SelectedPersonnel == null)
                {
                    PhoneOfPersonnelUpdate = "";
                    AddressOfPersonnelUpdate = "";
                    EmailOfPersonnelUpdate = "";
                } else
                {
                    PhoneOfPersonnelUpdate = SelectedPersonnel.SoDienThoai;
                    AddressOfPersonnelUpdate = SelectedPersonnel.DiaChi;
                    EmailOfPersonnelUpdate = SelectedPersonnel.Email;
                }
            } }

        private string _NameOfNewPersonnel;
        public string NameOfNewPersonnel { get => _NameOfNewPersonnel; set { _NameOfNewPersonnel = value; OnPropertyChanged(); } }

        private DateTime _BirthdayOfNewPersonnel;
        public DateTime BirthdayOfNewPersonnel { get => _BirthdayOfNewPersonnel; set { _BirthdayOfNewPersonnel = value; OnPropertyChanged(); } }

        private string _GenderOfNewPersonnel;
        public string GenderOfNewPersonnel { get => _GenderOfNewPersonnel; set { _GenderOfNewPersonnel = value; OnPropertyChanged(); } }

        private string _PhoneOfNewPersonnel;
        public string PhoneOfNewPersonnel { get => _PhoneOfNewPersonnel; set { _PhoneOfNewPersonnel = value; OnPropertyChanged(); } }

        private string _AddressOfNewPersonnel;
        public string AddressOfNewPersonnel { get => _AddressOfNewPersonnel; set { _AddressOfNewPersonnel = value; OnPropertyChanged(); } }

        private string _EmailOfNewPersonnel;
        public string EmailOfNewPersonnel { get => _EmailOfNewPersonnel; set { _EmailOfNewPersonnel = value; OnPropertyChanged(); } }

        private string _CMNDOfNewPersonnel;
        public string CMNDOfNewPersonnel { get => _CMNDOfNewPersonnel; set { _CMNDOfNewPersonnel = value; OnPropertyChanged(); } }

        private string _UsernameOfNewPersonnel;
        public string UsernameOfNewPersonnel { get => _UsernameOfNewPersonnel; set { _UsernameOfNewPersonnel = value; OnPropertyChanged(); } }

        private string _PhoneOfPersonnelUpdate;
        public string PhoneOfPersonnelUpdate { get => _PhoneOfPersonnelUpdate; set { _PhoneOfPersonnelUpdate = value; OnPropertyChanged(); } }

        private string _AddressOfPersonnelUpdate;
        public string AddressOfPersonnelUpdate { get => _AddressOfPersonnelUpdate; set { _AddressOfPersonnelUpdate = value; OnPropertyChanged(); } }

        private string _EmailOfPersonnelUpdate;
        public string EmailOfPersonnelUpdate { get => _EmailOfPersonnelUpdate; set { _EmailOfPersonnelUpdate = value; OnPropertyChanged(); } }

        public ICommand AddNewPersonnelButtonCommand { get; set; }
        public ICommand ResetPasswordButtonCommand { get; set; }
        public ICommand DeletePersonnelButtonCommand { get; set; }
        public ICommand UpdatePersonnelButtonCommand { get; set; }

        #endregion

        #region KhaiBaoTab2

        private ObservableCollection<BENHNHAN> _ListPatient;
        public ObservableCollection<BENHNHAN> ListPatient { get => _ListPatient; set { _ListPatient = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUKHAM> _ListMedicalRecord;
        public ObservableCollection<PHIEUKHAM> ListMedicalRecord { get => _ListMedicalRecord; set { _ListMedicalRecord = value; OnPropertyChanged(); } }

        private BENHNHAN _SelectedPatient;
        public BENHNHAN SelectedPatient { get => _SelectedPatient;
            set
            {
                _SelectedPatient = value; OnPropertyChanged();
                if (SelectedPatient != null)
                    ListMedicalRecord = new ObservableCollection<PHIEUKHAM>(DataProvider.Instant.DB.PHIEUKHAMs.Where(x => x.MaBenhNhan == SelectedPatient.MaBenhNhan));
            } }

        public ICommand DisplayAllMedicalRecordCommand { get; set; }
        public ICommand Patient_ListViewLoadedCommand { get; set; }
        public ICommand MedicalRecordDetailsButtonCommand { get; set; }

        #endregion

        #region KhaiBaoTab3

        private ObservableCollection<LOAIBENH> _ListDisease;
        public ObservableCollection<LOAIBENH> ListDisease { get => _ListDisease; set { _ListDisease = value; OnPropertyChanged(); } }

        private string _NameOfDiseaseUpdatee;
        public string NameOfDiseaseUpdate { get => _NameOfDiseaseUpdatee; set { _NameOfDiseaseUpdatee = value; OnPropertyChanged(); } }

        private string _NameOfNewService;
        public string NameOfNewService { get => _NameOfNewService; set { _NameOfNewService = value; OnPropertyChanged(); } }

        private string _PriceOfNewService;
        public string PriceOfNewService { get => _PriceOfNewService; set { _PriceOfNewService = value; OnPropertyChanged(); } }

        private string _NameOfServiceUpdate;
        public string NameOfServiceUpdate { get => _NameOfServiceUpdate; set { _NameOfServiceUpdate = value; OnPropertyChanged(); } }

        private string _PriceOfServiceUpdate;
        public string PriceOfServiceUpdate { get => _PriceOfServiceUpdate; set { _PriceOfServiceUpdate = value; OnPropertyChanged(); } }

        private LOAIBENH _SelectedDisease;
        public LOAIBENH SelectedDisease { get => _SelectedDisease;
            set
            {
                _SelectedDisease = value; OnPropertyChanged();
                if (SelectedDisease == null)
                    NameOfDiseaseUpdate = "";
                else
                    NameOfDiseaseUpdate = SelectedDisease.TenLoaiBenh;
            } }

        private DICHVU _SelectedService;
        public DICHVU SelectedService
        {
            get => _SelectedService;
            set
            {
                _SelectedService = value; OnPropertyChanged();
                if (SelectedService == null)
                {
                    NameOfServiceUpdate = "";
                    PriceOfServiceUpdate = "";
                }
                else
                {
                    NameOfServiceUpdate = SelectedService.TenDichVu;
                    PriceOfServiceUpdate = SelectedService.DonGia.ToString();
                }
            }
        }

        private ObservableCollection<DICHVU> _ListService;
        public ObservableCollection<DICHVU> ListService { get => _ListService; set { _ListService = value; OnPropertyChanged(); } }

        public ICommand AddNewDiseaseButtonCommand { get; set; }
        public ICommand DeleteDiseaseCommand { get; set; }
        public ICommand UpdateDiseaseButtonCommand { get; set; }
        public ICommand AddNewServiceButtonCommand { get; set; }
        public ICommand DeleteServiceCommand { get; set; }
        public ICommand UpdateServiceButtonCommand { get; set; }

        #endregion

        #region KhaiBaoTab4

        private ObservableCollection<THUOC> _ListMedicine;
        public ObservableCollection<THUOC> ListMedicine { get => _ListMedicine; set { _ListMedicine = value; OnPropertyChanged(); } }

        private ObservableCollection<DONVI> _ListUnitOfMedicine;
        public ObservableCollection<DONVI> ListUnitOfMedicine { get => _ListUnitOfMedicine; set { _ListUnitOfMedicine = value; OnPropertyChanged(); } }

        private THUOC _SelectedMedicine;
        public THUOC SelectedMedicine { get => _SelectedMedicine; set
            {
                _SelectedMedicine = value; OnPropertyChanged();
                if(SelectedMedicine == null)
                {
                    NameOfMedicineUpdate = "";
                    PriceOfMedicineUpdate = "";
                } else
                {
                    NameOfMedicineUpdate = SelectedMedicine.TenThuoc;
                    PriceOfMedicineUpdate = SelectedMedicine.DonGia.ToString();
                    SelectedUnitOfMedicineUpdate = SelectedMedicine.DONVI;
                }    
            } }

        private DONVI _SelectedUnitOfNewMedicine;
        public DONVI SelectedUnitOfNewMedicine { get => _SelectedUnitOfNewMedicine; set { _SelectedUnitOfNewMedicine = value; OnPropertyChanged(); } }

        private DONVI _SelectedUnitOfMedicineUpdate;
        public DONVI SelectedUnitOfMedicineUpdate { get => _SelectedUnitOfMedicineUpdate; set { _SelectedUnitOfMedicineUpdate = value; OnPropertyChanged(); } }

        private string _NameOfNewMedicine;
        public string NameOfNewMedicine { get => _NameOfNewMedicine; set { _NameOfNewMedicine = value; OnPropertyChanged(); } }

        private string _PriceOfNewMedicine;
        public string PriceOfNewMedicine { get => _PriceOfNewMedicine; set { _PriceOfNewMedicine = value; OnPropertyChanged(); } }

        private string _NameOfMedicineUpdate;
        public string NameOfMedicineUpdate { get => _NameOfMedicineUpdate; set { _NameOfMedicineUpdate = value; OnPropertyChanged(); } }

        private string _PriceOfMedicineUpdate;
        public string PriceOfMedicineUpdate { get => _PriceOfMedicineUpdate; set { _PriceOfMedicineUpdate = value; OnPropertyChanged(); } }

        public ICommand AddNewMedicineCommand { get; set; }
        public ICommand UpdateMedicineCommand { get; set; }
        public ICommand DeleteMedicineCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        #endregion

        #region KhaiBaoTab5

        private THAMSO _Para;
        public THAMSO Para { get => _Para; set { _Para = value; OnPropertyChanged(); } }

        private string _MaxPatitensInDay;
        public string MaxPatitensInDay { get => _MaxPatitensInDay; set { _MaxPatitensInDay = value; OnPropertyChanged(); } }

        private string _PriceExaminaiton;
        public string PriceExaminaiton { get => _PriceExaminaiton; set { _PriceExaminaiton = value; OnPropertyChanged(); } }
        public ICommand NewMaxPatitensCommand { get; set; }
        public ICommand NewPriceExaminaitonCommand { get; set; }

        #endregion

        #region KhaibaoTab6

        private ObservableCollection<BAOCAOSUDUNGTHUOC> _ListMedicineReport;
        public ObservableCollection<BAOCAOSUDUNGTHUOC> ListMedicineReport { get => _ListMedicineReport; set { _ListMedicineReport = value; OnPropertyChanged(); } }
        private ObservableCollection<BAOCAODOANHTHUTHANG> _ListProceedsReport;
        public ObservableCollection<BAOCAODOANHTHUTHANG> ListProceedsReport { get => _ListProceedsReport; set { _ListProceedsReport = value; OnPropertyChanged(); } }
        private ObservableCollection<CHITIETBAOCAODOANHTHUTHANG> _ListProceedsReportDetails;
        public ObservableCollection<CHITIETBAOCAODOANHTHUTHANG> ListProceedsReportDetails { get => _ListProceedsReportDetails; set { _ListProceedsReportDetails = value; OnPropertyChanged(); } }
        private BAOCAODOANHTHUTHANG _SelectedProceedsReport;
        public BAOCAODOANHTHUTHANG SelectedProceedsReport
        {
            get => _SelectedProceedsReport; set
            {
                _SelectedProceedsReport = value;
                OnPropertyChanged();
                if (SelectedProceedsReport != null)
                {
                    ListProceedsReportDetails = new ObservableCollection<CHITIETBAOCAODOANHTHUTHANG>(DataProvider.Instant.DB.CHITIETBAOCAODOANHTHUTHANGs.Where(x => x.MaBaoCaoDoanhThuThang == SelectedProceedsReport.MaBaoCaoDoanhThuThang));
                }
            }
        }

        public ICommand LoadedMedicineReport_ListViewCommand { get; set; }
        public ICommand LoadedProceedsReport_ListViewCommand { get; set; }
        #endregion
        public ICommand LoadedCommand { get; set; }
        public AdminViewModel()
        {
            LoadedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadListPersonnel();
                BirthdayOfNewPersonnel = DateTime.Now;

                LoadListPatient();
                LoadListMedicalRecord();

                LoadListDisease();
                LoadListService();

                LoadListMedicine();
                LoadListUnitOfMedicine();

                LoadPara();
            });

            AddNewPersonnelButtonCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameOfNewPersonnel) ||
                   string.IsNullOrEmpty(BirthdayOfNewPersonnel.ToString()) ||
                   string.IsNullOrEmpty(GenderOfNewPersonnel) ||
                   string.IsNullOrEmpty(CMNDOfNewPersonnel) ||
                   string.IsNullOrEmpty(UsernameOfNewPersonnel)
                ) return false;
                if (DataProvider.Instant.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == UsernameOfNewPersonnel || x.CMND == CMNDOfNewPersonnel).ToList().Count > 0) return false;
                return true;
            }, (p) =>
            {
                NGUOIDUNG tmp = new NGUOIDUNG() { TenNguoiDung = NameOfNewPersonnel, TenDangNhap = UsernameOfNewPersonnel, CMND = CMNDOfNewPersonnel, DiaChi = AddressOfNewPersonnel, Email = EmailOfNewPersonnel, GioiTinh = GenderOfNewPersonnel, MatKhau = "12345", NgaySinh = BirthdayOfNewPersonnel, SoDienThoai = PhoneOfNewPersonnel };
                DataProvider.Instant.DB.NGUOIDUNGs.Add(tmp);
                DataProvider.Instant.DB.SaveChanges();
                LoadListPersonnel();
                System.Windows.Forms.MessageBox.Show("Thêm người dùng thành công với mật khẩu mặc định là 12345");
            });

            ResetPasswordButtonCommand = new RelayCommand<NGUOIDUNG>((p) =>
            {
                if(p != null && p.TenDangNhap == "admin") return false;
                return true;
            }, (p) =>
            {
                p.MatKhau = "12345";
                DataProvider.Instant.DB.SaveChanges();
                LoadListPersonnel();
                System.Windows.Forms.MessageBox.Show("Mật khẩu mới là 12345");
            });

            DeletePersonnelButtonCommand = new RelayCommand<NGUOIDUNG>((p) =>
            {
                if (p != null && p.TenDangNhap == "admin") return false;
                return true;
            }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Xác nhận xoá nhân viên?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    DataProvider.Instant.DB.NGUOIDUNGs.Remove(p);
                    DataProvider.Instant.DB.SaveChanges();
                    LoadListPersonnel();
                    System.Windows.Forms.MessageBox.Show("Xóa thành công");
                }
            });

            UpdatePersonnelButtonCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedPersonnel == null) return false;
                return true;
            }, (p) =>
            {
                SelectedPersonnel.SoDienThoai = PhoneOfPersonnelUpdate;
                SelectedPersonnel.DiaChi = AddressOfPersonnelUpdate;
                SelectedPersonnel.Email = EmailOfPersonnelUpdate;
                DataProvider.Instant.DB.SaveChanges();
                LoadListPersonnel();
                System.Windows.Forms.MessageBox.Show("Cập nhật thành công");
            });

            DisplayAllMedicalRecordCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadListMedicalRecord();
            });

            Patient_ListViewLoadedCommand = new RelayCommand<System.Windows.Controls.TextBox>((p) => { return true; }, (p) =>
            {
                string PatitentName = p.Text;
                ListPatient = new ObservableCollection<BENHNHAN>(DataProvider.Instant.DB.BENHNHANs.Where(x => x.TenBenhNhan.ToString().Contains(PatitentName)));
            });

            MedicalRecordDetailsButtonCommand = new RelayCommand<PHIEUKHAM>((p) => { return true; }, (p) =>
            {
                MedicalRecordDetailWindow wd = new MedicalRecordDetailWindow();
                MedicalRecordDetailViewModel vm = new MedicalRecordDetailViewModel(p);
                wd.DataContext = vm;
                wd.ShowDialog();
            });

            AddNewDiseaseButtonCommand = new RelayCommand<System.Windows.Controls.TextBox>((p) =>
            {
                if (p != null && string.IsNullOrEmpty(p.Text)) return false;
                LOAIBENH tmp = DataProvider.Instant.DB.LOAIBENHs.Where(x => x.TenLoaiBenh == p.Text && x.DaXoa == false).SingleOrDefault();
                if (tmp != null) return false;
                return true;
            }, (p) =>
            {
                LOAIBENH NewDisease = new LOAIBENH() { TenLoaiBenh = p.Text , DaXoa = false};
                DataProvider.Instant.DB.LOAIBENHs.Add(NewDisease);
                DataProvider.Instant.DB.SaveChanges();
                LoadListDisease();
                p.Text = "";
                System.Windows.MessageBox.Show("Thêm loại bệnh mới thành công");
            });

            DeleteDiseaseCommand = new RelayCommand<LOAIBENH>((p) => { return true; }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Xác nhận xoá loại bệnh?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    p.DaXoa = true;
                    DataProvider.Instant.DB.SaveChanges();
                    LoadListDisease();
                    System.Windows.Forms.MessageBox.Show("Xóa thành công");
                }
            });

            UpdateDiseaseButtonCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedDisease == null) return false;
                if (string.IsNullOrEmpty(NameOfDiseaseUpdate)) return false;
                LOAIBENH tmp = DataProvider.Instant.DB.LOAIBENHs.Where(x => x.TenLoaiBenh == NameOfDiseaseUpdate && x.DaXoa == false && x.MaLoaiBenh != SelectedDisease.MaLoaiBenh).SingleOrDefault();
                if (tmp != null) return false;
                return true;
            }, (p) =>
            {
                SelectedDisease.TenLoaiBenh = NameOfDiseaseUpdate;
                DataProvider.Instant.DB.SaveChanges();
                LoadListDisease();
                LoadListMedicalRecord();
                NameOfDiseaseUpdate = "";
                System.Windows.Forms.MessageBox.Show("Cập nhật tên loại bệnh mới thành công");
            });

            AddNewServiceButtonCommand = new RelayCommand<StackPanel>((p) =>
            {
                if (string.IsNullOrEmpty(NameOfNewService) || string.IsNullOrEmpty(PriceOfNewService)) return false;
                if (DataProvider.Instant.DB.DICHVUs.Where(x => x.TenDichVu == NameOfNewService && x.DaXoa == false).SingleOrDefault() != null) return false;
                int PriceINT;
                try { PriceINT = int.Parse(PriceOfNewService); } catch { PriceINT = 0; }
                if (PriceINT == 0) return false;
                return true;
            }, (p) =>
            {
                int PriceINT;
                try { PriceINT = int.Parse(PriceOfNewService); } catch { PriceINT = 0; }
                DICHVU tmp = new DICHVU() { TenDichVu = NameOfNewService, DonGia = PriceINT, DaXoa = false };
                DataProvider.Instant.DB.DICHVUs.Add(tmp);
                DataProvider.Instant.DB.SaveChanges();
                LoadListService();
                NameOfNewService = "";
                PriceOfNewService = "";
                (p.FindName("NameOfService_TextBox") as System.Windows.Controls.TextBox).Text = "";
                (p.FindName("PriceOfService_TextBox") as System.Windows.Controls.TextBox).Text = "";
                System.Windows.Forms.MessageBox.Show("Thêm dịch vụ thành công");
            });

            DeleteServiceCommand = new RelayCommand<DICHVU>((p) => { return true; }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Xác nhận xoá dịch vụ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    p.DaXoa = true;
                    DataProvider.Instant.DB.SaveChanges();
                    LoadListService();
                    System.Windows.Forms.MessageBox.Show("Xóa thành công");
                }
            });

            UpdateServiceButtonCommand = new RelayCommand<StackPanel>((p) =>
            {
                if (string.IsNullOrEmpty(NameOfServiceUpdate) || string.IsNullOrEmpty(PriceOfServiceUpdate)) return false;
                if (DataProvider.Instant.DB.DICHVUs.Where(x => x.TenDichVu == NameOfServiceUpdate && x.DaXoa == false && x.MaDichVu != SelectedService.MaDichVu).SingleOrDefault() != null) return false;
                int PriceINT;
                try { PriceINT = int.Parse(PriceOfServiceUpdate); } catch { PriceINT = 0; }
                if (PriceINT == 0) return false;
                return true;
            }, (p) =>
            {
                SelectedService.TenDichVu = NameOfServiceUpdate;
                int PriceINT;
                try { PriceINT = int.Parse(PriceOfServiceUpdate); } catch { PriceINT = 0; }
                SelectedService.DonGia = PriceINT;
                DataProvider.Instant.DB.SaveChanges();
                LoadListService();
                NameOfNewService = "";
                PriceOfNewService = "";
                System.Windows.Forms.MessageBox.Show("Cập nhật dịch vụ thành công");
            });

            AddNewMedicineCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameOfNewMedicine) || string.IsNullOrEmpty(PriceOfNewMedicine)) return false;
                int PriceINT;
                try { PriceINT = int.Parse(PriceOfNewMedicine); } catch { PriceINT = 0; }
                if (PriceINT == 0) return false;
                if (SelectedUnitOfNewMedicine == null) return false;
                if (DataProvider.Instant.DB.THUOCs.Where(x => x.TenThuoc == NameOfNewMedicine && x.DaXoa == false).ToList().Count > 0) return false;
                return true;
            }, (p) =>
            {
                int PriceINT;
                try { PriceINT = int.Parse(PriceOfNewMedicine); } catch { PriceINT = 0; }
                THUOC tmp = new THUOC() { TenThuoc = NameOfNewMedicine, DonGia = PriceINT, MaDonVi = SelectedUnitOfNewMedicine.MaDonVi, DaXoa = false, SoLuongTon = 0 };
                DataProvider.Instant.DB.THUOCs.Add(tmp);
                DataProvider.Instant.DB.SaveChanges();
                NameOfNewMedicine = "";
                PriceOfNewMedicine = "";
                LoadListMedicine();
                System.Windows.Forms.MessageBox.Show("Thêm thuốc mới thành công");
            });

            UpdateMedicineCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedMedicine == null) return false;
                if (string.IsNullOrEmpty(NameOfMedicineUpdate) || string.IsNullOrEmpty(PriceOfMedicineUpdate)) return false;
                int PriceINT;
                try { PriceINT = int.Parse(PriceOfMedicineUpdate); } catch { PriceINT = 0; }
                if (PriceINT == 0) return false;
                if (SelectedUnitOfMedicineUpdate == null) return false;
                if (DataProvider.Instant.DB.THUOCs.Where(x => x.TenThuoc == NameOfMedicineUpdate && x.DaXoa == false && x.MaThuoc != SelectedMedicine.MaThuoc).ToList().Count > 0) return false;
                return true;
            }, (p) =>
            {
                int PriceINT;
                try { PriceINT = int.Parse(PriceOfMedicineUpdate); } catch { PriceINT = 0; }
                SelectedMedicine.TenThuoc = NameOfMedicineUpdate;
                SelectedMedicine.DonGia = PriceINT;
                SelectedMedicine.MaDonVi = SelectedUnitOfMedicineUpdate.MaDonVi;
                DataProvider.Instant.DB.SaveChanges();
                NameOfMedicineUpdate = "";
                PriceOfMedicineUpdate = "";
                LoadListMedicine();
                System.Windows.Forms.MessageBox.Show("Cập nhật thuốc mới thành công");
            });

            DeleteMedicineCommand = new RelayCommand<THUOC>((p) =>
            {
                return true;
            }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Xác nhận xoá thuốc?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    p.DaXoa = true;
                    p.SoLuongTon = 0;
                    DataProvider.Instant.DB.SaveChanges();
                    LoadListMedicine();
                    System.Windows.Forms.MessageBox.Show("Xóa thuốc thành công");
                }
            });

            UnitCommand = new RelayCommand<THUOC>((p) =>
            {
                return true;
            }, (p) =>
            {
                UnitWindow wd = new UnitWindow();
                UnitViewModel vm = new UnitViewModel();
                wd.DataContext = vm;
                wd.ShowDialog();
                LoadListUnitOfMedicine();
            });

            NewMaxPatitensCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaxPatitensInDay)) return false;
                int CountINT;
                try { CountINT = int.Parse(MaxPatitensInDay); } catch { CountINT = 0; }
                if (CountINT == 0) return false;
                return true;
            }, (p) =>
            {
                int CountINT;
                try { CountINT = int.Parse(MaxPatitensInDay); } catch { CountINT = 0; }
                Para.SoLuongBenhNhanToiDaTrongNgay = CountINT;
                DataProvider.Instant.DB.SaveChanges();
                MaxPatitensInDay = "";
                LoadPara();
                System.Windows.Forms.MessageBox.Show("Cập nhật thành công");
            });

            NewPriceExaminaitonCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(PriceExaminaiton)) return false;
                int CountINT;
                try { CountINT = int.Parse(PriceExaminaiton); } catch { CountINT = 0; }
                if (CountINT == 0) return false;
                return true;
            }, (p) =>
            {
                int CountINT;
                try { CountINT = int.Parse(PriceExaminaiton); } catch { CountINT = 0; }
                Para.TienKhamBenh = CountINT;
                DataProvider.Instant.DB.SaveChanges();
                PriceExaminaiton = "";
                LoadPara();
                System.Windows.Forms.MessageBox.Show("Cập nhật thành công");
            });
            LoadedMedicineReport_ListViewCommand = new RelayCommand<TabItem>((p) => { return true; }, (p) =>
            {
                string year = ((p.FindName("YearOfMedicineReport_Textbox")) as System.Windows.Controls.TextBox).Text;
                string month = ((p.FindName("MonthOfMedicineReport_Textbox")) as System.Windows.Controls.TextBox).Text;

                ListMedicineReport = new ObservableCollection<BAOCAOSUDUNGTHUOC>(DataProvider.Instant.DB.BAOCAOSUDUNGTHUOCs.Where(x => x.Nam.ToString() == year && x.Thang.ToString() == month));
            });
            LoadedProceedsReport_ListViewCommand = new RelayCommand<TabItem>((p) => { return true; }, (p) =>
            {
                string year = ((p.FindName("YearOfProceedsReport_Textbox")) as System.Windows.Controls.TextBox).Text;
                ListProceedsReport = new ObservableCollection<BAOCAODOANHTHUTHANG>(DataProvider.Instant.DB.BAOCAODOANHTHUTHANGs.Where(x => x.Nam.ToString() == year));
            });
        }

        #region MethodTab1
        void LoadListPersonnel()
        {
            ListPersonnel = new ObservableCollection<NGUOIDUNG>(DataProvider.Instant.DB.NGUOIDUNGs);
        }

        #endregion

        #region MethodTab2

        void LoadListPatient()
        {
            ListPatient = new ObservableCollection<BENHNHAN>(DataProvider.Instant.DB.BENHNHANs);
        }
        void LoadListMedicalRecord()
        {
            ListMedicalRecord = new ObservableCollection<PHIEUKHAM>(DataProvider.Instant.DB.PHIEUKHAMs);
        }

        #endregion

        #region MethodTab3

        void LoadListDisease()
        {
            ListDisease = new ObservableCollection<LOAIBENH>(DataProvider.Instant.DB.LOAIBENHs.Where(x => x.MaLoaiBenh != 1 && x.DaXoa == false));
        }
        void LoadListService()
        {
            ListService = new ObservableCollection<DICHVU>(DataProvider.Instant.DB.DICHVUs.Where(x => x.DaXoa == false));
        }

        #endregion

        #region MethodTab4

        void LoadListMedicine()
        {
            ListMedicine = new ObservableCollection<THUOC>(DataProvider.Instant.DB.THUOCs.Where(x => x.DaXoa == false));
        }

        void LoadListUnitOfMedicine()
        {
            ListUnitOfMedicine = new ObservableCollection<DONVI>(DataProvider.Instant.DB.DONVIs.Where(x => x.DaXoa == false));
        }
        #endregion

        #region MethodTab5

        void LoadPara()
        {
            Para = DataProvider.Instant.DB.THAMSOes.SingleOrDefault();
        }

        #endregion
    }
}
