using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Kho.Model
{
    public class DataProvider
    {
        private static DataProvider _dataProvider;
        public static DataProvider dataProvider {
            get {
                if (_dataProvider == null)
                {
                    _dataProvider = new DataProvider();
                }
                return _dataProvider;
            }
            set {
                _dataProvider = value;
            }
        }
        public QuanLyKhoEntities db { get; set; }
        public DataProvider()
        {
            db = new QuanLyKhoEntities();
        }
    }
}
