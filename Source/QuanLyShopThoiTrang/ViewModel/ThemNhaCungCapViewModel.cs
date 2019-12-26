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
    public class ThemNhaCungCapViewModel: BaseViewModel
    {
        private NhaCungCap _NhaCungCap;
        public NhaCungCap NhaCungCap { get => _NhaCungCap; set { _NhaCungCap = value; OnPropertyChanged(); } }
        public ICommand ThemCommand { get; set; }
        public ThemNhaCungCapViewModel()
        {
            NhaCungCap = new NhaCungCap();
            NhaCungCap.IDNhaCungCap = TaoIDNhaCungCap();

            ThemCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (NhaCungCap.TenNhaCungCap == "" || NhaCungCap.SoDienThoai == "" )
                    {
                        MessageBox.Show("Vui lòng nhập tên đầy đủ thông tin", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        NhaCungCap.SoDienThoai = NhaCungCap.SoDienThoai?.Trim();
                        NhaCungCap.TenNhaCungCap = NhaCungCap.TenNhaCungCap?.Trim();
                        NhaCungCap.Email = NhaCungCap.Email?.Trim();
                        DataProvider.GetInstance.DB.NhaCungCaps.Add(NhaCungCap);
                        DataProvider.GetInstance.DB.SaveChanges();
                        MessageBox.Show("Đã thêm thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                        NhaCungCap = new NhaCungCap();

                        NhaCungCap.IDNhaCungCap = TaoIDNhaCungCap();
                        (p.Owner as QuanLyNhaCungCapWindow).LoadData();
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

        public int TaoIDNhaCungCap()
        {
            int IDNhaCungCap = 101;
            NhaCungCap lstNCC = DataProvider.GetInstance.DB.NhaCungCaps.OrderByDescending(u => u.IDNhaCungCap).FirstOrDefault();
            if (lstNCC != null)
                IDNhaCungCap = lstNCC.IDNhaCungCap + 1;
            return IDNhaCungCap;
        }
    }
}
