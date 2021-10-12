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
    public class CreatePrescriptionViewModel : BaseViewModel
    {
        private PHIEUKHAM _CardExamination;
        public PHIEUKHAM CardExamination { get => _CardExamination; set { _CardExamination = value; OnPropertyChanged(); } }

        private string _Unit;
        public string Unit { get => _Unit; set { _Unit = value; OnPropertyChanged(); } }

        private string _Count;
        public string Count { get => _Count; set { _Count = value; OnPropertyChanged(); try { CountINT = int.Parse(Count); } catch { CountINT = 0; } } }

        private int _CountINT;
        public int CountINT { get => _CountINT; set { _CountINT = value; OnPropertyChanged(); } }

        private ObservableCollection<THUOC> _ListMedicine;
        public ObservableCollection<THUOC> ListMedicine { get => _ListMedicine; set { _ListMedicine = value; OnPropertyChanged(); } }

        private THUOC _SelectedMedicine;
        public THUOC SelectedMedicine { get => _SelectedMedicine; set
            { 
                _SelectedMedicine = value; OnPropertyChanged();
                if (SelectedMedicine != null)
                    Unit = SelectedMedicine.DONVI.TenDonVi;
            } }

        private ObservableCollection<CACHDUNG> _ListHowToUse;
        public ObservableCollection<CACHDUNG> ListHowToUse { get => _ListHowToUse; set { _ListHowToUse = value; OnPropertyChanged(); } }

        private CACHDUNG _SelectedHowToUse;
        public CACHDUNG SelectedHowToUse { get => _SelectedHowToUse; set { _SelectedHowToUse = value; OnPropertyChanged(); } }

        private ObservableCollection<DONTHUOC> _DetailPrescription;
        public ObservableCollection<DONTHUOC> DetailPrescription { get => _DetailPrescription; set { _DetailPrescription = value; OnPropertyChanged(); } }

        private DONTHUOC _SelectedDetailPrescription;
        public DONTHUOC SelectedDetailPrescription { get => _SelectedDetailPrescription; set { _SelectedDetailPrescription = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddHowToUseCommand { get; set; }
        public CreatePrescriptionViewModel(PHIEUKHAM tmp)
        {
            CardExamination = tmp;
            LoadPrescription();
            LoadListMedicine();
            LoadListHowToUse();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedMedicine == null) return false;
                if (SelectedHowToUse == null) return false;
                if (String.IsNullOrEmpty(Unit)) return false;
                if (CountINT <= 0) return false;
                return true;
            }, (p) =>
            {
                DONTHUOC temp = DataProvider.Instant.DB.DONTHUOCs.Where(x => x.MaPhieuKham == CardExamination.MaPhieuKham && x.MaThuoc == SelectedMedicine.MaThuoc).SingleOrDefault();
                if (temp == null)
                {
                    DataProvider.Instant.DB.DONTHUOCs.Add(new DONTHUOC() { MaPhieuKham = CardExamination.MaPhieuKham, MaThuoc = SelectedMedicine.MaThuoc, SoLuongKe = CountINT, MaCachDung = SelectedHowToUse.MaCachDung, SoLuongBan = 0, DonGia = 0, ThanhTien = 0});
                    DataProvider.Instant.DB.SaveChanges();
                    LoadPrescription();
                } else
                {
                    temp.SoLuongKe = temp.SoLuongKe + CountINT;
                    DataProvider.Instant.DB.SaveChanges();
                    LoadPrescription();
                }
            });
            DeleteCommand = new RelayCommand<DONTHUOC>((p) => { return true; }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Xác nhận xoá thuốc?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    DONTHUOC temp = DataProvider.Instant.DB.DONTHUOCs.Where(x => x.MaPhieuKham == CardExamination.MaPhieuKham && x.MaThuoc == p.MaThuoc).SingleOrDefault();
                    DataProvider.Instant.DB.DONTHUOCs.Remove(temp);
                    DataProvider.Instant.DB.SaveChanges();
                    LoadPrescription();
                }
                
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            AddHowToUseCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                HowToUseWindow wd = new HowToUseWindow();
                HowToUseViewModel vm = new HowToUseViewModel();
                wd.DataContext = vm;
                wd.ShowDialog();
                LoadListHowToUse();
            });
        }

        void LoadListMedicine()
        {
            ListMedicine = new ObservableCollection<THUOC>(DataProvider.Instant.DB.THUOCs.Where(x => x.DaXoa == false));
        }

        void LoadListHowToUse()
        {
            ListHowToUse = new ObservableCollection<CACHDUNG>(DataProvider.Instant.DB.CACHDUNGs);
        }

        void LoadPrescription()
        {
            DetailPrescription = new ObservableCollection<DONTHUOC>(DataProvider.Instant.DB.DONTHUOCs.Where(x => x.MaPhieuKham == CardExamination.MaPhieuKham));
        }
    }
}
