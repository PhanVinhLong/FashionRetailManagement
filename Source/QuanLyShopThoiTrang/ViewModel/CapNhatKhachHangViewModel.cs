using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Core;
using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.View;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class CapNhatKhachHangViewModel : BaseViewModel
    {
        private KhachHang _KH;
        public KhachHang KH { get => _KH; set { _KH = value; OnPropertyChanged(); } }
        public ICommand CapNhatCommand { get; set; }
        public ICommand XoaCommand { get; set; }
        public CapNhatKhachHangViewModel(KhachHang kh)
        {
            KH = new KhachHang { IDKhachHang = kh.IDKhachHang, HoTen = kh.HoTen, NamSinh = kh.NamSinh, GioiTinh = kh.GioiTinh, SoDienThoai = kh.SoDienThoai, Email = kh.Email, TongTienTichLuy = kh.TongTienTichLuy };

            CapNhatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (KH.HoTen == "" || KH.NamSinh == 0 || KH.GioiTinh == "" || KH.SoDienThoai == "")
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng nhập tên đầy đủ thông tin", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    }
                    else if (!Check(KH))
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng kiểm tra lại thông tin", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    }
                    else
                    {
                        var nKH = DataProvider.GetInstance.DB.KhachHangs.Where(x => x.IDKhachHang == KH.IDKhachHang).SingleOrDefault();
                        nKH.HoTen = KH.HoTen?.Trim();
                        nKH.NamSinh = KH.NamSinh;
                        nKH.GioiTinh = KH.GioiTinh;
                        nKH.SoDienThoai = KH.SoDienThoai?.Trim();
                        nKH.Email = KH.Email?.Trim();

                        DataProvider.GetInstance.DB.SaveChanges();
                        (p.Owner as QuanLyKhachHangWindow).LoadData();
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã cập nhật thành công", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
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
                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã xảy ra lỗi", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                }
            });
        }

        private bool Check(KhachHang nv)
        {
            string name = nv.HoTen?.Trim();
            foreach (char c in name)
            {
                int a = (int)c;
                if ((a >= 33 && a <= 64) || (a >= 91 && a <= 96) || (a >= 123 && a <= 126)) return false;
            }

            string phone = nv.SoDienThoai?.Trim();
            foreach (char c in phone)
            {
                int a = (int)c;
                if (!(a >= 48 && a <= 57))
                    return false;
            }


            return true;
        }
    }
}
