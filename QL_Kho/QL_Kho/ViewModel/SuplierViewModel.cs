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
    public class SuplierViewModel:BaseViewModel
    {
        private ObservableCollection<Suplier> _List;
        public ObservableCollection<Suplier> List
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

        private Suplier _SelectedSuplierItem;
        public Suplier SelectedSuplierItem
        {
            get
            {
                return _SelectedSuplierItem;
            }
            set
            {
                _SelectedSuplierItem = value;
                OnPropertyChanged();
                if (SelectedSuplierItem != null)
                {
                    DisplayName = SelectedSuplierItem.DisplayName;
                    Address = SelectedSuplierItem.Address;
                    Phone = SelectedSuplierItem.Phone;
                    Email = SelectedSuplierItem.Email;
                    ContractDate = SelectedSuplierItem.ContractDate;
                    MoreInfo = SelectedSuplierItem.MoreInfo;
                }
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

        private string _Address;
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
                OnPropertyChanged();
            }
        }

        private string _Phone;
        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
                OnPropertyChanged();
            }
        }

        private string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
        }

        private Nullable<System.DateTime> _ContractDate;
        public Nullable<System.DateTime> ContractDate
        {
            get
            {
                return _ContractDate;
            }
            set
            {
                _ContractDate = value;
                OnPropertyChanged();
            }
        }

        private string _MoreInfo;
        public string MoreInfo
        {
            get
            {
                return _MoreInfo;
            }
            set
            {
                _MoreInfo = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }

        public ICommand EditCommand { get; set; }

        public SuplierViewModel()
        {
            List = new ObservableCollection<Suplier>(DataProvider.dataProvider.db.Supliers);

            AddCommand = new RelayCommand<object>(
                (p) => { // Conditions
                    if (string.IsNullOrEmpty(DisplayName)) return false;

                    return true;
                },
                (p) => {
                    var Suplier = new Suplier() 
                    { 
                        DisplayName = this.DisplayName,
                        Address = this.Address,
                        Phone = this.Phone,
                        Email = this.Email,
                        ContractDate = this.ContractDate,
                        MoreInfo = this.MoreInfo
                    };
                    DataProvider.dataProvider.db.Supliers.Add(Suplier);
                    DataProvider.dataProvider.db.SaveChanges(); // Save to DB

                    List.Add(Suplier);
                }
                );

            EditCommand = new RelayCommand<object>(
                (p) => { // Conditions
                    if (SelectedSuplierItem == null || string.IsNullOrEmpty(DisplayName)) return false;

                    var displayList = DataProvider.dataProvider.db.Supliers.Where(Suplier => Suplier.Id == SelectedSuplierItem.Id);
                    if (displayList.Count() < 0) return false;

                    return true;
                },
                (p) => {
                    var Suplier = DataProvider.dataProvider.db.Supliers.Where(x => x.Id == SelectedSuplierItem.Id).SingleOrDefault();
                    Suplier.DisplayName = this.DisplayName;
                    Suplier.Address = this.Address;
                    Suplier.Phone = this.Phone;
                    Suplier.Email = this.Email;
                    Suplier.ContractDate = this.ContractDate;
                    Suplier.MoreInfo = this.MoreInfo;

                    DataProvider.dataProvider.db.SaveChanges(); // Save to DB
                }
                );
        }
    }
}
