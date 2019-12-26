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
    public class QuanLyKhachHangViewModel : BaseViewModel
    {
        public List<String> ListSapXep
        {
            get
            {
                return new List<String>
                {
                    "Tích lũy tăng dần","Tích lũy giảm dần","Tên KH từ A->Z","Tên KH từ Z->A"
                };
            }
        }

        private ObservableCollection<KhachHang> _ListKhachHang;
        public ObservableCollection<KhachHang> ListKhachHang { get => _ListKhachHang; set { _ListKhachHang = value; OnPropertyChanged(); } }

        private ObservableCollection<KhachHang> _DisplayList;
        public ObservableCollection<KhachHang> DisplayList { get => _DisplayList; set { _DisplayList = value; OnPropertyChanged(); } }

        private KhachHang _SelectedItem;
        public KhachHang SelectedItem {get => _SelectedItem; set {_SelectedItem = value; OnPropertyChanged(); } }

        private String _SelectedSapXep;
        public String SelectedSapXep
        {
            get => _SelectedSapXep;
            set
            {
                _SelectedSapXep = value;
                OnPropertyChanged();
                if (value == "Tích lũy tăng dần")
                {
                    DisplayList = new ObservableCollection<KhachHang>(ListKhachHang.OrderBy(x => x.TongTienTichLuy).ToList());
                }
                else if (value == "Tích lũy giảm dần")
                {
                    DisplayList = new ObservableCollection<KhachHang>(ListKhachHang.OrderByDescending(x => x.TongTienTichLuy).ToList());
                }
                else if (value == "Tên KH từ A->Z")
                {
                    DisplayList = new ObservableCollection<KhachHang>(ListKhachHang.OrderBy(x => x.HoTen).ToList());
                }
                else if (value == "Tên KH từ Z->A")
                {
                    DisplayList = new ObservableCollection<KhachHang>(ListKhachHang.OrderByDescending(x => x.HoTen).ToList());
                }

            }
        }

        private string _Keyword;
        public string Keyword { get => _Keyword; set
            {
                _Keyword = value; OnPropertyChanged();
                if (_Keyword == "")
                {
                    for (int i = DisplayList.Count - 1; i >= 0; i--)
                        DisplayList.RemoveAt(i);
                    foreach (KhachHang kh in ListKhachHang)
                    {
                        var a = new KhachHang() { IDKhachHang = kh.IDKhachHang, HoTen = kh.HoTen, NamSinh = kh.NamSinh, GioiTinh = kh.GioiTinh, SoDienThoai = kh.SoDienThoai, Email = kh.Email, TongTienTichLuy = kh.TongTienTichLuy };
                        DisplayList.Add(a);
                    }
                }
            }
        }

        public ICommand Them { get; set; }
        public ICommand TimKiem { get; set; }
        public ICommand CapNhat { get; set; }
        public ICommand Xoa { get; set; }
        public QuanLyKhachHangViewModel()
        {
            ListKhachHang = new ObservableCollection<KhachHang>(DataProvider.GetInstance.DB.KhachHangs);

            DisplayList = new ObservableCollection<KhachHang>();
            foreach (KhachHang kh in ListKhachHang)
                DisplayList.Add(kh);

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
                foreach (KhachHang kh in ListKhachHang)
                {
                    if (kh.HoTen.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || kh.HoTen.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || kh.IDKhachHang.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var a = new KhachHang() { IDKhachHang = kh.IDKhachHang, HoTen = kh.HoTen, NamSinh = kh.NamSinh, GioiTinh = kh.GioiTinh, SoDienThoai = kh.SoDienThoai, Email = kh.Email, TongTienTichLuy = kh.TongTienTichLuy };
                        DisplayList.Add(a);
                    }
                }
            });

            Them = new RelayCommand<Window>((p) => true, (p) =>
            {
                ThemKhachHangWindow window = new ThemKhachHangWindow();
                window.Owner = p;
                window.ShowDialog();
            });

            CapNhat = new RelayCommand<Window>((p) => {
                if (SelectedItem != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                KhachHang tmp = DataProvider.GetInstance.DB.KhachHangs.Where((x) => x.IDKhachHang == SelectedItem.IDKhachHang).FirstOrDefault();
                CapNhatKhachHangWindow window = new CapNhatKhachHangWindow(tmp);
                window.Owner = p;
                window.ShowDialog();
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
                    var Customer = DataProvider.GetInstance.DB.KhachHangs.Where(x => x.IDKhachHang == SelectedItem.IDKhachHang).SingleOrDefault();
                    DataProvider.GetInstance.DB.KhachHangs.Remove(Customer);
                    DataProvider.GetInstance.DB.SaveChanges();
                    MessageBox.Show("Đã xoá thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
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
                // TO-DO: Need a more clean way
               
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    MessageBox.Show("Dữ liệu này không thể xóa", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        public void LoadData()
        {
            ListKhachHang = new ObservableCollection<KhachHang>(DataProvider.GetInstance.DB.KhachHangs);

            DisplayList = new ObservableCollection<KhachHang>();
            foreach (KhachHang kh in ListKhachHang)
                DisplayList.Add(kh);
        }
    }
}
