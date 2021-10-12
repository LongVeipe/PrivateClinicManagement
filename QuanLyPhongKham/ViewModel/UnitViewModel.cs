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
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<DONVI> _ListUnit;
        public ObservableCollection<DONVI> ListUnit { get => _ListUnit; set { _ListUnit = value; OnPropertyChanged(); } }

        private DONVI _SelectedUnit;
        public DONVI SelectedUnit { get => _SelectedUnit; set
            {
                _SelectedUnit = value; OnPropertyChanged();
                if (SelectedUnit == null)
                    NameOfUnit = "";
                else
                    NameOfUnit = SelectedUnit.TenDonVi;
            } }

        private string _NameOfUnit;
        public string NameOfUnit { get => _NameOfUnit; set { _NameOfUnit = value; OnPropertyChanged(); } }

        public ICommand AddUnitCommand { get; set; }
        public ICommand EditUnitCommand { get; set; }
        public ICommand DeleteUnitCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public UnitViewModel()
        {
            LoadlistUnit();

            AddUnitCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameOfUnit)) return false;
                if (DataProvider.Instant.DB.DONVIs.Where(x => x.TenDonVi == NameOfUnit && x.DaXoa == false).ToList().Count > 0) return false;
                return true;
            }, (p) =>
            {
                DONVI tmp = new DONVI() { TenDonVi = NameOfUnit, DaXoa = false };
                DataProvider.Instant.DB.DONVIs.Add(tmp);
                DataProvider.Instant.DB.SaveChanges();
                NameOfUnit = "";
                LoadlistUnit();
                MessageBox.Show("Thêm đơn vị thành công");
            });

            EditUnitCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedUnit == null) return false;
                if (string.IsNullOrEmpty(NameOfUnit)) return false;
                if (DataProvider.Instant.DB.DONVIs.Where(x => x.TenDonVi == NameOfUnit && x.DaXoa == false).ToList().Count > 0) return false;
                return true;
            }, (p) =>
            {
                SelectedUnit.TenDonVi = NameOfUnit;
                DataProvider.Instant.DB.SaveChanges();
                NameOfUnit = "";
                LoadlistUnit();
                MessageBox.Show("Cập nhật đơn vị thành công");
            });

            DeleteUnitCommand = new RelayCommand<DONVI>((p) =>
            {
                if (DataProvider.Instant.DB.THUOCs.Where(x => x.DaXoa == false && x.MaDonVi == p.MaDonVi).ToList().Count > 0) return false;
                return true;
            }, (p) =>
            {
                DataProvider.Instant.DB.DONVIs.Remove(p);
                DataProvider.Instant.DB.SaveChanges();
                LoadlistUnit();
                MessageBox.Show("Xóa đơn vị thành công");
            });

            ExitCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
        }

        void LoadlistUnit()
        {
            ListUnit = new ObservableCollection<DONVI>(DataProvider.Instant.DB.DONVIs.Where(x => x.DaXoa == false));
        }
    }
}
