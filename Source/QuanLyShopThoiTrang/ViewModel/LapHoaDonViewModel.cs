using DevExpress.Xpf.Core;
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
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class LapHoaDonViewModel : BaseViewModel
    {

        private DateTime _NgayHoaDon;
        public DateTime NgayHoaDon { get => _NgayHoaDon; set { _NgayHoaDon = value; OnPropertyChanged(); } }

        private int _IDHoaDon;
        public int IDHoaDon { get => _IDHoaDon; set { _IDHoaDon = value; OnPropertyChanged(); } }

        private String _IDNhanVien;
        public String IDNhanVien { get => _IDNhanVien; set { _IDNhanVien = value; OnPropertyChanged(); } }

        private ObservableCollection<HienThiHoaDon> _List = new ObservableCollection<HienThiHoaDon>();
        public ObservableCollection<HienThiHoaDon> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private HienThiHoaDon _SelectedItem;
        public HienThiHoaDon SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();

            }
        }

        private string _IDSanPham;
        public string IDSanPham { get => _IDSanPham; set { _IDSanPham = value; OnPropertyChanged(); } }

        private int _SoLuong;
        public int SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }


        private double _TongHoaDon;
        public double TongHoaDon { get => _TongHoaDon; set { _TongHoaDon = value; OnPropertyChanged(); } }

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

        public ICommand LoadWindowCommand { get; set; }
        public ICommand ThemSanPham { get; set; }
        public ICommand XoaSanPham { get; set; }
        public ICommand ThanhToan { get; set; }
        public ICommand TimKiemSanPham { get; set; }

        public LapHoaDonViewModel()
        {

        }
        public LapHoaDonViewModel(NhanVien NV)
        {
            NewMethod(NV);

            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; },
                (p) =>
                {
                    TaoIDHoaDon();
                    _NgayHoaDon = DateTime.Now;
                    IDNhanVien = NV.HoTen;
                    TongHoaDon = 0;
                    SoSanPham = 0;
                    SoLuong = 1;
                    SoHang = 0;
                }

             );
            TimKiemSanPham = new RelayCommand<Window>((p) => { return true; },
                (p) =>
                {
                    QuanLySanPhamWindow window = new QuanLySanPhamWindow();
                    window.ShowDialog();
                }

             );


            ThemSanPham = new RelayCommand<Window>(
                (p) =>
                {
                    return true;
                },
                (p) =>
                {

                    if (IDSanPham != null)
                    {
                        if (IDSanPham.All(Char.IsDigit) && IDSanPham.Length >= 6)
                        {
                            int id = Int32.Parse(IDSanPham);
                            if (id < 100000)
                                DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng kiểm tra lại mã sản phẩm", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                            else if (SoLuong < 0)
                            {
                                DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng kiểm tra lại số lượng", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                            }
                            else
                            {
                                var sp = DataProvider.GetInstance.DB.SanPhams.Where(x => x.IDSanPham == id).SingleOrDefault();
                                if (sp != null)
                                {
                                    if (sp.SoLuongTon < SoLuong)
                                    {
                                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Hàng trong kho còn ít hơn số lượng yêu cầu", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                                    }
                                    else
                                    {
                                        bool isExist = false;
                                        foreach (HienThiHoaDon i in List)
                                        {
                                            if (i.IDSanPham == id)
                                            {
                                                i.SoLuong += SoLuong;
                                                i.ThanhTien = i.SoLuong * i.DonGia;
                                                isExist = true;
                                                TongHoaDon += (i.DonGia * SoLuong);
                                                SoHang += SoLuong;
                                                IDSanPham = "";
                                                SoLuong = 1;
                                                break;
                                            }
                                        }

                                        if (!isExist)
                                        {
                                            HienThiHoaDon A = new HienThiHoaDon();

                                            A.IDSanPham = sp.IDSanPham;
                                            A.TenSanPham = DataProvider.GetInstance.DB.LoaiSanPhams.Where(x => x.IDLoaiSanPham == sp.IDLoaiSanPham).SingleOrDefault().TenLoaiSanPham;
                                            A.MauSac = DataProvider.GetInstance.DB.MauSacs.Where(x => x.IDMauSac == sp.IDMauSac).SingleOrDefault().TenMauSac;
                                            A.Size = DataProvider.GetInstance.DB.KichCoes.Where(x => x.IDKichCo == sp.IDKichCo).SingleOrDefault().TenKichCo;
                                            A.SoLuong = SoLuong;
                                            A.DonGia = sp.DonGia * 1000;
                                            A.ThanhTien = A.DonGia * A.SoLuong;
                                            List.Add(A);

                                            SoSanPham += 1;
                                            SoHang += SoLuong;
                                            IDSanPham = "";
                                            SoLuong = 1;
                                            TongHoaDon += (A.ThanhTien);
                                        }
                                    }
                                }
                                else
                                {
                                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Hàng trong kho còn ít hơn số lượng yêu cầu", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                                }
                            }
                        }
                        else
                        {
                            DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng kiểm tra lại mã sản phẩm", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng kiểm tra lại số lượng", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    }

                }

            );

            XoaSanPham = new RelayCommand<Window>(
               (p) =>
               {
                   return true;
               },
               (p) =>
               {
                   HienThiHoaDon a = SelectedItem;
                   SelectedItem = null;
                   TongHoaDon -= a.DonGia * a.SoLuong;
                   SoSanPham -= 1;
                   SoHang -= a.SoLuong;
                   List.Remove(a);
               }

           );

            ThanhToan = new RelayCommand<Window>(
               (p) =>
               {
                   return true;
               },
               (p) =>
               {
                   //MessageBox.Show("Xác nhận thanh toán", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Warning);
                   if (List.Count == 0)
                   {
                       MessageBox.Show("Vui lòng chọn sản phẩm", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                   }
                   else
                   {

                       XuatHoaDonWindow wd = new XuatHoaDonWindow(NV.IDNhanVien, TongHoaDon, SoSanPham, SoHang, List, NgayHoaDon);
                       var vm = new XuatHoaDonViewModel(NV.IDNhanVien, TongHoaDon, SoSanPham, SoHang, List, NgayHoaDon);
                       wd.DataContext = wd.ViewModel = vm;

                      

                       wd.ShowDialog();
                       if (vm.returnValue)
                       {
                           List = new ObservableCollection<HienThiHoaDon>(); ;
                           TaoIDHoaDon();

                           NgayHoaDon = DateTime.Now;

                           SoHang = 0;
                           SoSanPham = 0;
                           TongHoaDon = 0;
                       }
                   }

               }

           );


        }

        private void NewMethod(NhanVien NV)
        {
            NgayHoaDon = DateTime.Now;
            IDNhanVien = NV.HoTen;
        }

        public void TaoIDHoaDon()
        {
            List<HoaDon> l = DataProvider.GetInstance.DB.HoaDons.ToList();
            IDHoaDon = l.Count() + 10000000;

        }


    }

}
