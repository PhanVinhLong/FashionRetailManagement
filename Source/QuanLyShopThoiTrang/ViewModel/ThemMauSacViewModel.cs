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
    public class ThemMauSacViewModel : BaseViewModel
    {
        private MauSac _MauSac;
        public MauSac MauSac { get => _MauSac; set { _MauSac = value; OnPropertyChanged(); } }
        public ICommand ThemCommand { get; set; }
        public ThemMauSacViewModel()
        {
            MauSac = new MauSac();
            MauSac.IDMauSac = TaoIDMauSac();

            ThemCommand = new RelayCommand<Window>((p) => 
            {
                return true;
            }, (p) =>
            {
                try
                {
                    if (MauSac.TenMauSac == "")
                    {
                        MessageBox.Show("Vui lòng nhập tên màu sắc", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        DataProvider.GetInstance.DB.MauSacs.Add(MauSac);
                        DataProvider.GetInstance.DB.SaveChanges();
                        MessageBox.Show("Đã thêm thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                        MauSac = new MauSac();
                        MauSac.IDMauSac = TaoIDMauSac();
                        (p.Owner as QuanLyMauSacWindow).LoadData();
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

        public int TaoIDMauSac()
        {
            int ID = 1;
            MauSac lstNV = DataProvider.GetInstance.DB.MauSacs.OrderByDescending(u => u.IDMauSac).FirstOrDefault();
            if (lstNV != null)
                ID = lstNV.IDMauSac + 1;
            return ID;
        }
    }
}
