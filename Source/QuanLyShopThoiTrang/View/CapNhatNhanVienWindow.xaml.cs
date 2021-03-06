﻿using QuanLyShopThoiTrang.Model;
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
    /// Interaction logic for CapNhatNhanVienWindow.xaml
    /// </summary>
    public partial class CapNhatNhanVienWindow : Window
    {
        public CapNhatNhanVienViewModel ViewModel { get; set; }
        public CapNhatNhanVienWindow(NhanVien NV)
        {
            InitializeComponent();
            this.DataContext = ViewModel = new CapNhatNhanVienViewModel(NV);
        }
    }
}
