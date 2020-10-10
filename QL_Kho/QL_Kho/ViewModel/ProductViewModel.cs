using QL_Kho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QL_Kho.ViewModel
{
    public class ProductViewModel:BaseViewModel
    {
        private ObservableCollection<Product> _List;
        public ObservableCollection<Product> List
        {
            get
            {
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged();
            }
        }

        private Product _SelectedProductItem;
        public Product SelectedProductItem
        {
            get
            {
                return _SelectedProductItem;
            }
            set
            {
                _SelectedProductItem = value;
                OnPropertyChanged();
                if (SelectedProductItem != null)
                {
                    Id = SelectedProductItem.Id;
                    DisplayName = SelectedProductItem.DisplayName;
                    SelectedUnit = SelectedProductItem.Unit;
                    SelectedSuplier = SelectedProductItem.Suplier;
                    QRCode = SelectedProductItem.QRCode;
                    BarCode = SelectedProductItem.BarCode;
                }
            }
        }

        private string _Id;
        public string Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                OnPropertyChanged();
            }
        }

        private string _DisplayName;
        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                _DisplayName = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Unit> _Unit;
        public ObservableCollection<Unit> Unit
        {
            get
            {
                return _Unit;
            }
            set
            {
                _Unit = value;
                OnPropertyChanged();
            }
        }
        private Unit _SelectedUnit;
        public Unit SelectedUnit
        {
            get
            {
                return _SelectedUnit;
            }
            set
            {
                _SelectedUnit = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Suplier> _Suplier;
        public ObservableCollection<Suplier> Suplier
        {
            get
            {
                return _Suplier;
            }
            set
            {
                _Suplier = value;
                OnPropertyChanged();
            }
        }

        private Suplier _SelectedSuplier;
        public Suplier SelectedSuplier
        {
            get
            {
                return _SelectedSuplier;
            }
            set
            {
                _SelectedSuplier = value;
                OnPropertyChanged();
            }
        }

        private string _QRCode;
        public string QRCode
        {
            get
            {
                return _QRCode;
            }
            set
            {
                _QRCode = value;
                OnPropertyChanged();
            }
        }

        private string _BarCode;
        public string BarCode
        {
            get
            {
                return _BarCode;
            }
            set
            {
                _BarCode = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }

        public ICommand EditCommand { get; set; }

        public ProductViewModel()
        {
            List = new ObservableCollection<Product>(DataProvider.dataProvider.db.Products);
            Unit = new ObservableCollection<Unit>(DataProvider.dataProvider.db.Units);
            Suplier = new ObservableCollection<Suplier>(DataProvider.dataProvider.db.Supliers);

            AddCommand = new RelayCommand<object>(
                (p) => { // Conditions
                    if (string.IsNullOrEmpty(DisplayName) || SelectedUnit == null || SelectedSuplier == null) return false;

                    return true;
                },
                (p) => {
                    var Product = new Product()
                    {
                        Id = Guid.NewGuid().ToString(),
                        DisplayName = this.DisplayName,
                        IdUnit = SelectedUnit.Id,
                        IdSuplier = SelectedSuplier.Id,
                        QRCode = this.QRCode,
                        BarCode = this.BarCode
                    };

                    DataProvider.dataProvider.db.Products.Add(Product);
                    DataProvider.dataProvider.db.SaveChanges(); // Save to DB

                    List.Add(Product);
                }
                );

            EditCommand = new RelayCommand<object>(
                (p) => { // Conditions
                    if (SelectedProductItem == null || SelectedUnit == null || SelectedSuplier == null || string.IsNullOrEmpty(DisplayName)) return false;

                    var displayList = DataProvider.dataProvider.db.Products.Where(Product => Product.Id == SelectedProductItem.Id);
                    if (displayList.Count() < 0) return false;

                    return true;
                },
                (p) => {
                    var Product = DataProvider.dataProvider.db.Products.Where(x => x.Id == SelectedProductItem.Id).SingleOrDefault();
                    Product.Id = this.Id;
                    Product.DisplayName = this.DisplayName;
                    Product.IdUnit = this.SelectedUnit.Id;
                    Product.IdSuplier = this.SelectedSuplier.Id;
                    Product.QRCode = this.QRCode;
                    Product.BarCode = this.BarCode;

                    DataProvider.dataProvider.db.SaveChanges(); // Save to DB
                }
                );
        }
    }
}
