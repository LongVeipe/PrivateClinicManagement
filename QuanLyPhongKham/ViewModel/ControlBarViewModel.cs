using QuanLyKho.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyPhongKham.ViewModel
{
    public class ControlBarViewModel: BaseViewModel
    {
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MoveReceptionWindowCommand { get; set; }
        private string _Icon;
        public string Icon { get => _Icon; set { _Icon = value; OnPropertyChanged(); } }
        public ControlBarViewModel()
        {
            Icon = "WindowMaximize";
            CloseWindowCommand = new RelayCommand<UserControl>((p)=> { return p == null ? false : true; }, (p) =>
                {
                    FrameworkElement window = GetWindowParent(p);
                    var w = window as Window;
                    if(w!= null)
                    {
                        w.Close();
                    }    
                }
            );
            MinimizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                    w.WindowState = WindowState.Minimized;
            }
            );
            MaximizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Maximized)
                    {
                        w.WindowState = WindowState.Maximized;
                        Icon = "ContentCopy";
                    }
                    else
                    {
                        w.WindowState = WindowState.Normal;
                        Icon = "WindowMaximize";
                    }
                }
            }
            );
            MoveReceptionWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent (p);
                var w = window as Window;
                if (w != null)
                    w.DragMove();
            }
            );
        }
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while(parent.Parent !=null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
        
    }
}
