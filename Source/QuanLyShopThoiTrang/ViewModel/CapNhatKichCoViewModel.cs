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
                        MessageBox.Show("Vui lòng nhập tên kích cỡ", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        var nKC = DataProvider.GetInstance.DB.KichCoes.Where(x => x.IDKichCo == KichCo.IDKichCo).SingleOrDefault();
                        nKC.TenKichCo = KichCo.TenKichCo;

                        DataProvider.GetInstance.DB.SaveChanges();
                        (p.Owner as QuanLyKichCoWindow).LoadData();
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
