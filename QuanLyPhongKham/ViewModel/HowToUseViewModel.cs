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
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyPhongKham.ViewModel
{
    public class HowToUseViewModel : BaseViewModel
    {
        private ObservableCollection<CACHDUNG> _List;
        public ObservableCollection<CACHDUNG> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private CACHDUNG _Selected;
        public CACHDUNG Selected
        {
            get => _Selected; set
            {
                _Selected = value; OnPropertyChanged();
                if (Selected == null)
                    Name = "";
                else
                    Name = Selected.TenCachDung;
            }
        }

        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public HowToUseViewModel()
        {
            Loadlist();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Name)) return false;
                if (DataProvider.Instant.DB.CACHDUNGs.Where(x => x.TenCachDung == Name && x.DaXoa == false).ToList().Count > 0) return false;
                return true;
            }, (p) =>
            {
                CACHDUNG tmp = new CACHDUNG() { TenCachDung = Name, DaXoa = false };
                DataProvider.Instant.DB.CACHDUNGs.Add(tmp);
                DataProvider.Instant.DB.SaveChanges();
                Name = "";
                Loadlist();
                System.Windows.Forms.MessageBox.Show("Thêm cách dùng thành công");
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (Selected == null) return false;
                if (string.IsNullOrEmpty(Name)) return false;
                if (DataProvider.Instant.DB.CACHDUNGs.Where(x => x.TenCachDung == Name && x.DaXoa == false).ToList().Count > 0) return false;
                return true;
            }, (p) =>
            {
                Selected.TenCachDung = Name;
                DataProvider.Instant.DB.SaveChanges();
                Name = "";
                Loadlist();
                System.Windows.Forms.MessageBox.Show("Cập nhật cách dùng thành công");
            });

            DeleteCommand = new RelayCommand<CACHDUNG>((p) =>
            {
                return true;
            }, (p) =>
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("Xác nhận xoá cách dùng?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    DataProvider.Instant.DB.CACHDUNGs.Remove(p);
                    DataProvider.Instant.DB.SaveChanges();
                    Loadlist();
                    System.Windows.Forms.MessageBox.Show("Xóa đơn vị thành công");

                }
            });

            ExitCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
        }

        void Loadlist()
        {
            List = new ObservableCollection<CACHDUNG>(DataProvider.Instant.DB.CACHDUNGs.Where(x => x.DaXoa == false));
        }
    }
}
