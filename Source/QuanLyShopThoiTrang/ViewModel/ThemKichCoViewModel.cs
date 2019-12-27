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
    public class ThemKichCoViewModel : BaseViewModel
    {
        private KichCo _KichCo;
        public KichCo KichCo { get => _KichCo; set { _KichCo = value; OnPropertyChanged(); } }
        public ICommand ThemCommand { get; set; }
        public ThemKichCoViewModel()
        {
            KichCo = new KichCo();
            KichCo.IDKichCo = TaoIDKichCo();

            ThemCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (KichCo.TenKichCo == "")
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng nhập tên kích cỡ", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
                    }
                    else
                    {
                        DataProvider.GetInstance.DB.KichCoes.Add(KichCo);
                        DataProvider.GetInstance.DB.SaveChanges();
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã thêm thành công", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
                        KichCo = new KichCo();
                        KichCo.IDKichCo = TaoIDKichCo();
                        (p.Owner as QuanLyKichCoWindow).LoadData();
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

        public int TaoIDKichCo()
        {
            int ID = 1;
            KichCo lstNV = DataProvider.GetInstance.DB.KichCoes.OrderByDescending(u => u.IDKichCo).FirstOrDefault();
            if (lstNV != null)
                ID = lstNV.IDKichCo + 1;
            return ID;
        }
    }
}
