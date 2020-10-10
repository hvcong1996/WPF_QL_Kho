using QL_Kho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QL_Kho.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList
        {
            get
            {
                return _TonKhoList;
            }
            set
            {
                _TonKhoList = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; } // Đơn vị đo command
        public ICommand SuplierCommand { get; set; } // Nhà cung cấp command
        public ICommand CustomerCommand { get; set; } // Khách hàng command
        public ICommand ObjectCommand { get; set; } // Sản phẩm command
        public ICommand UserCommand { get; set; } // User command
        public ICommand InputCommand { get; set; } // Input command
        public ICommand OutputCommand { get; set; } // Output command

        #endregion

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; },
                (p) => {
                    if (p == null) return;
                    p.Hide();
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.ShowDialog();

                    if (loginWindow.DataContext == null) return;
                    var loginVM = loginWindow.DataContext as LoginViewModel;
                    if (loginVM.IsLogin)
                    {
                        p.Show();
                        LoadTonKhoData();
                    }
                    else
                    {
                        p.Close();
                    }
                }
                );

            UnitCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    UnitWindow window = new UnitWindow();
                    window.ShowDialog();
                }
                );
            SuplierCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    SuplierWindow window = new SuplierWindow();
                    window.ShowDialog();
                }
                );
            CustomerCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    CustomerWindow window = new CustomerWindow();
                    window.ShowDialog();
                }
                );
            ObjectCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    ObjectWindow window = new ObjectWindow();
                    window.ShowDialog();
                }
                );
            UserCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    UserWindow window = new UserWindow();
                    window.ShowDialog();
                }
                );
            InputCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    InputWindow window = new InputWindow();
                    window.ShowDialog();
                }
                );
            OutputCommand = new RelayCommand<object>((p) => { return true; },
                (p) => {
                    OutputWindow window = new OutputWindow();
                    window.ShowDialog();
                }
                );
        }

        private void LoadTonKhoData()
        {
            TonKhoList = new ObservableCollection<TonKho>();

            var productList = DataProvider.dataProvider.db.Products;

            var dataProduct = DataProvider.dataProvider.db.Products.Count();
            var dataInputInfo = DataProvider.dataProvider.db.InputInfoes.Count();

            int stt = 1;
            foreach(var item in productList)
            {
                // Lấy sản phẩm input
                var inputList = DataProvider.dataProvider.db.InputInfoes.Where(p => p.IdProduct == item.Id);
                // Lấy sản phẩm output
                var outputList = DataProvider.dataProvider.db.OutputInfoes.Where(p => p.IdProduct == item.Id);

                int sumInput = 0;
                int sumOutput = 0;

                // Count sản phẩm input
                if (inputList.Count() > 0)
                {
                    sumInput = (int)inputList.Sum(p => p.Count);
                }

                // Count sản phẩm output
                if (inputList.Count() > 0)
                {
                    sumOutput = (int)outputList.Sum(p => p.Count);
                }

                TonKho tonKho = new TonKho();
                tonKho.STT = stt;
                tonKho.Count = sumInput - sumOutput;
                tonKho.Product = item;
                TonKhoList.Add(tonKho);

                stt++;
            }
        }
    }
}
