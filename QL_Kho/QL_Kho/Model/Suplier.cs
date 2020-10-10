//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QL_Kho.Model
{
    using QL_Kho.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class Suplier:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Suplier()
        {
            this.Products = new HashSet<Product>();
        }

        private int _Id;
        public int Id
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
