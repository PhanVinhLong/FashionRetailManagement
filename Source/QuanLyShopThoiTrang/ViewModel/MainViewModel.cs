using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.View;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        private NhanVien nhanvien;
        private int _doanhSo;
        private int _DSTB;
        private int _doanhSoTrungBinh;
        private int _doanhSoMax;
        private String _doanhSoString;
        private String _chiTieuString;

        public int DoanhSo { get { return _doanhSo; } set { _doanhSo = value; OnPropertyChanged(); } }
        public int DSTB { get { return _DSTB; } set { _DSTB = value; OnPropertyChanged(); } }
        public int ChiTieu { get { return _doanhSoTrungBinh; } set { _doanhSoTrungBinh = value; OnPropertyChanged(); DoanhSoMax = (int)(1.3f * value); } }
        public int DoanhSoMax { get { return _doanhSoMax; } set { _doanhSoMax = value; } }
        public String DoanhSoString { get { return _doanhSoString; } set { _doanhSoString = value; OnPropertyChanged(); } }
        public String ChiTieuString { get { return _chiTieuString; } set { _chiTieuString = value; } }

        public ICommand OpenQuanLyKhachHangCommand { get; set; }
        public ICommand OpenQuanLyLoaiKhachHangCommand { get; set; }
        public ICommand OpenQuanLyNhanVienCommand { get; set; }
        public ICommand OpenQuanLyNhaCungCapCommand { get; set; }
        public ICommand OpenQuanLyKichCoCommand { get; set; }
        public ICommand OpenQuanLyMauSacCommand { get; set; }
        public ICommand OpenQuanLyLoaiSanPhamCommand { get; set; }
        public ICommand OpenQuanLySanPhamCommand { get; set; }
        public ICommand OpenBaoCaoNgayCommand { get; set; }
        public ICommand OpenBaoCaoThangCommand { get; set; }
        public ICommand OpenBaoCaoCommand { get; set; }
        public ICommand OpenLapHoaDonCommand { get; set; }
        public ICommand OpenQuanLyHoaDonCommand { get; set; }
        public ICommand OpenPhanQuyenCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand OpenQuanLyPhieuNhapCommand { get; set; }
        public ICommand XemTaiKhoanCommand { get; set; }
        public ICommand DoiMatKhauCommand { get; set; }
        public ICommand OpenTraHangCommand { get; set; }
        //public ICommand OpenTraHangCommand { get; set; }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        private List<HienThiHoaDon> _LackList;
        private List<HienThiHoaDon> _TopList;
        public List<HienThiHoaDon> LackList { get { return _LackList; } set { _LackList = value; OnPropertyChanged(); } }
        public List<HienThiHoaDon> TopList { get { return _TopList; } set { _TopList = value; OnPropertyChanged(); } }

        public MainViewModel()
        {

        }

        public MainViewModel(NhanVien NV)
        {

            LoadChartStatic();
            LoadChart2();
            LoadLackList();
            LoadTopList();
            nhanvien = NV;

            VaiTro vaitro = DataProvider.GetInstance.DB.VaiTroes.Where(x => x.IDVaiTro == NV.IDVaiTro).SingleOrDefault();
            GlobalVar.SetData(NV, vaitro);

            OpenPhanQuyenCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.QLVaiTro)
                {
                    PhanQuyenWindow window = new PhanQuyenWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            OpenLapHoaDonCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.LapHoaDon)
                {
                    LapHoaDonWindow window = new LapHoaDonWindow(NV);
                    window.ShowDialog();
                    LoadChartStatic();
                    LoadLackList();
                    LoadTopList();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            OpenQuanLyHoaDonCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.LapHoaDon)
                {
                    QuanLyHoaDonWindow window = new QuanLyHoaDonWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            OpenQuanLyKhachHangCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.QLKhachHang)
                {
                    QuanLyKhachHangWindow window = new QuanLyKhachHangWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            OpenQuanLyLoaiKhachHangCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.QLLoaiKhachHang)
                {
                    QuanLyLoaiKhachHangWindow window = new QuanLyLoaiKhachHangWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            OpenQuanLyNhanVienCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.QLNhanVien)
                {
                    QuanLyNhanVienWindow window = new QuanLyNhanVienWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            LogOutCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                var result = MessageBox.Show("Bạn có muốn đăng xuất?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {

                    DangNhapWindow window = new DangNhapWindow();
                    window.Show();
                    p.Close();


                }
            });

            OpenQuanLyNhaCungCapCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.QLNhaCungCap)
                {
                    QuanLyNhaCungCapWindow window = new QuanLyNhaCungCapWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            OpenQuanLyKichCoCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.QLSizeMau)
                {
                    QuanLyKichCoWindow window = new QuanLyKichCoWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            OpenQuanLyMauSacCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.QLSizeMau)
                {
                    QuanLyMauSacWindow window = new QuanLyMauSacWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            OpenQuanLyLoaiSanPhamCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.QLLoaiSanPham)
                {
                    QuanLyLoaiSanPhamWindow window = new QuanLyLoaiSanPhamWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            OpenQuanLySanPhamCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.QLSanPham)
                {
                    QuanLySanPhamWindow window = new QuanLySanPhamWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            OpenBaoCaoNgayCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.BaoCao)
                {
                    BaoCaoNgayWindow window = new BaoCaoNgayWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }
            });

            OpenBaoCaoThangCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                if (vaitro.BaoCao)
                {
                    BaoCaoThangWindow window = new BaoCaoThangWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }

            });

            OpenBaoCaoCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.BaoCao)
                {
                    BaoCaoWindow window = new BaoCaoWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }

            });

            OpenQuanLyPhieuNhapCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (vaitro.LapPhieuNhapHang)
                {
                    QuanLyPhieuNhapWindow window = new QuanLyPhieuNhapWindow(NV);
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập tính năng này");
                }

            });


            XemTaiKhoanCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //if (true)
                // {
                XemThongTinNhanVienWindow window = new XemThongTinNhanVienWindow(NV, false);
                window.ShowDialog();
                //}


            });

            DoiMatKhauCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //if (true)
                // {
                DoiMatKhauWindow window = new DoiMatKhauWindow(NV);
                window.ShowDialog();
                //}


            });

            OpenTraHangCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                TraHangWindow window = new TraHangWindow(NV);
                window.ShowDialog();
            });
        }

        public void LoadChartStatic()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            HoaDon[] hoaDons = DataProvider.GetInstance.DB.HoaDons.Where(x => x.NgayHoaDon.Month == month && x.NgayHoaDon.Year == year && x.NgayHoaDon.Day == day).ToArray();
            int doanhSoThang = 0;
            foreach (HoaDon hoaDon in hoaDons)
                doanhSoThang += (int)DataProvider.GetInstance.DB.ChiTietHoaDons.Where(x => x.IDHoaDon == hoaDon.IDHoaDon).Sum(x => x.SoLuong * x.DonGia);

            DoanhSo = doanhSoThang / 1000;
            ChiTieu = 50; // change this value
            DoanhSoString = "Doanh số trong ngày: " + DoanhSo + " triệu VND / " + ChiTieu.ToString() + "triệu VND Chỉ tiêu";
            ChiTieuString = "Chỉ tiêu: " + ChiTieu + " triệu VND";

            if (ChiTieu > DoanhSo)
                DSTB = ChiTieu / 10;
            else DSTB = DoanhSo / 10;
        }

        public double TinhDoanhThu(int d, int m, int y)
        {
            HoaDon[] hoaDons = DataProvider.GetInstance.DB.HoaDons.Where(x => x.NgayHoaDon.Month == d && x.NgayHoaDon.Year == y && x.NgayHoaDon.Day == m).ToArray();
            int doanhSo = 0;
            foreach (HoaDon hoaDon in hoaDons)
                doanhSo += (int)DataProvider.GetInstance.DB.ChiTietHoaDons.Where(x => x.IDHoaDon == hoaDon.IDHoaDon).Sum(x => x.SoLuong * x.DonGia);

            return (double)doanhSo;
        }

        public void LoadLackList()
        {
            List<SanPham> a = new List<SanPham>(DataProvider.GetInstance.DB.SanPhams);
            a = a.OrderBy(x => x.SoLuongTon).ToList();

            LackList = new List<HienThiHoaDon>();
            if (a.Count >= 3)
            {
                int i;
                for (i = 0; i < 3; i++)
                {
                    SanPham t1 = (SanPham)a.ElementAt(i);
                    HienThiHoaDon t = new HienThiHoaDon(
                        t1.IDSanPham,
                        DataProvider.GetInstance.DB.LoaiSanPhams.Where(x => x.IDLoaiSanPham == t1.IDLoaiSanPham).SingleOrDefault().TenLoaiSanPham,
                        "",
                        "",
                        0,
                        t1.SoLuongTon,
                        0
                        );

                    LackList.Add(t);
                }

                while (true)
                {
                    if (a.ElementAt(i).SoLuongTon == a.ElementAt(2).SoLuongTon)
                    {
                        SanPham t1 = (SanPham)a.ElementAt(i);
                        HienThiHoaDon t = new HienThiHoaDon(
                            t1.IDSanPham,
                            DataProvider.GetInstance.DB.LoaiSanPhams.Where(x => x.IDLoaiSanPham == t1.IDLoaiSanPham).SingleOrDefault().TenLoaiSanPham,
                            "",
                            "",
                            0,
                            t1.SoLuongTon,
                            0
                            );

                        LackList.Add(t);
                        i++;
                    }
                    else break;
                }

            }
            else
            {

            }

            //   a.RemoveAll();

        }

        public void LoadTopList()
        {
            List<HienThiHoaDon> temp = new List<HienThiHoaDon>();
            List<HoaDon> hd = new List<HoaDon>(DataProvider.GetInstance.DB.HoaDons.Where(x => x.NgayHoaDon.Month == DateTime.Now.Month &&
            x.NgayHoaDon.Year == DateTime.Now.Year));


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
                                0,
                                c.SoLuong,
                                0
                                );

                            temp.Add(t);
                        }
                    }
                }

                TopList = new List<HienThiHoaDon>();
                temp = temp.OrderByDescending(x => x.SoLuong).ToList();
                int i = 0;
                if (temp.Count != 0)
                {
                    if (temp.Count < 3)
                    {
                        for (i = 0; i < temp.Count; i++)
                        {
                            TopList.Add(temp.ElementAt(i));
                        }
                    }
                    else
                    {
                        for (i = 0; i < 3; i++)
                        {
                            TopList.Add(temp.ElementAt(i));
                        }
                        if (temp.Count >= 4)
                        {
                            while (true)
                            {
                                if (temp.ElementAt(i).SoLuong == temp.ElementAt(2).SoLuong)
                                {
                                    TopList.Add(temp.ElementAt(i));
                                    i++;
                                }
                                else break;
                            }
                        }
                    }
                }

            }
        }

        public void LoadChart2()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Doanh Thu",
                    Values = new ChartValues<double> {}
                }
            };

            DateTime start = (DateTime.Now).AddDays(-38);
            Labels = new string[7];
            for (int i = 0; i <= 6; i++)
            {
                SeriesCollection[0].Values.Add(TinhDoanhThu(start.Day, start.Month, start.Year));
                Labels[i] = start.Day.ToString() + "/" + start.Month.ToString();
                start = start.AddDays(1);
            }

            Formatter = value => value.ToString("N");
        }
    }
}
