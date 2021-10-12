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
    public class MedicalRecordDetailViewModel : BaseViewModel
    {
        private PHIEUKHAM _Record;
        public PHIEUKHAM Record { get => _Record; set { _Record = value; OnPropertyChanged(); } }

        private ObservableCollection<CHITIETDICHVU> _ListService;
        public ObservableCollection<CHITIETDICHVU> ListService { get => _ListService; set { _ListService = value; OnPropertyChanged(); } }

        private ObservableCollection<DONTHUOC> _ListMedicine;
        public ObservableCollection<DONTHUOC> ListMedicine { get => _ListMedicine; set { _ListMedicine = value; OnPropertyChanged(); } }
        public ICommand ExitCommand { get; set; }
        public MedicalRecordDetailViewModel(PHIEUKHAM record)
        {
            Record = record;

            ListService = new ObservableCollection<CHITIETDICHVU>(DataProvider.Instant.DB.CHITIETDICHVUs.Where(x => x.MaPhieuKham == Record.MaPhieuKham));
            ListMedicine = new ObservableCollection<DONTHUOC>(DataProvider.Instant.DB.DONTHUOCs.Where(x => x.MaPhieuKham == Record.MaPhieuKham));

            ExitCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
    }
}
