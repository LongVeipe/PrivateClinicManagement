using QuanLyKho.ViewModel;
using QuanLyPhongKham.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyPhongKham.ViewModel
{
    public class RequestServiceViewModel : BaseViewModel
    {
        private PHIEUKHAM _CardExamination;
        public PHIEUKHAM CardExamination { get => _CardExamination; set { _CardExamination = value; OnPropertyChanged(); } }

        private ObservableCollection<DICHVU> _ListService;
        public ObservableCollection<DICHVU> ListService { get => _ListService; set { _ListService = value; OnPropertyChanged(); } }

        private ObservableCollection<HANGCHOTHANHTOAN> _ListRequest;
        public ObservableCollection<HANGCHOTHANHTOAN> ListRequest { get => _ListRequest; set { _ListRequest = value; OnPropertyChanged(); } }

        private ObservableCollection<CHITIETDICHVU> _ListUsed;
        public ObservableCollection<CHITIETDICHVU> ListUsed { get => _ListUsed; set { _ListUsed = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public RequestServiceViewModel(PHIEUKHAM tmp)
        {
            CardExamination = tmp;
            LoadListService();
            LoadListRequest();
            LoadListUsed();

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            AddCommand = new RelayCommand<DICHVU>((p) => { return true; }, (p) =>
            {
                DataProvider.Instant.DB.HANGCHOTHANHTOANs.Add(new HANGCHOTHANHTOAN() { MaPhieuKham = CardExamination.MaPhieuKham, MaDichVu = p.MaDichVu });
                DataProvider.Instant.DB.SaveChanges();
                LoadListService();
                LoadListRequest();
                LoadListUsed();
            });

            DeleteCommand = new RelayCommand<HANGCHOTHANHTOAN>((p) => { return true; }, (p) =>
            {
                DataProvider.Instant.DB.HANGCHOTHANHTOANs.Remove(p);
                DataProvider.Instant.DB.SaveChanges();
                LoadListService();
                LoadListRequest();
                LoadListUsed();
            });
        }

        void LoadListService()
        {
            ListService = new ObservableCollection<DICHVU>(DataProvider.Instant.DB.DICHVUs.Where(x => x.DaXoa == false));
        }

        void LoadListRequest()
        {
            ListRequest = new ObservableCollection<HANGCHOTHANHTOAN>(DataProvider.Instant.DB.HANGCHOTHANHTOANs.Where(x => x.MaPhieuKham == CardExamination.MaPhieuKham));
            foreach(HANGCHOTHANHTOAN item in ListRequest)
            {
                ListService.Remove(item.DICHVU);
            }
        }

        void LoadListUsed()
        {
            ListUsed = new ObservableCollection<CHITIETDICHVU>(DataProvider.Instant.DB.CHITIETDICHVUs.Where(x => x.MaPhieuKham == CardExamination.MaPhieuKham));
            foreach (CHITIETDICHVU item in ListUsed)
            {
                ListService.Remove(item.DICHVU);
            }
        }
    }
}
