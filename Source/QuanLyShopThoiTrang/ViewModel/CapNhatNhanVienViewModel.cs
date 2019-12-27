using DevExpress.Xpf.Core;
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
    public class CapNhatNhanVienViewModel : BaseViewModel
    {
        private NhanVien _NhanVien;
        public NhanVien NhanVien { get => _NhanVien; set { _NhanVien = value; OnPropertyChanged(); } }

        private List<VaiTro> _ListVaiTro;
        public List<VaiTro> ListVaiTro { get => _ListVaiTro; set { _ListVaiTro = value; OnPropertyChanged(); } }

        private VaiTro _SelectedVaiTro;
        public VaiTro SelectedVaiTro { get => _SelectedVaiTro; set { _SelectedVaiTro = value; OnPropertyChanged(); NhanVien.IDVaiTro = SelectedVaiTro.IDVaiTro; } }

        private string _password;
        public string password { get => _password; set { _password = value; } }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand CapNhatCommand { get; set; }
        public ICommand XoaCommand { get; set; }
        public CapNhatNhanVienViewModel(NhanVien nhanVien)
        {
            NhanVien = new NhanVien
            {
                IDNhanVien = nhanVien.IDNhanVien,
                IDVaiTro = nhanVien.IDVaiTro,
                MatKhau = nhanVien.MatKhau,
                HoTen = nhanVien.HoTen,
                ChungMinhNhanDan = nhanVien.ChungMinhNhanDan,
                SoDienThoai = nhanVien.SoDienThoai,
                Email = nhanVien.Email,
                DiaChi = nhanVien.DiaChi,
                GioiTinh = nhanVien.GioiTinh
            };
            ListVaiTro = DataProvider.GetInstance.DB.VaiTroes.ToList();
            SelectedVaiTro = DataProvider.GetInstance.DB.VaiTroes.Where((p) => p.IDVaiTro == NhanVien.IDVaiTro).FirstOrDefault();

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                password = p.Password;
            });

            CapNhatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (NhanVien.HoTen == "" || NhanVien.MatKhau == "" || NhanVien.SoDienThoai == "" || NhanVien.GioiTinh == "" || NhanVien.Email == "" || NhanVien.DiaChi == "" || NhanVien.ChungMinhNhanDan == "" || !Check(NhanVien))
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng kiểm tra lại thông tin", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    }
                    else
                    {
                        var NV = DataProvider.GetInstance.DB.NhanViens.Where(x => x.IDNhanVien == nhanVien.IDNhanVien).SingleOrDefault();
                        NV.IDVaiTro = SelectedVaiTro.IDVaiTro;
                        if (password != null && password.Length > 0)
                            NV.MatKhau = ComputeSha256Hash(password);
                        NV.HoTen = NhanVien.HoTen;
                        NV.ChungMinhNhanDan = NhanVien.ChungMinhNhanDan;
                        NV.SoDienThoai = NhanVien.SoDienThoai;
                        NV.Email = NhanVien.Email;
                        NV.DiaChi = NhanVien.DiaChi;
                        NV.GioiTinh = NhanVien.GioiTinh;

                        DataProvider.GetInstance.DB.SaveChanges();
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã cập nhật thành công", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
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
                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã xảy ra lỗi", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
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

        private bool Check(NhanVien nv)
        {
            string name = nv.HoTen;
            foreach (char c in name)
            {
                int a = (int)c;
                if (!((a >= 65 && a <= 90) || (a >= 97 && a <= 122) || (a == 32)))
                    return false;
            }

            string phone = nv.SoDienThoai;
            foreach (char c in phone)
            {
                int a = (int)c;
                if (!(a >= 48 && a <= 57))
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
