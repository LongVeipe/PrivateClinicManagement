using QuanLyKho.ViewModel;
using QuanLyPhongKham.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyPhongKham.ViewModel
{
    public class ExaminationViewModel : BaseViewModel
    {
        private ObservableCollection<HANGCHOKHAMBENH> _QueueExamination;
        public ObservableCollection<HANGCHOKHAMBENH> QueueExamination { get => _QueueExamination; set { _QueueExamination = value; OnPropertyChanged(); } }

        private ObservableCollection<LOAIBENH> _ListIllness;
        public ObservableCollection<LOAIBENH> ListIllness { get => _ListIllness; set { _ListIllness = value; OnPropertyChanged(); } }

        private LOAIBENH _SelectedIllness;
        public LOAIBENH SelectedIllness { get => _SelectedIllness; set { _SelectedIllness = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUKHAM> _ListHistory;
        public ObservableCollection<PHIEUKHAM> ListHistory { get => _ListHistory; set { _ListHistory = value; OnPropertyChanged(); } }

        private String _Symptom;
        public String Symptom { get => _Symptom; set { _Symptom = value; OnPropertyChanged(); } }

        private Boolean _ReExam;
        public Boolean ReExam { get => _ReExam; set { _ReExam = value; OnPropertyChanged(); } }

        private DateTime? _DateReExam;
        public DateTime? DateReExam { get => _DateReExam; set { _DateReExam = value; OnPropertyChanged(); } }

        private HANGCHOKHAMBENH _SelectedExamination;
        public HANGCHOKHAMBENH SelectedExamination { get => _SelectedExamination; set
            {
                _SelectedExamination = value; OnPropertyChanged();
                if (SelectedExamination != null)
                {
                    LoadListHistory(SelectedExamination.PHIEUKHAM);
                    Symptom = SelectedExamination.PHIEUKHAM.TrieuChung;
                    SelectedIllness = SelectedExamination.PHIEUKHAM.LOAIBENH;
                    if(String.IsNullOrEmpty(SelectedExamination.PHIEUKHAM.NgayTaiKham.ToString()))
                    {
                        ReExam = false;
                        DateReExam = null;
                    }
                    else
                    {
                        ReExam = true;
                        DateReExam = SelectedExamination.PHIEUKHAM.NgayTaiKham;
                    }    
                }    
                else
                {
                    Symptom = "";
                    SelectedIllness.MaLoaiBenh = 1;
                    DateReExam = null;
                }
                    
            } }
        public ICommand RequestServiceCommand { get; set; }
        public ICommand CreatePrescriptionCommand { get; set; }
        public ICommand PayCommand { get; set; }
        public ICommand CompleteCommand { get; set; }
        public ExaminationViewModel()
        {
            LoadQueueExamination();
            LoadListIllness();

            RequestServiceCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedExamination == null) return false;
                return true;
            }, (p) =>
            {
                RequestServiceWindow wd = new RequestServiceWindow();
                RequestServiceViewModel vm = new RequestServiceViewModel(SelectedExamination.PHIEUKHAM);
                wd.DataContext = vm;
                wd.ShowDialog();
            });
            CreatePrescriptionCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedExamination == null) return false;
                return true;
            }, (p) =>
            {
                CreatePrescriptionWindow wd = new CreatePrescriptionWindow();
                CreatePrescriptionViewModel vm = new CreatePrescriptionViewModel(SelectedExamination.PHIEUKHAM);
                wd.DataContext = vm;
                wd.ShowDialog();
            });
            CompleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedExamination == null) return false;
                return true;
            }, (p) =>
            {
                if(String.IsNullOrEmpty(Symptom))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin triệu chứng");
                    return;
                }
                if(SelectedIllness == null || SelectedIllness.MaLoaiBenh == 1)
                {
                    MessageBox.Show("Vui lòng chẩn đoán cho bệnh nhân");
                    return;
                }
                if (ReExam)
                    SelectedExamination.PHIEUKHAM.NgayTaiKham = DateReExam;
                else
                    SelectedExamination.PHIEUKHAM.NgayTaiKham = null;
                SelectedExamination.PHIEUKHAM.TrieuChung = Symptom;
                SelectedExamination.PHIEUKHAM.LOAIBENH = SelectedIllness;
                DataProvider.Instant.DB.HANGCHOKHAMBENHs.Remove(SelectedExamination);
                DataProvider.Instant.DB.SaveChanges();
                ListHistory = null;
                LoadListIllness();
                LoadQueueExamination();
            });
            PayCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedExamination == null) return false;
                else if (DataProvider.Instant.DB.HANGCHOTHANHTOANs.Where(x => x.MaPhieuKham == SelectedExamination.MaPhieuKham).ToList().Count == 0) 
                    return false;
                return true;
            }, (p) =>
            {
                DataProvider.Instant.DB.HANGCHOKHAMBENHs.Remove(SelectedExamination);
                DataProvider.Instant.DB.SaveChanges();
                ListHistory = null;
                LoadListIllness();
                LoadQueueExamination();
            });
        }

        void LoadQueueExamination()
        {
            QueueExamination = new ObservableCollection<HANGCHOKHAMBENH>(DataProvider.Instant.DB.HANGCHOKHAMBENHs.OrderBy(x => x.TimeInQueue));
        }

        void LoadListHistory(PHIEUKHAM card)
        {
            ListHistory = new ObservableCollection<PHIEUKHAM>(DataProvider.Instant.DB.PHIEUKHAMs.Where(x => x.MaPhieuKham != card.MaPhieuKham && x.MaBenhNhan == card.MaBenhNhan));
        }

        void LoadListIllness()
        {
            ListIllness = new ObservableCollection<LOAIBENH>(DataProvider.Instant.DB.LOAIBENHs.Where(x => x.DaXoa == false));
        }
    }
}
