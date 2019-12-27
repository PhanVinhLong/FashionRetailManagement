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
    public class ThemKhachHangViewModel : BaseViewModel
    {
        private KhachHang _KH;
        public KhachHang KH { get => _KH; set { _KH = value; OnPropertyChanged(); } }
        public ICommand ThemKhachHangCommand { get; set; }

        public ThemKhachHangViewModel()
        {
            KH = new KhachHang();
            KH.IDKhachHang = TaoIDKhachHang();

            ThemKhachHangCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (KH.HoTen == "" || KH.NamSinh == 0 || KH.GioiTinh =="" || KH.SoDienThoai == "" )
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng nhập tên đầy đủ thông tin", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    }
                    else if(!Check(KH))
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng kiểm tra lại thông tin", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    }
                    else
                    {
                        KH.HoTen = KH.HoTen?.Trim();
                        KH.SoDienThoai = KH.SoDienThoai?.Trim();
                        KH.Email = KH.Email?.Trim();
                        DataProvider.GetInstance.DB.KhachHangs.Add(KH);
                        DataProvider.GetInstance.DB.SaveChanges();
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã thêm thành công", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
                        KH = new KhachHang();
                        KH.IDKhachHang = TaoIDKhachHang();
                        (p.Owner as QuanLyKhachHangWindow).LoadData();
                        // MessageBox.Show("Đã cập nhật thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                        // p.Close();
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

        public int TaoIDKhachHang()
        {
            int IDKhachHang = 10000001;
            KhachHang lstKH = DataProvider.GetInstance.DB.KhachHangs.OrderByDescending(u => u.IDKhachHang).FirstOrDefault();
            if (lstKH != null)
                IDKhachHang = lstKH.IDKhachHang + 1;
            return IDKhachHang;
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
