using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.ViewModel;
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
    /// Interaction logic for CapNhatLoaiSanPhamWindow.xaml
    /// </summary>
    public partial class CapNhatLoaiSanPhamWindow : Window
    {
        public CapNhatLoaiSanPhamViewModel ViewModel { get; set; }
        public CapNhatLoaiSanPhamWindow(LoaiSanPham LSP)
        {
            InitializeComponent();
            this.DataContext = ViewModel = new CapNhatLoaiSanPhamViewModel(LSP);
        }
    }
}
