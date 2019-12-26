using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class ThemLoaiKhachHangViewModel : BaseViewModel
    {
        private LoaiKhachHang _LoaiKhachHang;
        public LoaiKhachHang LoaiKhachHang { get => _LoaiKhachHang; set { _LoaiKhachHang = value; OnPropertyChanged(); } }

        private string _MucGiam;
        public string MucGiam { get => _MucGiam; set { _MucGiam = value; OnPropertyChanged(); } }

        public ICommand ThemCommand { get; set; }
        public ThemLoaiKhachHangViewModel()
        {
            LoaiKhachHang = new LoaiKhachHang();
            LoaiKhachHang.IDLoaiKhachHang = TaoIDNLoaiKhachHang();
            

            ThemCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    double d  = Double.Parse(MucGiam);
                    
                    if (d >= 1 || d < 0)
                    {
                        MessageBox.Show("Mức Giảm Giá phải có giá trị từ 0 đến 1", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (LoaiKhachHang.MoTa == "")
                        {
                            MessageBox.Show("Vui lòng nhập tên đầy đủ thông tin", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            LoaiKhachHang.MucGiamGia = d;
                            LoaiKhachHang.MoTa = LoaiKhachHang.MoTa?.Trim();
                            DataProvider.GetInstance.DB.LoaiKhachHangs.Add(LoaiKhachHang);

                            DataProvider.GetInstance.DB.SaveChanges();
                            MessageBox.Show("Đã thêm thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoaiKhachHang = new LoaiKhachHang();
                            LoaiKhachHang.IDLoaiKhachHang = TaoIDNLoaiKhachHang();
                            (p.Owner as QuanLyLoaiKhachHangWindow).LoadData();
                            p.Close();
                        }
                    }
                }
                catch(FormatException e)
                {
                    MessageBox.Show("Mức giảm giá chưa hợp lệ", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public int TaoIDNLoaiKhachHang()
        {
            int IDLoaiKhachHang = 1;
            LoaiKhachHang lstNV = DataProvider.GetInstance.DB.LoaiKhachHangs.OrderByDescending(u => u.IDLoaiKhachHang).FirstOrDefault();
            if (lstNV != null)
                IDLoaiKhachHang = lstNV.IDLoaiKhachHang + 1;
            return IDLoaiKhachHang;
        }
    }
}
