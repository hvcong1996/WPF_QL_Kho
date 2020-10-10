using QL_Kho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace QL_Kho.ViewModel
{
    public class UnitViewModel:BaseViewModel
    {
        private ObservableCollection<Unit> _List;
        public ObservableCollection<Unit> List
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

        private Unit _SelectedUnitItem;
        public Unit SelectedUnitItem
        { 
            get
            {
                return _SelectedUnitItem;
            }
            set
            {
                _SelectedUnitItem = value;
                OnPropertyChanged();
                if (SelectedUnitItem != null)
                {
                    DisplayName = SelectedUnitItem.DisplayName;
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

        public ICommand AddCommand { get; set; }

        public ICommand EditCommand { get; set; }

        public UnitViewModel()
        {
            List = new ObservableCollection<Unit>(DataProvider.dataProvider.db.Units);

            AddCommand = new RelayCommand<object>(
                (p) => { // Conditions
                    if (string.IsNullOrEmpty(DisplayName)) return false;

                    return true;
                },
                (p) => {
                    var unit = new Unit() { DisplayName = this.DisplayName };
                    DataProvider.dataProvider.db.Units.Add(unit);
                    DataProvider.dataProvider.db.SaveChanges(); // Save to DB

                    List.Add(unit);
                }
                );

            EditCommand = new RelayCommand<object>(
                (p) => { // Conditions
                    if (SelectedUnitItem == null || string.IsNullOrEmpty(DisplayName)) return false;

                    var displayList = DataProvider.dataProvider.db.Supliers.Where(unit => unit.Id == SelectedUnitItem.Id);
                    if (displayList.Count() < 0) return false;

                    return true;
                },
                (p) => {
                    var unit = DataProvider.dataProvider.db.Units.Where(x => x.Id == SelectedUnitItem.Id).SingleOrDefault();
                    unit.DisplayName = this.DisplayName;
                    DataProvider.dataProvider.db.SaveChanges(); // Save to DB
                }
                );
        }
    }
}
