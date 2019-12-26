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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyShopThoiTrang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel {get; set;}

        public MainWindow(NhanVien nhanvien)
        {
            InitializeComponent();
            this.DataContext = ViewModel = new MainViewModel(nhanvien);
        }

        public void Load()
        {
            ViewModel.LoadTopList();
            ViewModel.LoadLackList();
        }

        private void PopupBox_OnOpened(object sender, RoutedEventArgs e) //Event which will be triggerd on click of ya button
        {
            //throw new NotImplementedException();
        }

        private void PopupBox_OnClosed(object sender, RoutedEventArgs e) //Event which will be triggerd on click of ya button
        {
            //throw new NotImplementedException();
        }

        private void ControlBarUC_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
