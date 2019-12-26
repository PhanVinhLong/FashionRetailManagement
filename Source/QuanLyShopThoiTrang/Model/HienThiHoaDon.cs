using QuanLyShopThoiTrang.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyShopThoiTrang.Model
{
    public class HienThiHoaDon: BaseViewModel
    {
        private int _IDSanPham;
        public int IDSanPham { get => _IDSanPham; set { _IDSanPham = value; OnPropertyChanged(); } }

        private string _TenSanPham;
        public string TenSanPham { get => _TenSanPham; set { _TenSanPham = value; OnPropertyChanged(); } }

        private string _MauSac;
        public string MauSac { get => _MauSac; set { _MauSac = value; OnPropertyChanged(); } }

        private string _Size;
        public string Size { get => _Size; set { _Size = value; OnPropertyChanged(); } }


        private double _DonGia;
        public double DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }

        private int _SoLuong;
        public int SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }

        private double _ThanhTien;
        public double ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }

        public HienThiHoaDon() { }
        public HienThiHoaDon(int id, string name, string color, string size, double price, int number, double total)
        {
            IDSanPham = id;
            TenSanPham = name;
            MauSac = color;
            Size = size;
            DonGia = price;
            SoLuong = number;
            ThanhTien = total;
        }
    }
}
