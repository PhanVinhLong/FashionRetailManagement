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
    public class CapNhatKichCoViewModel: BaseViewModel
    {
        private KichCo _KichCo;
        public KichCo KichCo { get => _KichCo; set { _KichCo = value; OnPropertyChanged(); } }
        public ICommand CapNhatCommand { get; set; }
        public CapNhatKichCoViewModel(KichCo kichCo)
        {
            KichCo = new KichCo { IDKichCo = kichCo.IDKichCo, TenKichCo = kichCo.TenKichCo };

            CapNhatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (KichCo.TenKichCo == "")
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng nhập tên kích cỡ", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
                    }
                    else
                    {
                        var nKC = DataProvider.GetInstance.DB.KichCoes.Where(x => x.IDKichCo == KichCo.IDKichCo).SingleOrDefault();
                        nKC.TenKichCo = KichCo.TenKichCo;

                        DataProvider.GetInstance.DB.SaveChanges();
                        (p.Owner as QuanLyKichCoWindow).LoadData();
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
    }
}
