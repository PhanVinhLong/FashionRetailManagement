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
    public class CapNhatNhaCungCapViewModel : BaseViewModel
    {
        private NhaCungCap _NhaCungCap;
        public NhaCungCap NhaCungCap { get => _NhaCungCap; set { _NhaCungCap = value; OnPropertyChanged(); } }
        public ICommand CapNhatCommand { get; set; }
        public ICommand XoaCommand { get; set; }
        public CapNhatNhaCungCapViewModel(NhaCungCap nhaCungCap)
        {
            NhaCungCap = new NhaCungCap() { IDNhaCungCap = nhaCungCap.IDNhaCungCap, TenNhaCungCap = nhaCungCap.TenNhaCungCap, SoDienThoai = nhaCungCap.SoDienThoai, Email = nhaCungCap.Email, DiaChi = nhaCungCap.DiaChi };

            CapNhatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (NhaCungCap.TenNhaCungCap == "" || NhaCungCap.SoDienThoai == "")
                    {
                        MessageBox.Show("Vui lòng nhập tên đầy đủ thông tin", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        var NCC = DataProvider.GetInstance.DB.NhaCungCaps.Where(x => x.IDNhaCungCap == NhaCungCap.IDNhaCungCap).SingleOrDefault();
                        NCC.TenNhaCungCap = NhaCungCap.TenNhaCungCap?.Trim();
                        NCC.SoDienThoai = NhaCungCap.SoDienThoai?.Trim();
                        NCC.Email = NhaCungCap.Email?.Trim();
                        NCC.DiaChi = NhaCungCap.DiaChi?.Trim();

                        DataProvider.GetInstance.DB.SaveChanges();
                        (p.Owner as QuanLyNhaCungCapWindow).LoadData();
                        MessageBox.Show("Đã cập nhật thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
