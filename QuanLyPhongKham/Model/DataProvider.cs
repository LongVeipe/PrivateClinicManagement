using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Model
{
    class DataProvider
    {
        private static DataProvider _instant;
        public static DataProvider Instant {
            get
            {
                if (_instant == null)
                    _instant = new DataProvider();
                return _instant;
            } 
            set
            {
                _instant = value;
            }

        }
        public QuanLyPhongKhamTuEntities DB { get; set; }
        private DataProvider ()
        {
            DB = new QuanLyPhongKhamTuEntities();
        }
    }
}
