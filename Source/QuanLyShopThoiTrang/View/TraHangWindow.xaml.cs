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
using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.ViewModel;


namespace QuanLyShopThoiTrang.View
{
    /// <summary>
    /// Interaction logic for TraHangWindow.xaml
    /// </summary>
    public partial class TraHangWindow : Window
    {
        public TraHangViewModel ViewModel;
        public TraHangWindow(NhanVien nhanVien)
        {
            InitializeComponent();
            this.DataContext = ViewModel = new TraHangViewModel(nhanVien);
        }
    }
}
