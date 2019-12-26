using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for XuatHoaDonWindow.xaml
    /// </summary>
    public partial class XuatHoaDonWindow : Window
    {
        public XuatHoaDonViewModel ViewModel { get; set; }
        public int a;

        public XuatHoaDonWindow(int IDNV,
            double Tong, int SoSp, int SoHang, ObservableCollection<HienThiHoaDon> L, DateTime NgayHD)
        {
            InitializeComponent();
            //this.DataContext = ViewModel = new XuatHoaDonViewModel( IDNV,
            // Tong,  SoSp,  SoHang,  L,  NgayHD);


            int b;
    }
    }
}
