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
    public class UserViewModel:BaseViewModel
    {
        private ObservableCollection<Account> _List;
        public ObservableCollection<Account> List
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

        private Account _SelectedUserItem;
        public Account SelectedUserItem
        {
            get
            {
                return _SelectedUserItem;
            }
            set
            {
                _SelectedUserItem = value;
                OnPropertyChanged();
                if (SelectedUserItem != null)
                {
                    DisplayName = SelectedUserItem.DisplayName;
                    UserName = SelectedUserItem.UserName;
                    Password = SelectedUserItem.Password;
                    SelectedAccountRole = SelectedUserItem.AccountRole;
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

        private string _UserName;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }

        private string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<AccountRole> _AccountRole;
        public ObservableCollection<AccountRole> AccountRole
        {
            get
            {
                return _AccountRole;
            }
            set
            {
                _AccountRole = value;
                OnPropertyChanged();
            }
        }

        private AccountRole _SelectedAccountRole;
        public AccountRole SelectedAccountRole
        {
            get
            {
                return _SelectedAccountRole;
            }
            set
            {
                _SelectedAccountRole = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }

        public ICommand EditCommand { get; set; }

        public UserViewModel()
        {
            List = new ObservableCollection<Account>(DataProvider.dataProvider.db.Accounts);
            AccountRole = new ObservableCollection<AccountRole>(DataProvider.dataProvider.db.AccountRoles);

            AddCommand = new RelayCommand<object>(
                (p) => { // Conditions
                    if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || SelectedAccountRole == null) return false;

                    var displayList = DataProvider.dataProvider.db.Accounts.Where(Account => Account.UserName == UserName);
                    if (displayList.Count() > 0) return false;

                    return true;
                },
                (p) => {
                    var Account = new Account() 
                    {
                        DisplayName = this.DisplayName,
                        UserName = this.UserName,
                        Password= this.Password,
                        IdRole = SelectedAccountRole.Id
                    };
                    DataProvider.dataProvider.db.Accounts.Add(Account);
                    DataProvider.dataProvider.db.SaveChanges(); // Save to DB

                    List.Add(Account);
                }
                );

            EditCommand = new RelayCommand<object>(
                (p) => { // Conditions
                    if (SelectedUserItem == null || SelectedAccountRole == null || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password)) return false;

                    var displayList = DataProvider.dataProvider.db.Accounts.Where(Account => Account.Id == SelectedUserItem.Id);
                    if (displayList.Count() < 0) return false;

                    return true;
                },
                (p) => {
                    var Account = DataProvider.dataProvider.db.Accounts.Where(x => x.Id == SelectedUserItem.Id).SingleOrDefault();
                    Account.DisplayName = this.DisplayName;
                    Account.UserName = this.UserName;
                    Account.Password = this.Password;
                    Account.IdRole = this.SelectedAccountRole.Id;
                    DataProvider.dataProvider.db.SaveChanges(); // Save to DB
                }
                );
        }
    }
}
