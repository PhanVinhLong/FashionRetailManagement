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
    public class QuanLyKichCoViewModel: BaseViewModel
    {
        private ObservableCollection<KichCo> _ListKichCo;
        public ObservableCollection<KichCo> ListKichCo { get => _ListKichCo; set { _ListKichCo = value; OnPropertyChanged(); } }

        private ObservableCollection<KichCo> _DisplayList;
        public ObservableCollection<KichCo> DisplayList { get => _DisplayList; set { _DisplayList = value; OnPropertyChanged(); } }

        private KichCo _SelectedItem;
        public KichCo SelectedItem
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
                    foreach (KichCo kc in ListKichCo)
                    {
                        var a = new KichCo() { IDKichCo = kc.IDKichCo, TenKichCo = kc.TenKichCo };
                        DisplayList.Add(a);
                    }
                }
            }
        }

        public ICommand TimKiem { get; set; }
        public ICommand Them { get; set; }
        public ICommand CapNhat { get; set; }
        public ICommand Xoa { get; set; }
        public QuanLyKichCoViewModel()
        {
            ListKichCo = new ObservableCollection<KichCo>(DataProvider.GetInstance.DB.KichCoes);

            DisplayList = new ObservableCollection<KichCo>();
            foreach (KichCo kc in ListKichCo)
                DisplayList.Add(kc);

            CapNhat = new RelayCommand<object>((p) => {
                if (SelectedItem != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                CapNhatKichCoWindow window = new CapNhatKichCoWindow(SelectedItem);
                window.Owner = (p as QuanLyKichCoWindow);
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

                foreach (KichCo kc in ListKichCo)
                {
                    if (kc.TenKichCo.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || kc.TenKichCo.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || kc.IDKichCo.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var a = new KichCo() { IDKichCo = kc.IDKichCo, TenKichCo = kc.TenKichCo };
                        DisplayList.Add(a);
                    }
                }
            });

            Them = new RelayCommand<Window>((p) => true, (p) =>
            {
                ThemKichCoWindow window = new ThemKichCoWindow();
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
                    var KC = DataProvider.GetInstance.DB.KichCoes.Where(x => x.IDKichCo == SelectedItem.IDKichCo).SingleOrDefault();
                    DataProvider.GetInstance.DB.KichCoes.Remove(KC);
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
                catch (InvalidOperationException ex)
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
            ListKichCo = new ObservableCollection<KichCo>(DataProvider.GetInstance.DB.KichCoes);

            DisplayList = new ObservableCollection<KichCo>();
            foreach (KichCo kc in ListKichCo)
                DisplayList.Add(kc);
        }
    }
}
