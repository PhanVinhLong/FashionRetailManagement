using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Core;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class XuatHoaDonViewModel : BaseViewModel
    {

        private int _IDNhanVien;
        public int IDNhanVien { get => _IDNhanVien; set { _IDNhanVien = value; OnPropertyChanged(); } }

        private double _TongHoaDon;
        public double TongHoaDon { get => _TongHoaDon; set { _TongHoaDon = value; OnPropertyChanged(); } }

        private ObservableCollection<HienThiHoaDon> _List = new ObservableCollection<HienThiHoaDon>();
        public ObservableCollection<HienThiHoaDon> ListHD { get => _List; set { _List = value; OnPropertyChanged(); } }

        private int _SoSanPham;
        public int SoSanPham
        {
            get => _SoSanPham;
            set
            {
                _SoSanPham = value;
                OnPropertyChanged();
            }
        }

        private int _SoHang;
        public int SoHang
        {
            get => _SoHang;
            set
            {
                _SoHang = value;
                OnPropertyChanged();
            }
        }

        private double TongTichLuy;


        private double _MucGiamGia;
        public double MucGiamGia
        {
            get => _MucGiamGia;
            set
            {
                _MucGiamGia = value;
                OnPropertyChanged();
            }
        }

        private double _TienGiam;
        public double TienGiam
        {
            get => _TienGiam;
            set
            {
                _TienGiam = value;
                OnPropertyChanged();
            }
        }

        private int _IDKhachHang;
        public int IDKhachHang
        {
            get => _IDKhachHang;
            set
            {
                _IDKhachHang = value;
                OnPropertyChanged();
                if (IDKhachHang > 1000)
                {
                    var customer = DataProvider.GetInstance.DB.KhachHangs.Where(x => x.IDKhachHang == IDKhachHang).SingleOrDefault();

                    if (customer != null)
                    {
                        TenKhachHang = customer.HoTen;
                        TongTichLuy = customer.TongTienTichLuy;

                        foreach (LoaiKhachHang i in _LoaiKhachHang)
                        {
                            if (TongTichLuy > i.TichLuyToiThieu)
                            {
                                MucGiamGia = i.MucGiamGia;
                                break;
                            }
                        }
                        CapNhapGiamGiaVaTongHoaDon();
                    }
                    else
                    {
                        TenKhachHang = "";
                        TongTichLuy = 0;
                        MucGiamGia = 0;
                        CapNhapGiamGiaVaTongHoaDon();
                    }

                }
                else
                {
                    TenKhachHang = "";

                    TongTichLuy = 0;
                    MucGiamGia = 0;
                    CapNhapGiamGiaVaTongHoaDon();
                }
            }
        }

        private string _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }

        private int _TienKhachDua;
        public int TienKhachDua
        {
            get => _TienKhachDua;
            set
            {
                _TienKhachDua = value;
                OnPropertyChanged();
            }
        }

        private double _KhachPhaiTra;
        public double KhachPhaiTra { get => _KhachPhaiTra; set { _KhachPhaiTra = value; OnPropertyChanged(); } }

        private bool _Ispatron;
        public bool Ispatron { get => _Ispatron; set { _Ispatron = value; OnPropertyChanged(); } }

        private ObservableCollection<LoaiKhachHang> _LoaiKhachHang;

        private DateTime _NgayHoaDon;
        public DateTime NgayHoaDon { get => _NgayHoaDon; set { _NgayHoaDon = value; OnPropertyChanged(); } }

        private int _IDHoaDon;
        public int IDHoaDon { get => _IDHoaDon; set { _IDHoaDon = value; OnPropertyChanged(); } }

        private bool _returnValue;
        public bool returnValue { get => _returnValue; set { _returnValue = value; OnPropertyChanged(); } }

        public ICommand LostFocus1 { get; set; }
        public ICommand ThemKH { get; set; }
        public ICommand LuuHoaDon { get; set; }

        public XuatHoaDonViewModel(int IDNV,
            double Tong, int SoSp, int SH, ObservableCollection<HienThiHoaDon> L, DateTime NgayHD)
        {
            _LoaiKhachHang = new ObservableCollection<LoaiKhachHang>(DataProvider.GetInstance.DB.LoaiKhachHangs);
            _LoaiKhachHang = new ObservableCollection<LoaiKhachHang>(_LoaiKhachHang.OrderBy(x => x.TichLuyToiThieu).ToList());
            ListHD = L;
            NgayHoaDon = NgayHD;
            IDNhanVien = IDNV;
            TongHoaDon = Tong;
            SoHang = SH;
            Ispatron = false;
            SoSanPham = SoSp;
            CapNhapGiamGiaVaTongHoaDon();
            TaoIDHoaDon();
            returnValue = false;
            TenKhachHang = "";


            LostFocus1 = new RelayCommand<Window>((p) => { return true; },
           (p) =>
           {
               if (TenKhachHang == "")
               {
                   IDKhachHang = 0;
                   Ispatron = false;
                   CapNhapGiamGiaVaTongHoaDon();
               }
               else
                   Ispatron = true;

           });




            LuuHoaDon = new RelayCommand<Window>((p) => { return true; },
               (p) =>
               {
                   if (TenKhachHang == "")
                       Ispatron = false;
                   if (TienKhachDua < KhachPhaiTra)
                   {
                       DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Khách chưa đưa đủ tiền", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                   }
                   else
                   {
                       var hoadon = new HoaDon();
                       hoadon.IDNhanVien = IDNhanVien;
                       TaoIDHoaDon();
                       hoadon.IDHoaDon = IDHoaDon;
                       hoadon.NgayHoaDon = DateTime.Now;
                       if (!Ispatron)
                       {
                           hoadon.IDKhachHang = 1000;
                           hoadon.GiamGia = 0;
                       }
                       else
                       {
                           hoadon.IDKhachHang = IDKhachHang;
                           hoadon.GiamGia = TienGiam / 1000;
                           var Customer = DataProvider.GetInstance.DB.KhachHangs.Where(x => x.IDKhachHang == hoadon.IDKhachHang).SingleOrDefault();
                           Customer.TongTienTichLuy += KhachPhaiTra / 1000;
                           DataProvider.GetInstance.DB.SaveChanges();

                       }

                       DataProvider.GetInstance.DB.HoaDons.Add(hoadon);
                       DataProvider.GetInstance.DB.SaveChanges();


                       foreach (HienThiHoaDon i in ListHD)
                       {
                           var CTHD = new ChiTietHoaDon()
                           {
                               IDSanPham = i.IDSanPham,
                               IDHoaDon = IDHoaDon,
                               SoLuong = i.SoLuong,
                               DonGia = i.DonGia / 1000,
                           };

                           var product = DataProvider.GetInstance.DB.SanPhams.Where(x => x.IDSanPham == i.IDSanPham).SingleOrDefault();
                           product.SoLuongTon -= i.SoLuong;
                           DataProvider.GetInstance.DB.SaveChanges();

                           DataProvider.GetInstance.DB.ChiTietHoaDons.Add(CTHD);
                           DataProvider.GetInstance.DB.SaveChanges();

                       }

                       string thongbao = "Đã thanh toán. Tiền thừa " + (TienKhachDua - TongHoaDon).ToString();

                       DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: thongbao, button: MessageBoxButton.OK, icon: MessageBoxImage.Information);

                       returnValue = true;

                       ////Window pParent = ()p.Parent;
                       ///
                       // Hỏi in hoá đơn
                       if(DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "In hoá đơn?", button: MessageBoxButton.YesNo, icon: MessageBoxImage.Question)==MessageBoxResult.Yes)
                       {
                           DocumentViewer dv = new DocumentViewer(hoadon);
                           dv.ShowDialog();
                       }

                       // ((MainWindow)((Window)p.Owner)).Load();
                       p.Close();
                   }
               }

            );

            ThemKH = new RelayCommand<Window>((p) => { return true; },
            (p) =>
            {

                ThemKhachHangWindow wd = new ThemKhachHangWindow();
                wd.ShowDialog();
            });
        }

        public void TaoIDHoaDon()
        {
            List<HoaDon> l = DataProvider.GetInstance.DB.HoaDons.ToList();
            IDHoaDon = l.Count() + 10000000;

        }

        public void CapNhapGiamGiaVaTongHoaDon()
        {
            TienGiam = 0;
            TongHoaDon = 0;
            if (ListHD != null)
            {
                foreach (HienThiHoaDon i in ListHD)
                {
                    TongHoaDon += i.ThanhTien;
                }

                TienGiam = MucGiamGia * TongHoaDon;
                KhachPhaiTra = TongHoaDon - TienGiam;


            }

        }
    }
}
