using QuanLyShopThoiTrang.Model;
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
    public class ThemVaiTroViewModel:BaseViewModel
    {
        private VaiTro _vaitro;
        public VaiTro vaitro { get => _vaitro; set { _vaitro = value; OnPropertyChanged(); } }

        public ICommand LoadWindowCommand { get; set; }
        public ICommand ThemCommand { get; set; }
        public ThemVaiTroViewModel()
        {
            vaitro = new VaiTro();
            vaitro.IDVaiTro = 1;
            vaitro.IDVaiTro = TaoIDVaiTro();
            vaitro.TenVaiTro = "";
            vaitro.MoTa = "";
           

            ThemCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                try
                {
                    DataProvider.GetInstance.DB.VaiTroes.Add(vaitro);
                    DataProvider.GetInstance.DB.SaveChanges();
                    MessageBox.Show("Đã thêm thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                    vaitro = new VaiTro();
                    vaitro.IDVaiTro = TaoIDVaiTro();
                    vaitro.TenVaiTro = "";
                    vaitro.MoTa = "";
                    p.Close();

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

        public int TaoIDVaiTro()
        {
            int IDVaiTro = 1;
            VaiTro lstVT = DataProvider.GetInstance.DB.VaiTroes.OrderByDescending(u => u.IDVaiTro).FirstOrDefault();
            if (lstVT != null)
                IDVaiTro = lstVT.IDVaiTro + 1;
            return IDVaiTro;
        }
    }
}
