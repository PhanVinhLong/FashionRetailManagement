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
using QuanLyShopThoiTrang.ViewModel;
using QuanLyShopThoiTrang.Model;

namespace QuanLyShopThoiTrang.View
{
    /// <summary>
    /// Interaction logic for ThemPhieuNhapWindow.xaml
    /// </summary>
    public partial class ThemPhieuNhapWindow : Window
    {
        public ThemPhieuNhapViewModel ViewModel;
        public ThemPhieuNhapWindow(NhanVien nhanVien)
        {
            InitializeComponent();
            this.DataContext = ViewModel = new ThemPhieuNhapViewModel(nhanVien);
        }
    }
}
