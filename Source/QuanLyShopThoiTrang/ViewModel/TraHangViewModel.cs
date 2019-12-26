using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Globalization;



namespace QuanLyShopThoiTrang.ViewModel
{
    public class TraHangViewModel : BaseViewModel
    {
        private int _Maphieu;
        public int MaPhieu { get { return _Maphieu; } set { _Maphieu = value; OnPropertyChanged(); } }

        public QLShopEntities Database = DataProvider.GetInstance.DB;
        private ObservableCollection<DisplayItemInfo> cthds = new ObservableCollection<DisplayItemInfo>();
        public ObservableCollection<DisplayItemInfo> CTHDs
        {
            get { return cthds; }
            set { cthds = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DisplayItemInfo> danhSachTra = new ObservableCollection<DisplayItemInfo>();
        public ObservableCollection<DisplayItemInfo> DanhSachTra
        {
            get { return danhSachTra; }
            set { danhSachTra = value; OnPropertyChanged(); }
        }

        private DisplayItemInfo selectedItem;
        public DisplayItemInfo SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        private DisplayItemInfo selectedItemTra;
        public DisplayItemInfo SelectedItemTra
        {
            get { return selectedItemTra; }
            set { selectedItemTra = value; OnPropertyChanged(); }
        }

        private String input;
        public String Input
        {
            get { return input; }
            set { input = value; OnPropertyChanged(); }
        }


        private HoaDon hoaDon;
        public HoaDon HoaDonTra
        {
            get { return hoaDon; }
            set { hoaDon = value; OnPropertyChanged(); }
        }


        private String ngayBan;
        public String NgayBan
        {
            get { return ngayBan; }
            set { ngayBan = value; OnPropertyChanged(); }
        }

        private KhachHang khachHang;
        public KhachHang KhachHang
        {
            get { return khachHang; }
            set { khachHang = value; OnPropertyChanged(); }
        }

        private NhanVien nhanVien;
        public NhanVien NhanVien
        {
            get { return nhanVien; }
            set { nhanVien = value; OnPropertyChanged(); }
        }

        private double tongTienCu;
        public double TongTienCu
        {
            get { return tongTienCu; } 
            set { tongTienCu = value; OnPropertyChanged(); OnPropertyChanged("DisplayTongTienCu"); }
        }

        private double tongTienTra;
        public double TongTienTra
        {
            get { return tongTienTra; }
            set { tongTienTra = value; OnPropertyChanged(); OnPropertyChanged("DisplayTongTienTra"); }
        }

        public String DisplayTongTienTra
        {
            get { return (tongTienTra * 1000).ToString("C", CultureInfo.CreateSpecificCulture("vi-VN")); }

        }

        public String DisplayTongTienCu
        {
            get { return (tongTienCu * 1000).ToString("C", CultureInfo.CreateSpecificCulture("vi-VN")); }
        }


        public ICommand TraCuuCommand { get; set; }
        public ICommand TraSanPhamCommand { get; set; }
        public ICommand LuuPhieuCommand { get; set; }
        public ICommand HoanTacCommand { get; set; }

        public TraHangViewModel()
        {

        }

        public TraHangViewModel(NhanVien nhanVien)
        {
            // Load data
            LoadData(nhanVien);
            SetupCommand(nhanVien);
        }

        public void LoadData(NhanVien nhanVien)
        {
            NhanVien = nhanVien;
            MaPhieu = TaoID();
        }

        public void SetupCommand(NhanVien nhanVien)
        {
            TraSanPhamCommand = new RelayCommand<object>(p => true, p =>
            {
                if (SelectedItem == null)
                { MessageBox.Show("Chọn một sản phẩm để trả hàng", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error); return; }

                if (SelectedItem.SoLuong == 1)
                {
                    ThemVaoDSTra(SelectedItem);
                    CTHDs.Remove(SelectedItem);
                }

                else if (SelectedItem.SoLuong > 1)
                {
                    var newItem = new DisplayItemInfo(SelectedItem);
                    newItem.SoLuong--;
                    CTHDs[CTHDs.IndexOf(SelectedItem)] = newItem;
                    SelectedItem = newItem;

                    ThemVaoDSTra(new DisplayItemInfo(SelectedItem));
                }
            });

            HoanTacCommand = new RelayCommand<object>(p => true, p =>
            {
                if (SelectedItemTra == null) {
                    MessageBox.Show("Chọn một sản phẩm để hoàn tác", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (SelectedItemTra.SoLuong == 1)
                {
                    ThemVaoCTHD(SelectedItemTra);
                    DanhSachTra.Remove(SelectedItemTra);
                }

                else if (SelectedItemTra.SoLuong > 1)
                {
                    var newItem = new DisplayItemInfo(SelectedItemTra);
                    newItem.SoLuong--;
                    DanhSachTra[DanhSachTra.IndexOf(SelectedItemTra)] = newItem;
                    SelectedItemTra = newItem;

                    ThemVaoCTHD(new DisplayItemInfo(SelectedItemTra));
                }
            });

            TraCuuCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                DanhSachTra = new ObservableCollection<DisplayItemInfo>();
                TongTienTra = 0;

                HoaDonTra = Database.HoaDons.Where(hd => hd.IDHoaDon.ToString() == Input).SingleOrDefault();

                if (HoaDonTra == null)
                {
                    NgayBan = null;
                    KhachHang = null;
                    TongTienCu = 0;
                    TongTienTra = 0;
                    MessageBox.Show("Không tìm thấy hóa đơn theo ID đã nhập", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (Database.PhieuTras.Where(pt => HoaDonTra.IDHoaDon == pt.IDHoaDon).SingleOrDefault() != null)
                {
                    MessageBox.Show("Hóa đơn của bạn đã được trả hàng trước đó. \nKhông thể trả lần nữa.", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    CTHDs.Clear();
                    var cthds = Database.ChiTietHoaDons.Where(ct => ct.IDHoaDon == HoaDonTra.IDHoaDon);
                    NgayBan = HoaDonTra.NgayHoaDon.ToString();
                    KhachHang = Database.KhachHangs.Where(value => value.IDKhachHang == HoaDonTra.IDKhachHang).SingleOrDefault();

                    TongTienCu = 0;
                    foreach (var ct in cthds)
                    {
                        var sanPham = Database.SanPhams.Where(sp => sp.IDSanPham == ct.IDSanPham).SingleOrDefault();
                        var loaiSanPham = Database.LoaiSanPhams.Where(lsp => lsp.IDLoaiSanPham == sanPham.IDLoaiSanPham).SingleOrDefault();
                        var mauSac = Database.MauSacs.Where(ms => ms.IDMauSac == sanPham.IDMauSac).SingleOrDefault();
                        var kichCo = Database.KichCoes.Where(kc => kc.IDKichCo == sanPham.IDKichCo).SingleOrDefault();
                        CTHDs.Add(new DisplayItemInfo(sanPham.IDSanPham, loaiSanPham.TenLoaiSanPham,
                            mauSac.TenMauSac, kichCo.TenKichCo, ct.DonGia, ct.SoLuong));

                        TongTienCu += (ct.DonGia * ct.SoLuong);
                    }
                    Console.WriteLine(TongTienCu);
                    Console.WriteLine(tongTienCu);
                    Console.WriteLine(DisplayTongTienCu);
                }
            });

            LuuPhieuCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                try
                {
                    if (DanhSachTra.Count == 0)
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        var idMoi = TaoID();
                        var newObj = new PhieuTra();
                        newObj.IDPhieuTra = idMoi;
                        newObj.IDHoaDon = HoaDonTra.IDHoaDon;
                        newObj.IDNhanVien = NhanVien.IDNhanVien;
                        newObj.TongTien = TongTienTra;

                        var list = Database.PhieuTras;
                        list.Add(newObj);
                        Database.SaveChanges();

                        var ctpt = Database.ChiTietPhieuTras;
                        foreach (var item in DanhSachTra)
                        {
                            var newCT = new ChiTietPhieuTra();
                            newCT.IDPhieuTra = idMoi;
                            newCT.IDSanPham = item.IDSanPham;
                            newCT.SoLuong = item.SoLuong;
                            ctpt.Add(newCT);
                        }
                        Database.SaveChanges();
                        MessageBox.Show("Thêm thành công.", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                        p.Close();
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                    MessageBox.Show("Đã xảy ra lỗi", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        public void ThemVaoDSTra(DisplayItemInfo obj)
        {
            var existObj = DanhSachTra.Where(v => obj.IDSanPham == v.IDSanPham).SingleOrDefault();
            if (existObj == null)
            {
                obj.SoLuong = 1;
                DanhSachTra.Add(obj);
            }
            else
            {
                obj = new DisplayItemInfo(existObj);
                obj.SoLuong++;
                DanhSachTra[DanhSachTra.IndexOf(existObj)] = obj;
            }

            // Re-calculate TongTienTra
            TongTienTra = 0;
            foreach(var item in DanhSachTra)
                TongTienTra += item.SoLuong * item.DonGia;
        }

        public void ThemVaoCTHD(DisplayItemInfo obj)
        {
            var existObj = CTHDs.Where(v => obj.IDSanPham == v.IDSanPham).SingleOrDefault();
            if (existObj == null)
            {
                obj.SoLuong = 1;
                CTHDs.Add(obj);
            }
            else
            {
                obj = new DisplayItemInfo(existObj);
                obj.SoLuong++;
                CTHDs[CTHDs.IndexOf(existObj)] = obj;
            }

            // Re-calculate TongTienTra
            TongTienTra = 0;
            foreach (var item in DanhSachTra)
                TongTienTra += item.SoLuong * item.DonGia;
        }

        public int TaoID()
        {
            var obj = Database.PhieuTras.OrderByDescending(vl => vl.IDHoaDon).FirstOrDefault();
            if (obj == null) return 10001;
            else
            {
                return obj.IDPhieuTra + 1;
            }
        }

        public class DisplayItemInfo
        {
            public int IDSanPham { get; set; }
            public String TenLoaiSP { get; set; }
            public String TenMauSac { get; set; }
            public String TenKichCo { get; set; }
            public double DonGia { get; set; }
            public int SoLuong { get; set; }

            public DisplayItemInfo(int idSP, String tenLoaiSP, String tenMau, String tenKichCo, double donGia, int soLuong)
            {
                IDSanPham = idSP;
                TenLoaiSP = tenLoaiSP;
                TenMauSac = tenMau;
                TenKichCo = tenKichCo;
                DonGia = donGia;
                SoLuong = soLuong;
            }

            public DisplayItemInfo(DisplayItemInfo obj)
            {
                this.IDSanPham = obj.IDSanPham;
                this.TenLoaiSP = obj.TenLoaiSP;
                this.TenMauSac = obj.TenMauSac;
                this.TenKichCo = obj.TenKichCo;
                this.DonGia = obj.DonGia;
                this.SoLuong = obj.SoLuong;
            }
        }
    }
}
