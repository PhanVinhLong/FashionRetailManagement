using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class QuanLyLoaiKhachHangViewModel : BaseViewModel
    {
        private ObservableCollection<LoaiKhachHang> _ListLoaiKhachHang;
        public ObservableCollection<LoaiKhachHang> ListLoaiKhachHang { get => _ListLoaiKhachHang; set { _ListLoaiKhachHang = value; OnPropertyChanged(); } }

        private ObservableCollection<LoaiKhachHang> _DisplayList;
        public ObservableCollection<LoaiKhachHang> DisplayList { get => _DisplayList; set { _DisplayList = value; OnPropertyChanged(); } }

        private LoaiKhachHang _SelectedItem;
        public LoaiKhachHang SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }

        private string _Keyword;
        public string Keyword
        {
            get => _Keyword; set
            {
                _Keyword = value; OnPropertyChanged();
                if (_Keyword == "")
                {
                    for (int i = DisplayList.Count - 1; i >= 0; i--) DisplayList.RemoveAt(i);
                    foreach (LoaiKhachHang lkh in ListLoaiKhachHang)
                    {
                        var a = new LoaiKhachHang() { IDLoaiKhachHang = lkh.IDLoaiKhachHang, MoTa = lkh.MoTa, TichLuyToiThieu = lkh.TichLuyToiThieu, MucGiamGia = lkh.MucGiamGia };
                        DisplayList.Add(a);
                    }
                }
            }
        }

        public ICommand TimKiem { get; set; }
        public ICommand Them { get; set; }
        public ICommand CapNhat { get; set; }
        public ICommand Xoa { get; set; }

        public QuanLyLoaiKhachHangViewModel()
        {
            LoadData();

            CapNhat = new RelayCommand<object>((p) => {
                if (SelectedItem != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                CapNhatLoaiKhachHangWindow window = new CapNhatLoaiKhachHangWindow(SelectedItem);
                window.Owner = (p as QuanLyLoaiKhachHangWindow);
                window.ShowDialog();
            });

            TimKiem = new RelayCommand<object>((p) =>
            {
                if (Keyword != null)
                {
                    return true;

                }
                else
                {
                    return false;

                }
            }, (p) =>
            {
                for (int i = DisplayList.Count - 1; i >= 0; i--)
                    DisplayList.RemoveAt(i);


                Keyword = Keyword?.Trim();
                foreach (LoaiKhachHang kh in ListLoaiKhachHang)
                {
                    if (kh.MoTa.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || kh.MoTa.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || kh.IDLoaiKhachHang.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var a = new LoaiKhachHang() { IDLoaiKhachHang = kh.IDLoaiKhachHang, MoTa = kh.MoTa, TichLuyToiThieu = kh.TichLuyToiThieu, MucGiamGia = kh.MucGiamGia };
                        DisplayList.Add(a);
                    }
                }
            });

            Them = new RelayCommand<Window>((p) => true, (p) =>
            {
                ThemLoaiKhachHangWindow wd = new ThemLoaiKhachHangWindow();
                wd.Owner = p;
                wd.ShowDialog();
            });


            Xoa = new RelayCommand<Window>((p) => {
                if (SelectedItem != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                try
                {
                    var LKH = DataProvider.GetInstance.DB.LoaiKhachHangs.Where(x => x.IDLoaiKhachHang == SelectedItem.IDLoaiKhachHang).SingleOrDefault();
                    DataProvider.GetInstance.DB.LoaiKhachHangs.Remove(LKH);
                    DataProvider.GetInstance.DB.SaveChanges();
                    LoadData();
                    MessageBox.Show("Đã xoá thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                    MessageBox.Show("Đã xảy ra lỗi", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    MessageBox.Show("Dữ liệu này không thể xóa", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
        public void LoadData()
        {
            ListLoaiKhachHang = new ObservableCollection<LoaiKhachHang>(DataProvider.GetInstance.DB.LoaiKhachHangs);

            DisplayList = new ObservableCollection<LoaiKhachHang>();
            foreach (LoaiKhachHang lkh in ListLoaiKhachHang)
            {
               
                DisplayList.Add(lkh);
            }
                
        }
    }
}
