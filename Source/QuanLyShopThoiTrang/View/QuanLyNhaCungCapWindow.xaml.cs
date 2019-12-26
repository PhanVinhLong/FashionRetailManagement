﻿using QuanLyShopThoiTrang.ViewModel;
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
    /// Interaction logic for QuanLyNhaCungCapWindow.xaml
    /// </summary>
    public partial class QuanLyNhaCungCapWindow : Window
    {
        public QuanLyNhaCungCapWindow()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            (this.DataContext as QuanLyNhaCungCapViewModel).LoadData();
        }
    }
}
