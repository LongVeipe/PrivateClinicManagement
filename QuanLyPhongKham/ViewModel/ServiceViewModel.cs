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
    public class ServiceViewModel : BaseViewModel
    {
        private ObservableCollection<HANGSUDUNGDICHVU> _QueueService;
        public ObservableCollection<HANGSUDUNGDICHVU> QueueService { get => _QueueService; set { _QueueService = value; OnPropertyChanged(); } }

        private ObservableCollection<HANGCHOKHAMBENH> _QueueExamination;
        public ObservableCollection<HANGCHOKHAMBENH> QueueExamination { get => _QueueExamination; set { _QueueExamination = value; OnPropertyChanged(); } }

        private string _SearchId;
        public string SearchId { get => _SearchId; set { _SearchId = value; OnPropertyChanged(); } }

        public ICommand CompleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ServiceViewModel()
        {
            LoadQueueService();
            LoadQueueExamination();

            CompleteCommand = new RelayCommand<HANGSUDUNGDICHVU>((p) => { return true; }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Đã cung cấp dịch vụ " + p.DICHVU.TenDichVu + " cho bệnh nhân " + p.PHIEUKHAM.BENHNHAN.TenBenhNhan + " thành công?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes) return;
                HANGCHOKHAMBENH tmp = DataProvider.Instant.DB.HANGCHOKHAMBENHs.Where(x => x.MaPhieuKham == p.MaPhieuKham).SingleOrDefault();
                if (tmp == null)
                    DataProvider.Instant.DB.HANGCHOKHAMBENHs.Add(new HANGCHOKHAMBENH() { MaPhieuKham = p.MaPhieuKham});
                else
                {
                    DateTime t;
                    t = DateTime.Now;
                    tmp.TimeInQueue = t;
                }
                DataProvider.Instant.DB.HANGSUDUNGDICHVUs.Remove(p);
                DataProvider.Instant.DB.SaveChanges();
                LoadQueueExamination();
                LoadQueueService();
            });

            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (String.IsNullOrEmpty(SearchId))
                    LoadQueueService();
                else
                {
                    int Id;
                    bool IsNumberic = int.TryParse(SearchId, out Id);
                    if (!IsNumberic) return;
                    QueueService = new ObservableCollection<HANGSUDUNGDICHVU>(DataProvider.Instant.DB.HANGSUDUNGDICHVUs.Where(x => x.MaPhieuKham == Id).OrderBy(x => x.TimeInQueue));
                }
            });
        }

        void LoadQueueService()
        {
            QueueService = new ObservableCollection<HANGSUDUNGDICHVU>(DataProvider.Instant.DB.HANGSUDUNGDICHVUs.OrderBy(x => x.TimeInQueue));
        }

        void LoadQueueExamination()
        {
            QueueExamination = new ObservableCollection<HANGCHOKHAMBENH>(DataProvider.Instant.DB.HANGCHOKHAMBENHs.OrderBy(x => x.TimeInQueue));
        }
    }
}
