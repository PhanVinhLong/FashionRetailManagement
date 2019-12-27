using DevExpress.Xpf.Core;
using QuanLyShopThoiTrang.Model;
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
    public class DoiMatKhauViewModel : BaseViewModel
    {
        private string _password;
        private string _password1;
        private string _password2;

        public string password { get => _password; set { _password = value; } }
        public string password1 { get => _password1; set { _password1 = value; } }
        public string password2 { get => _password2; set { _password2 = value; } }


        public ICommand CapNhatCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand PasswordChangedCommand1 { get; set; }
        public ICommand PasswordChangedCommand2 { get; set; }

        private NhanVien nv;

        public DoiMatKhauViewModel(NhanVien nhanvien)
        {
            nv = nhanvien;

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                password = p.Password;
            });

            PasswordChangedCommand1 = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                password1 = p.Password;
            });

            PasswordChangedCommand2 = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                password2 = p.Password;
            });

            CapNhatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                // MessageBox.Show(password + " -  " + password1 + "  -   " + password2);

                try
                {

                    string hashedPassword = ComputeSha256Hash1(password);
                    if (nv.MatKhau != hashedPassword)
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Mật Khẩu Hiện Tại Chưa Đúng", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    }
                    else
                    {
                        if (!Check())
                        {
                            DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Mật khẩu chưa hợp lệ. Mật khẩu chỉ bao gồm số và kí tự. Chiều dài mật khẩu phải trên 8", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                        }
                        else
                        {
                            if (password1 != password2)
                            {
                                DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Xác Nhận Mật Khẩu Chưa Đúng", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                            }
                            else
                            {

                                if (password1 == password)
                                {
                                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Bạn vừa nhập lại mật khẩu cũ", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                                }
                                else
                                {
                                    var nMS = DataProvider.GetInstance.DB.NhanViens.Where(x => x.IDNhanVien == nv.IDNhanVien).SingleOrDefault();
                                    nMS.MatKhau = ComputeSha256Hash1(password1);
                                    DataProvider.GetInstance.DB.SaveChanges();
                                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã cập nhật thành công", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                                    p.Close();
                                }
                            }
                        }
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

        private bool Check()
        {

            if (password1.Length < 6 )
            {
                return false;
            }
            else
            {
                int NumOfDigit = 0, NumOfCharacter = 0, NumOfOther = 0;
                byte[] asciiBytes = Encoding.ASCII.GetBytes(password1);

                for (int i = 0; i < asciiBytes.Length; i++)
                {
                    if (asciiBytes[i] >= 65 && asciiBytes[i] <= 90) NumOfCharacter++;
                    else if (asciiBytes[i] >= 97 && asciiBytes[i] <= 122) NumOfCharacter++;
                    else if (asciiBytes[i] >= 48 && asciiBytes[i] <= 57) NumOfDigit++;
                    else NumOfOther++;

                }


                if (NumOfOther != 0 || NumOfDigit == 0 || NumOfCharacter == 0)
                    return false;
            }


            return true;
        }

        static string ComputeSha256Hash1(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash1 = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash1.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }

            //return rawData;
        }
    }
}
