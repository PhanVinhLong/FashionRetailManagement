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
    /// Interaction logic for QuanLyMauSacWindow.xaml
    /// </summary>
    public partial class QuanLyMauSacWindow : Window
    {
        public QuanLyMauSacWindow()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            (this.DataContext as QuanLyMauSacViewModel).LoadData();
        }
    }
}
