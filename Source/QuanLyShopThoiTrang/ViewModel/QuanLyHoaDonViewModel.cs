using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.View;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class QuanLyHoaDonViewModel : BaseViewModel
    {
        private ObservableCollection<HoaDon> _List;
        public ObservableCollection<HoaDon> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<HoaDon> _DisplayList;
        public ObservableCollection<HoaDon> DisplayList { get => _DisplayList; set { _DisplayList = value; OnPropertyChanged(); } }

        private bool _IsDateFilter;
        public bool IsDateFilter { get => _IsDateFilter; set { _IsDateFilter = value; OnPropertyChanged(); } }

        private DateTime _Tungay;
        public DateTime Tungay { get => _Tungay; set { _Tungay = value; OnPropertyChanged(); } }

        private DateTime _Denngay;
        public DateTime Denngay { get => _Denngay; set { _Denngay = value; OnPropertyChanged(); } }

        private HoaDon _SelectedItem;
        public HoaDon SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        private string _Keyword;
        public string Keyword
        {
            get => _Keyword; set
            {
                _Keyword = value;
                OnPropertyChanged();

                //if (Keyword == "")
                //{
                //    DisplayList = new ObservableCollection<HoaDon>();
                //    foreach (HoaDon kh in List)
                //        DisplayList.Add(kh);
                //}
            }
        }

        public ICommand TimKiem { get; set; }
        public ICommand Refresh { get; set; }
        public ICommand Xem { get; set; }

        public QuanLyHoaDonViewModel()
        {
            LoadData();

            TimKiem = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                if (IsDateFilter == true || Keyword != null)
                {
                    for (int i = DisplayList.Count - 1; i >= 0; i--)
                        DisplayList.RemoveAt(i);


                    if (IsDateFilter)
                    {
                        DateTime date1 = new DateTime(Tungay.Year, Tungay.Month, Tungay.Day, 0, 0, 0);
                        DateTime date2 = new DateTime(Denngay.Year, Denngay.Month, Denngay.Day, 11, 59, 59);

                        var hd1 = List.OrderByDescending(u => u.NgayHoaDon).Last();
                        var hd2 = List.OrderByDescending(u => u.NgayHoaDon).FirstOrDefault();

                      //  MessageBox.Show(hd1.ToString());

                        if (DateTime.Compare(hd1.NgayHoaDon, date1) > 0 || DateTime.Compare(hd2.NgayHoaDon, date1) < 0)
                            LoadList(Tungay, Denngay);

                        if (Keyword != null)
                        {
                            foreach (HoaDon kh in List)
                            {
                                if ((kh.IDHoaDon.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0
                                || kh.IDNhanVien.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0
                                || kh.IDKhachHang.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0) && (DateTime.Compare(kh.NgayHoaDon, date1) >= 0) && (DateTime.Compare(kh.NgayHoaDon, date2) < 0))
                                {
                                    var a = new HoaDon() { IDKhachHang = kh.IDKhachHang, IDNhanVien = kh.IDNhanVien, IDHoaDon = kh.IDHoaDon, NgayHoaDon = kh.NgayHoaDon };
                                    DisplayList.Add(a);
                                }
                            }
                        }
                        else
                        {
                            foreach (HoaDon kh in List)
                            {
                                if ((DateTime.Compare(kh.NgayHoaDon, date1) >= 0) && (DateTime.Compare(kh.NgayHoaDon, date2) < 0))
                                {
                                    var a = new HoaDon() { IDKhachHang = kh.IDKhachHang, IDNhanVien = kh.IDNhanVien, IDHoaDon = kh.IDHoaDon, NgayHoaDon = kh.NgayHoaDon };
                                    DisplayList.Add(a);
                                }
                            }
                        }
                    }
                    else

                        foreach (HoaDon kh in List)
                        {
                            if (kh.IDHoaDon.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0
                            || kh.IDNhanVien.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0
                            || kh.IDKhachHang.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                var a = new HoaDon() { IDKhachHang = kh.IDKhachHang, IDNhanVien = kh.IDNhanVien, IDHoaDon = kh.IDHoaDon, NgayHoaDon = kh.NgayHoaDon };
                                DisplayList.Add(a);
                            }
                        }
                }


            });


            Refresh = new RelayCommand<object>((p) =>
            {
                return true;
            },
             (p) =>
             {
                 LoadData();
             });


            Xem = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
               if(SelectedItem != null)
                {
                    XemChiTietHoaDonWindow wd = new XemChiTietHoaDonWindow(SelectedItem);
                    wd.ShowDialog();
                }
            });
        }

        private void LoadData()
        {
            List = new ObservableCollection<HoaDon>(DataProvider.GetInstance.DB.HoaDons.Where(x => x.NgayHoaDon.Month == DateTime.Now.Month || x.NgayHoaDon.Month == DateTime.Now.Month - 1));
            DisplayList = new ObservableCollection<HoaDon>();
            foreach (HoaDon kh in List)
                DisplayList.Add(kh);

            IsDateFilter = false;

            Tungay = DateTime.Now.AddDays(-7);
            Denngay = DateTime.Now;
        }

        private void LoadList(DateTime d1, DateTime d2)
        {
            List = new ObservableCollection<HoaDon>(DataProvider.GetInstance.DB.HoaDons.Where(x => DateTime.Compare(x.NgayHoaDon, d1) >= 0 && DateTime.Compare(x.NgayHoaDon, d2) < 0));
        }
    }
}
