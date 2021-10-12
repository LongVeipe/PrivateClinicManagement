using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuanLyKho.ViewModel;
using QuanLyPhongKham.Model;

namespace QuanLyPhongKham.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {
        private NGUOIDUNG _loginAccount;
        public NGUOIDUNG loginAccount { get => _loginAccount; set { _loginAccount = value; OnPropertyChanged(); } }

        public ICommand ReceptionCommand { get; set; }
        public ICommand InfoCommand { get; set; }
        public ICommand ServiceCommand { get; set; }
        public ICommand AdminCommand { get; set; }
        public ICommand ExaminationCommand { get; set; }
        public ICommand MedicineCommand { get; set; }

        public DashboardViewModel(NGUOIDUNG acc)
        {
            loginAccount = acc;
            InfoCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                InfoWindow wd = new InfoWindow();
                InfoViewModel vm = new InfoViewModel(loginAccount);
                wd.DataContext = vm;
                wd.ShowDialog();
                p.ShowDialog();
            });
            ServiceCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                ServiceWindow wd = new ServiceWindow();
                ServiceViewModel vm = new ServiceViewModel();
                wd.DataContext = vm;
                wd.ShowDialog();
                p.ShowDialog();
            });
            ExaminationCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                ExaminationWindow wd = new ExaminationWindow();
                ExaminationViewModel vm = new ExaminationViewModel();
                wd.DataContext = vm;
                wd.ShowDialog();
                p.ShowDialog();
            });
            MedicineCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                MedicineWindow wd = new MedicineWindow();
                MedicineViewModel vm = new MedicineViewModel();
                wd.DataContext = vm;
                wd.ShowDialog();
                p.ShowDialog();
            });
            ReceptionCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                RececptionWindow wd = new RececptionWindow();
                ReceptionViewModel vm = new ReceptionViewModel();
                wd.DataContext = vm;
                wd.ShowDialog();
                p.ShowDialog();
            });
            AdminCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                AdminWindow wd = new AdminWindow();
                AdminViewModel vm = new AdminViewModel();
                wd.DataContext = vm;
                wd.ShowDialog();
                p.ShowDialog();
            });
        }
    }
}
