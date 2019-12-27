using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using QuanLyShopThoiTrang.Model;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class BaoCaoTongQuanViewModel: BaseViewModel
    {
        private List<HienThiHoaDon> _ListSanPham;
        public List<HienThiHoaDon> ListSanPham { get => _ListSanPham; set { _ListSanPham = value; OnPropertyChanged(); } }
        public SeriesCollection DataDoanhThu { get; set; } = new SeriesCollection { new LineSeries { } };
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private DateTime _StartDate;
        public DateTime StartDate { get => _StartDate; set { _StartDate = value; OnPropertyChanged(); } }

        private DateTime _EndDate;
        public DateTime EndDate { get => _EndDate; set { _EndDate = value; OnPropertyChanged(); } }

        public ICommand LoadBaoCao { get; set; }

        public BaoCaoTongQuanViewModel()
        {
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;

            LoadBaoCao = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadDoanhThuSanPham();
                LoadDoanhThu();
            });

            LoadDoanhThuSanPham();
            LoadDoanhThu();
        }

        void LoadDoanhThu()
        {
            List<double> temp = new List<double> { };

            for(DateTime dt = StartDate; dt <= EndDate; dt = dt.AddDays(1))
            {
                double tong = 0;
                List<HoaDon> hd = new List<HoaDon>(DataProvider.GetInstance.DB.HoaDons.Where(x => x.NgayHoaDon.Day == dt.Day && x.NgayHoaDon.Month == dt.Month && x.NgayHoaDon.Year == dt.Year));
                if (hd.Count != 0)
                {
                    foreach (HoaDon h in hd)
                    {
                        List<ChiTietHoaDon> ct = new List<ChiTietHoaDon>(DataProvider.GetInstance.DB.ChiTietHoaDons.Where(x => x.IDHoaDon == h.IDHoaDon));
                        foreach (ChiTietHoaDon c in ct)
                        {
                            tong = c.SoLuong * c.SanPham.DonGia * 1000;
                        }
                    }
                }
                temp.Add(tong);
            }

            try
            {
                DataDoanhThu[0] =
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double>(temp),
                    LineSmoothness = 0,
                    PointGeometry = null
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        void LoadDoanhThuSanPham()
        {
            List<HienThiHoaDon> temp = new List<HienThiHoaDon>();
            List<HoaDon> hd = new List<HoaDon>(DataProvider.GetInstance.DB.HoaDons.Where(x => x.NgayHoaDon >= StartDate && x.NgayHoaDon <= EndDate));

            if (hd.Count != 0)
            {
                foreach (HoaDon h in hd)
                {
                    List<ChiTietHoaDon> ct = new List<ChiTietHoaDon>(DataProvider.GetInstance.DB.ChiTietHoaDons.Where(x => x.IDHoaDon == h.IDHoaDon));

                    foreach (ChiTietHoaDon c in ct)
                    {
                        bool Exist = false;
                        foreach (HienThiHoaDon hthd in temp)
                        {

                            if (hthd.IDSanPham == c.IDSanPham)
                            {
                                hthd.SoLuong += c.SoLuong;
                                hthd.ThanhTien += c.SoLuong * c.DonGia;
                                Exist = true;
                                break;
                            }
                        }

                        if (!Exist)
                        {
                            SanPham t1 = DataProvider.GetInstance.DB.SanPhams.Where(x => x.IDSanPham == c.IDSanPham).SingleOrDefault();
                            HienThiHoaDon t = new HienThiHoaDon(
                                t1.IDSanPham,
                                DataProvider.GetInstance.DB.LoaiSanPhams.Where(x => x.IDLoaiSanPham == t1.IDLoaiSanPham).SingleOrDefault().TenLoaiSanPham,
                                "",
                                "",
                                t1.DonGia,
                                c.SoLuong,
                                t1.DonGia * c.SoLuong
                                );

                            temp.Add(t);
                        }
                    }
                }
            }
            ListSanPham = new List<HienThiHoaDon>(temp);
        }
    }
}
