using QuanLyShopThoiTrang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class PhanQuyenViewModel: BaseViewModel
    {
        private ObservableCollection<VaiTro> _List = new ObservableCollection<VaiTro>();
        public ObservableCollection<VaiTro> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        public ICommand LoadWindowCommand { get; set; }
        public ICommand CapNhatCommand { get; set; }

        public PhanQuyenViewModel()
        {
            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; },
                (p) =>
                {
                    List = new ObservableCollection<VaiTro>(DataProvider.GetInstance.DB.VaiTroes);
                }

             );

            CapNhatCommand = new RelayCommand<Window>((p) => { return true; },
               (p) =>
               {
                   try
                   {
                       foreach (VaiTro e in List)
                       {
                           var vt = DataProvider.GetInstance.DB.VaiTroes.Where(x => x.IDVaiTro == e.IDVaiTro).SingleOrDefault();
                           vt.QLKhachHang = e.QLKhachHang;
                           vt.QLNhaCungCap = e.QLNhaCungCap;
                           vt.QLSanPham = e.QLSanPham;
                           vt.QLHoaDon = e.QLHoaDon;
                           vt.QLNhanVien = e.QLNhanVien;
                           vt.QLLoaiKhachHang = e.QLLoaiKhachHang;
                           vt.LapHoaDon = e.LapHoaDon;
                           vt.LapPhieuTraHang = e.LapPhieuTraHang;
                           vt.LapPhieuNhapHang = e.LapPhieuNhapHang;
                           vt.QLLoaiSanPham = e.QLLoaiSanPham;
                           vt.BaoCao = e.BaoCao;
                           vt.QLSizeMau = e.QLSizeMau;
                           vt.QLVaiTro = e.QLVaiTro;
                            DataProvider.GetInstance.DB.SaveChanges();
                       }
                       MessageBox.Show("Đã cập nhật thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
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
                   p.Close();
               }

            );
        }
    }
}
