using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyShopThoiTrang.Model
{
    class DataProvider
    {

        private static DataProvider instance;
        public static DataProvider GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public QLShopEntities DB { get; set; }

        private DataProvider()
        {
            DB = new QLShopEntities();
        }
    }
}
