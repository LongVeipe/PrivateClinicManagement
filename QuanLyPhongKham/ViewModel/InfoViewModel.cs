using QuanLyKho.ViewModel;
using QuanLyPhongKham.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyPhongKham.ViewModel
{
    class InfoViewModel : BaseViewModel
    {
        private NGUOIDUNG _loginAccount;
        public NGUOIDUNG loginAccount { get => _loginAccount; set { _loginAccount = value; OnPropertyChanged(); } }

        private string _Name;
        public string Name { get => "Tên người dùng: " + loginAccount.TenNguoiDung; set { _Name = value; OnPropertyChanged(); } }

        private string _UserName;
        public string UserName { get => "Tên đăng nhập: " + loginAccount.TenDangNhap; set { _Name = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private string _PasswordNew;
        public string PasswordNew { get => _PasswordNew; set { _PasswordNew = value; OnPropertyChanged(); } }

        private string _PasswordReEnter;
        public string PasswordReEnter { get => _PasswordReEnter; set { _PasswordReEnter = value; OnPropertyChanged(); } }

        public ICommand PasswordCurentChangedCommand { get; set; }
        public ICommand PasswordNewChangedCommand { get; set; }
        public ICommand PasswordReEnterChangedCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        public InfoViewModel(NGUOIDUNG acc)
        {
            loginAccount = acc;

            PasswordCurentChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            PasswordNewChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { PasswordNew = p.Password; });
            PasswordReEnterChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { PasswordReEnter = p.Password; });
            ConfirmCommand = new RelayCommand<Window>((p) =>
            {
                if (String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(PasswordNew) || string.IsNullOrEmpty(PasswordReEnter)) return false;
                if (PasswordNew != PasswordReEnter) return false;
                if (loginAccount.MatKhau != Password) return false;
                return true;
            }, (p) =>
            {
                loginAccount.MatKhau = PasswordNew;
                DataProvider.Instant.DB.SaveChanges();
                MessageBox.Show("Thành công");
                p.Close();
            });
        }
    }
}
