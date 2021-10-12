using MaterialDesignThemes.Wpf;
using QuanLyKho.ViewModel;
using QuanLyPhongKham.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyPhongKham.ViewModel
{
    public class LoginViewModel: BaseViewModel
    {
        private string _Username;
        public string Username  { get => _Username;  set { _Username = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand MoveLoginWindowCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public LoginViewModel()
        {
            MoveLoginWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Bạn muốn thoát chương trình?","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    p.Close(); 
                }
            });
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>{Login(p);});
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>{Password = p.Password;});
        }

        
        void Login(Window p)
        {

            if (p == null)
                return;
            NGUOIDUNG Accounts = (DataProvider.Instant.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == Username && x.MatKhau == Password)).SingleOrDefault();
            if (Accounts != null)
            {
                p.Hide();
                
                DashboardWindow dashboardWindow = new DashboardWindow();
                DashboardViewModel vm = new DashboardViewModel(Accounts);
                dashboardWindow.DataContext = vm;
                dashboardWindow.ShowDialog();
                p.Show();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }
        }


    }
}
