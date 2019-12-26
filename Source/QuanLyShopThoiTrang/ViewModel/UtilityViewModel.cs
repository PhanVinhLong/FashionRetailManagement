using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyShopThoiTrang.Model;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class GlobalVar
    {
        public static NhanVien nhanVien { get; set;  }
        public static VaiTro vaiTro { get; set; }

        private GlobalVar()
        {

        }

        private static GlobalVar instance;
        public static GlobalVar GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new GlobalVar();
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public static void SetData(NhanVien nv, VaiTro vt)
        {
            nhanVien = nv;
            vaiTro = vt;
        }

    }
    class UtilityViewModel
    {

    }
}
