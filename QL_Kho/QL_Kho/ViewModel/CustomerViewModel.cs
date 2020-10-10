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
    public class CustomerViewModel:BaseViewModel
    {
        private ObservableCollection<Customer> _List;
        public ObservableCollection<Customer> List
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

        private Customer _SelectedCustomerItem;
        public Customer SelectedCustomerItem
        {
            get
            {
                return _SelectedCustomerItem;
            }
            set
            {
                _SelectedCustomerItem = value;
                OnPropertyChanged();
                if (SelectedCustomerItem != null)
                {
                    DisplayName = SelectedCustomerItem.DisplayName;
                    Address = SelectedCustomerItem.Address;
                    Phone = SelectedCustomerItem.Phone;
                    Email = SelectedCustomerItem.Email;
                    ContractDate = SelectedCustomerItem.ContractDate;
                    MoreInfo = SelectedCustomerItem.MoreInfo;
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

        public CustomerViewModel()
        {
            List = new ObservableCollection<Customer>(DataProvider.dataProvider.db.Customers);

            AddCommand = new RelayCommand<object>(
                (p) => { // Conditions
                    if (string.IsNullOrEmpty(DisplayName)) return false;

                    return true;
                },
                (p) => {
                    var Customer = new Customer()
                    {
                        DisplayName = this.DisplayName,
                        Address = this.Address,
                        Phone = this.Phone,
                        Email = this.Email,
                        ContractDate = this.ContractDate,
                        MoreInfo = this.MoreInfo
                    };
                    DataProvider.dataProvider.db.Customers.Add(Customer);
                    DataProvider.dataProvider.db.SaveChanges(); // Save to DB

                    List.Add(Customer);
                }
                );

            EditCommand = new RelayCommand<object>(
                (p) => { // Conditions
                    if (SelectedCustomerItem == null || string.IsNullOrEmpty(DisplayName)) return false;

                    var displayList = DataProvider.dataProvider.db.Customers.Where(Customer => Customer.Id == SelectedCustomerItem.Id);
                    if (displayList.Count() < 0) return false;

                    return true;
                },
                (p) => {
                    var Customer = DataProvider.dataProvider.db.Customers.Where(x => x.Id == SelectedCustomerItem.Id).SingleOrDefault();
                    Customer.DisplayName = this.DisplayName;
                    Customer.Address = this.Address;
                    Customer.Phone = this.Phone;
                    Customer.Email = this.Email;
                    Customer.ContractDate = this.ContractDate;
                    Customer.MoreInfo = this.MoreInfo;

                    DataProvider.dataProvider.db.SaveChanges(); // Save to DB
                }
                );
        }
    }
}
