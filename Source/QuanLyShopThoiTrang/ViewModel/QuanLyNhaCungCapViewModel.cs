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
    public class QuanLyNhaCungCapViewModel : BaseViewModel
    {
        private ObservableCollection<NhaCungCap> _ListNhaCungCap;
        public ObservableCollection<NhaCungCap> ListNhaCungCap { get => _ListNhaCungCap; set { _ListNhaCungCap = value; OnPropertyChanged(); } }

        private ObservableCollection<NhaCungCap> _DisplayList;
        public ObservableCollection<NhaCungCap> DisplayList { get => _DisplayList; set { _DisplayList = value; OnPropertyChanged(); } }

        private NhaCungCap _SelectedItem;
        public NhaCungCap SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        private string _Keyword;
        public string Keyword
        {
            get => _Keyword; set
            {
                _Keyword = value; OnPropertyChanged();
                if (_Keyword == "")
                {
                    for (int i = DisplayList.Count - 1; i >= 0; i--)
                        DisplayList.RemoveAt(i);
                    foreach (NhaCungCap ncc in ListNhaCungCap)
                    {
                       
                        DisplayList.Add(ncc);
                    }
                }
            }
        }

        public ICommand TimKiem { get; set; }
        public ICommand Them { get; set; }
        public ICommand CapNhat { get; set; }
        public ICommand Xoa { get; set; }
        public QuanLyNhaCungCapViewModel()
        {
            LoadData();

            TimKiem = new RelayCommand<object>((p) => Keyword!=null, (p) =>
            {
                for (int i = DisplayList.Count - 1; i >= 0; i--)
                    DisplayList.RemoveAt(i);

                Keyword = Keyword?.Trim();

                foreach (NhaCungCap ncc in ListNhaCungCap)
                {
                    if (ncc.TenNhaCungCap.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || ncc.TenNhaCungCap.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || ncc.IDNhaCungCap.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var a = new NhaCungCap() { IDNhaCungCap = ncc.IDNhaCungCap, TenNhaCungCap = ncc.TenNhaCungCap, SoDienThoai = ncc.SoDienThoai, Email = ncc.Email, DiaChi = ncc.DiaChi };
                        DisplayList.Add(a);
                    }
                }
            });

            Them = new RelayCommand<object>((p) => true, (p) =>
            {
                ThemNhaCungCapWindow window = new ThemNhaCungCapWindow();
                window.Owner = (p as QuanLyNhaCungCapWindow);
                window.ShowDialog();
            });


            CapNhat = new RelayCommand<object>((p) =>
            {
                if (SelectedItem != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                NhaCungCap tmp = DataProvider.GetInstance.DB.NhaCungCaps.Where((x) => x.IDNhaCungCap == SelectedItem.IDNhaCungCap).FirstOrDefault();
                CapNhatNhaCungCapWindow window = new CapNhatNhaCungCapWindow(tmp);
                window.Owner = (p as QuanLyNhaCungCapWindow);
                window.ShowDialog();
            });

            Xoa = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                try
                {
                    var NCC = DataProvider.GetInstance.DB.NhaCungCaps.Where(x => x.IDNhaCungCap == SelectedItem.IDNhaCungCap).SingleOrDefault();
                    DataProvider.GetInstance.DB.NhaCungCaps.Remove(NCC);
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
                catch(InvalidOperationException ex)
                {
                    MessageBox.Show("Dữ liệu này không thể xóa", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
                 catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    MessageBox.Show("Dữ liệu này không thể xóa", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
        public void LoadData()
        {
            ListNhaCungCap = new ObservableCollection<NhaCungCap>(DataProvider.GetInstance.DB.NhaCungCaps);

            DisplayList = new ObservableCollection<NhaCungCap>();
            foreach (NhaCungCap ncc in ListNhaCungCap)
                DisplayList.Add(ncc);
        }
    }
}
