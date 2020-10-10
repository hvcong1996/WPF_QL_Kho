using QL_Kho.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QL_Kho.ViewModel
{
    class LoginViewModel:BaseViewModel
    {
        public bool IsLogin { get; set; }
        private string _UserName = "";
        public string UserName {
            get { 
                return _UserName;
            }
            set { 
                _UserName = value;
                OnPropertyChanged();
            } 
        }
        private string _Password = "";
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


        public ICommand LoginCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, 
                (p) => {
                    Login(p);
                });
            CloseCommand = new RelayCommand<Window>((p) => { return true; },
                (p) => {
                    p.Close();
                });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; },
                (p) => {
                    Password = p.Password;
                });
        }

        void Login(Window p)
        {
            if (p == null) return;

            this.Password = MD5Hash(EncodeTo64(this.Password));
            var account = DataProvider.dataProvider.db.Accounts.Where(item => item.UserName == this.UserName && item.Password == this.Password).Count();
            if(account > 0)
            {
                IsLogin = true;
                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Login fail");
            }
        }

        private string EncodeTo64(string password)
        {
            byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(password);
            return Convert.ToBase64String(toEncodeAsBytes);
        }

        private string MD5Hash(string password)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(password));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
