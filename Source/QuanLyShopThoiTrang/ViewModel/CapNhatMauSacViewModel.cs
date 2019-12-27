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
    public class CapNhatMauSacViewModel : BaseViewModel
    {
        private MauSac _MauSac;
        public MauSac MauSac { get => _MauSac; set { _MauSac = value; OnPropertyChanged(); } }
        public ICommand CapNhatCommand { get; set; }
        public ICommand XoaCommand { get; set; }
        public CapNhatMauSacViewModel(MauSac mauSac)
        {
            MauSac = new MauSac() { IDMauSac = mauSac.IDMauSac, TenMauSac = mauSac.TenMauSac };

            CapNhatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (MauSac.TenMauSac == "")
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng nhập tên màu sắc", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    }
                    else
                    {
                        var nMS = DataProvider.GetInstance.DB.MauSacs.Where(x => x.IDMauSac == MauSac.IDMauSac).SingleOrDefault();
                        nMS.TenMauSac = MauSac.TenMauSac;

                        DataProvider.GetInstance.DB.SaveChanges();
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã cập nhật thành công", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
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
                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã xảy ra lỗi", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                }
            });
        }
    }
}
