﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using QuanLyShopThoiTrang.Model;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class BaoCaoNhanVien
    {
        public BaoCaoNhanVien(int iDNhanVien, string hoTen, int doanhThu)
        {
            IDNhanVien = iDNhanVien;
            HoTen = hoTen;
            DoanhThu = doanhThu;
        }

        public int IDNhanVien { get; set; }
        public string HoTen { get; set; }
        public int DoanhThu { get; set; }
    }

    public class BaoCaoNhanVienViewModel: BaseViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        private List<BaoCaoNhanVien> _ListNhanVien;
        public List<BaoCaoNhanVien> ListNhanVien { get => _ListNhanVien; set { _ListNhanVien = value; OnPropertyChanged(); } }
        public ICommand LoadBaoCao { get; set; }
        private SeriesCollection _DataNhanVien;
        public SeriesCollection DataNhanVien { get => _DataNhanVien; set { _DataNhanVien = value; OnPropertyChanged(); } }

    public BaoCaoNhanVienViewModel()
        {
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;

            DataNhanVien = new SeriesCollection();

            LoadBaoCao = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadDoanhThuNhanVien();
            });

            LoadDoanhThuNhanVien();
        }

        void LoadDoanhThuNhanVien()
        {
            List<BaoCaoNhanVien> temp = new List<BaoCaoNhanVien>();
            List<NhanVien> listNV = new List<NhanVien>(DataProvider.GetInstance.DB.NhanViens);

            foreach(NhanVien nv in listNV)
            {
                BaoCaoNhanVien bcnv = new BaoCaoNhanVien(nv.IDNhanVien, nv.HoTen, 0);
                foreach(HoaDon hd in nv.HoaDons)
                {
                    if(hd.NgayHoaDon >= StartDate && hd.NgayHoaDon <= EndDate)
                    {
                        foreach (ChiTietHoaDon cthd in hd.ChiTietHoaDons)
                        {
                            bcnv.DoanhThu += cthd.SoLuong * (int)cthd.DonGia * 1000;
                        }
                    }
                }
                if (bcnv.DoanhThu > 0)
                    temp.Add(bcnv);
            }
            if(temp.Count() <= 0)
            {
                Console.WriteLine("-----------------");
                ListNhanVien = new List<BaoCaoNhanVien>(temp);
                DataNhanVien = new SeriesCollection();
            }

            ListNhanVien = new List<BaoCaoNhanVien>(temp);
            DataNhanVien = new SeriesCollection { };
            foreach(BaoCaoNhanVien nv in ListNhanVien)
            {
                DataNhanVien.Add(new PieSeries
                {
                    Title = nv.HoTen,
                    Values = new ChartValues<double> { nv.DoanhThu },
                    DataLabels = true
                });
            }
        }
    }
}
