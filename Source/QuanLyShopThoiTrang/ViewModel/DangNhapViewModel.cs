using QuanLyShopThoiTrang.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Security.Cryptography;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class DangNhapViewModel
    {
        private string _username;
        private string _password;
        private NhanVien nhanvien;
        public string username { get => _username; set { _username = value; } }
        public string password { get => _password; set { _password = value; } }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand DangNhapCommand { get; set; }
        public DangNhapViewModel()
        {
            
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                password = p.Password;
            });

            DangNhapCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
            string hashedPassword = ComputeSha256Hash(password);
            int a;
            int.TryParse(username, out a);
            var accCount = DataProvider.GetInstance.DB.NhanViens.Where(x => x.IDNhanVien == a && x.MatKhau == hashedPassword).Count();
            if (accCount > 0)
            {
                    nhanvien = DataProvider.GetInstance.DB.NhanViens.Where(x => x.IDNhanVien == a && x.MatKhau == hashedPassword).SingleOrDefault();
                    MainWindow mainWindow = new MainWindow(nhanvien);
                    mainWindow.Show();
                    p.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng");
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
    }
}
