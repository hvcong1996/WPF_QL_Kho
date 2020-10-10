using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QL_Kho.ViewModel
{
    public class ControlBarViewModel:BaseViewModel
    {
        #region Commands

        public ICommand MinimumSizeWindowCommand { get; set; }
        public ICommand MaximumSizeWindowCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }

        #endregion

        public ControlBarViewModel()
        {
            MinimumSizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; },
                (p) => {
                    FrameworkElement element = GetWindowParent(p);
                    var window = element as Window;
                    if (window != null)
                    {
                        if (window.WindowState != WindowState.Minimized) window.WindowState = WindowState.Minimized;
                        else window.WindowState = WindowState.Maximized;
                    }
                }
                );

            MaximumSizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; },
                (p) => {
                    FrameworkElement element = GetWindowParent(p);
                    var window = element as Window;
                    if (window != null)
                    {
                        if (window.WindowState != WindowState.Maximized) window.WindowState = WindowState.Maximized;
                        else window.WindowState = WindowState.Normal;
                    }
                }
                );

            CloseWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; },
                (p) => {
                    FrameworkElement element = GetWindowParent(p);
                    var window = element as Window;
                    if (window != null) window.Close();
                }
                );

            MouseMoveWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; },
                (p) => {
                    FrameworkElement element = GetWindowParent(p);
                    var window = element as Window;
                    if (window != null)
                    {
                        try
                        {
                            window.DragMove();
                        }
                        catch { }
                    }
                }
                );
        }

        /// <summary>
        /// Lấy Window để close
        /// </summary>
        /// <param name="userControl"></param>
        /// <returns></returns>
        public FrameworkElement GetWindowParent(UserControl userControl)
        {
            FrameworkElement parent = userControl;

            // Window nằm ở ngoài cùng nên for đến parent cuối cùng
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
