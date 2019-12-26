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
                        MessageBox.Show("Vui lòng nhập tên kích cỡ", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        DataProvider.GetInstance.DB.KichCoes.Add(KichCo);
                        DataProvider.GetInstance.DB.SaveChanges();
                        MessageBox.Show("Đã thêm thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    MessageBox.Show("Đã xảy ra lỗi", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
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
