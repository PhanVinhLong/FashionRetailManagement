using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyShopThoiTrang.Model
{
    public class HienThiBaoCaoThang
    {
        public int IDHoaDon { get; set; }
        public string KhachHang { get; set; }
        public string NhanVien { get; set; }
        public int SoSanPham { get; set; }
        public double GiamGia { get; set; }
        public double TongTien { get; set; }
        public DateTime NgayHoaDon { get; set; }
    }
}
