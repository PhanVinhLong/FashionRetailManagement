using DevExpress.Xpf.Core;
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
    public class CapNhatLoaiKhachHangViewModel : BaseViewModel
    {

        private string _MucGiam;
        public string MucGiam { get => _MucGiam; set { _MucGiam = value; OnPropertyChanged(); } }


        private LoaiKhachHang _LoaiKhachHang;
        public LoaiKhachHang LoaiKhachHang { get => _LoaiKhachHang; set { _LoaiKhachHang = value; OnPropertyChanged(); } }
        public ICommand CapNhatCommand { get; set; }
        public ICommand XoaCommand { get; set; }
        public CapNhatLoaiKhachHangViewModel(LoaiKhachHang loaiKhachHang)
        {
            LoaiKhachHang = new LoaiKhachHang() { IDLoaiKhachHang = loaiKhachHang.IDLoaiKhachHang, MoTa = loaiKhachHang.MoTa, TichLuyToiThieu = loaiKhachHang.TichLuyToiThieu, MucGiamGia = loaiKhachHang.MucGiamGia };
            MucGiam = loaiKhachHang.MucGiamGia.ToString();


            CapNhatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    double d = Double.Parse(MucGiam);

                    if (d >= 1 || d < 0)
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Mức Giảm Giá phải có giá trị từ 0 đến 1", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    }
                    else
                    {
                        if (LoaiKhachHang.MoTa == "")
                        {
                            DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng nhập tên đầy đủ thông tin", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
                        }
                        else
                        {
                            var LKH = DataProvider.GetInstance.DB.LoaiKhachHangs.Where(x => x.IDLoaiKhachHang == LoaiKhachHang.IDLoaiKhachHang).SingleOrDefault();
                            LKH.MoTa = LoaiKhachHang.MoTa?.Trim();
                            LKH.TichLuyToiThieu = LoaiKhachHang.TichLuyToiThieu;
                            LKH.MucGiamGia = d;

                            DataProvider.GetInstance.DB.SaveChanges();
                            (p.Owner as QuanLyLoaiKhachHangWindow).LoadData();
                            DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã cập nhật thành công", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
                            p.Close();
                        }
                    }
                }
                catch (FormatException e)
                {
                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Mức giảm giá chưa hợp lệ", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
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
    }
}
