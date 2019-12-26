using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class ThemNhanVienViewModel: BaseViewModel
    {
        private NhanVien _NhanVien;
        public NhanVien NhanVien { get => _NhanVien; set { _NhanVien = value; OnPropertyChanged(); } }

        private List<VaiTro> _ListVaiTro;
        public List<VaiTro> ListVaiTro { get => _ListVaiTro; set { _ListVaiTro = value; OnPropertyChanged(); } }

        private VaiTro _SelectedVaiTro;
        public VaiTro SelectedVaiTro { get => _SelectedVaiTro; set { _SelectedVaiTro = value; OnPropertyChanged(); NhanVien.IDVaiTro = SelectedVaiTro.IDVaiTro; } }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ThemNhanVienCommand { get; set; }

        public ThemNhanVienViewModel()
        {
            ListVaiTro = DataProvider.GetInstance.DB.VaiTroes.ToList();
            NhanVien = new NhanVien();
            NhanVien.IDNhanVien = TaoIDNhanVien();

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                NhanVien.MatKhau = ComputeSha256Hash(p.Password);
            });

            ThemNhanVienCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (NhanVien.HoTen == "" || NhanVien.MatKhau == "" || NhanVien.SoDienThoai == "" || NhanVien.GioiTinh == "" || NhanVien.Email == "" || NhanVien.DiaChi == "" || NhanVien.ChungMinhNhanDan == "" || !Check(NhanVien))
                    {
                        MessageBox.Show("Vui lòng kiểm tra lại thông tin", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        DataProvider.GetInstance.DB.NhanViens.Add(NhanVien);
                        DataProvider.GetInstance.DB.SaveChanges();
                        MessageBox.Show("Đã thêm thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                        NhanVien = new NhanVien();
                        NhanVien.IDNhanVien = TaoIDNhanVien();
                        (p.Owner as QuanLyNhanVienWindow).LoadData();
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

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public int TaoIDNhanVien()
        {
            int IDNhanVien = 10000001;
            NhanVien lstNV = DataProvider.GetInstance.DB.NhanViens.OrderByDescending(u => u.IDNhanVien).FirstOrDefault();
            if (lstNV != null)
                IDNhanVien = lstNV.IDNhanVien + 1;

            lstNV.IDVaiTro = ListVaiTro.First().IDVaiTro;
            SelectedVaiTro = ListVaiTro.First();
            return IDNhanVien;
        }

        private bool Check(NhanVien nv)
        {
            //string name = nv.HoTen;
            //foreach(char c in name)
            //{
            //    int a = (int)c;
            //    if (!((a >= 65 && a <= 90) || (a >= 97 && a <= 122) || (a == 32)))
            //        return false;
            //}

            string phone = nv.SoDienThoai;
            foreach(char c in phone)
            {
                int a = (int)c;
                if (!(a>=48 && a<=57))
                    return false;
            }

            string id = nv.ChungMinhNhanDan;
            foreach (char c in id)
            {
                int a = (int)c;
                if (!(a >= 48 && a <= 57))
                    return false;
            }

      

            return true;
        }
    }
}
