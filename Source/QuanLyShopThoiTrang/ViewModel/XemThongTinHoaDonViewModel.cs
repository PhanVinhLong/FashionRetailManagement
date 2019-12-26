using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuanLyShopThoiTrang.Model;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class XemThongTinHoaDonViewModel:BaseViewModel
    {

        private HoaDon _hoadon;
        public HoaDon hd { get => _hoadon; set { _hoadon = value; OnPropertyChanged(); } }

        private double _TongTien;
        public double TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

        private string _HoTenNhanVien;
        public string HoTenNhanVien { get => _HoTenNhanVien; set { _HoTenNhanVien = value; OnPropertyChanged(); } }

        private string _HoTenKhachHang;
        public string HoTenKhachHang { get => _HoTenKhachHang; set { _HoTenKhachHang = value; OnPropertyChanged(); } }

        private double _GiamGia;
        public double GiamGia { get => _GiamGia; set { _GiamGia = value; OnPropertyChanged(); } }

        private ObservableCollection<ChiTietHoaDon> chitiet;

        private ObservableCollection<HienThiHoaDon> _List;
        public ObservableCollection<HienThiHoaDon> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        public XemThongTinHoaDonViewModel (HoaDon hoadon)
        {
            hd = hoadon;
            TongTien = 0;
            chitiet = new ObservableCollection<ChiTietHoaDon>(DataProvider.GetInstance.DB.ChiTietHoaDons.Where(x => x.IDHoaDon == hd.IDHoaDon));
            List = new ObservableCollection<HienThiHoaDon>();


            foreach (ChiTietHoaDon ct in chitiet)
            {
                HienThiHoaDon A = new HienThiHoaDon();

                A.IDSanPham = ct.IDSanPham;

                var sp = DataProvider.GetInstance.DB.SanPhams.Where(x => x.IDSanPham == ct.IDSanPham).SingleOrDefault();

                A.TenSanPham = DataProvider.GetInstance.DB.LoaiSanPhams.Where(x => x.IDLoaiSanPham == sp.IDLoaiSanPham).SingleOrDefault().TenLoaiSanPham;
                A.MauSac = DataProvider.GetInstance.DB.MauSacs.Where(x => x.IDMauSac == sp.IDMauSac).SingleOrDefault().TenMauSac;
                A.Size = DataProvider.GetInstance.DB.KichCoes.Where(x => x.IDKichCo == sp.IDKichCo).SingleOrDefault().TenKichCo;
                A.SoLuong = ct.SoLuong;
                A.DonGia = ct.DonGia * 1000;
                A.ThanhTien = A.DonGia * A.SoLuong;
                List.Add(A);

                TongTien += A.ThanhTien;
            }

            GiamGia = hd.GiamGia * 1000;
            TongTien -= GiamGia;

            HoTenKhachHang = DataProvider.GetInstance.DB.KhachHangs.Where(x => x.IDKhachHang == hd.IDKhachHang).SingleOrDefault().HoTen;
            HoTenNhanVien = DataProvider.GetInstance.DB.NhanViens.Where(x => x.IDNhanVien == hd.IDNhanVien).SingleOrDefault().HoTen;


            //MessageBox.Show(hd.IDHoaDon.ToString());
        }
    }
}
