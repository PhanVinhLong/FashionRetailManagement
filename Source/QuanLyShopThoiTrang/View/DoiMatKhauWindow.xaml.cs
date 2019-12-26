using QuanLyShopThoiTrang.ViewModel;
using QuanLyShopThoiTrang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyShopThoiTrang.View
{
    /// <summary>
    /// Interaction logic for DoiMatKhauWindow.xaml
    /// </summary>
    public partial class DoiMatKhauWindow : Window
    {
        public DoiMatKhauViewModel ViewModel { get; set; }
        public DoiMatKhauWindow(NhanVien nhanvien)
        {
            InitializeComponent();
            this.DataContext = ViewModel = new DoiMatKhauViewModel(nhanvien);
        }
    }
}
