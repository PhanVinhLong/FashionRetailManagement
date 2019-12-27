using DevExpress.Xpf.Core;
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
    class QuanLyMauSacViewModel : BaseViewModel
    {
        private ObservableCollection<MauSac> _ListMauSac;
        public ObservableCollection<MauSac> ListMauSac { get => _ListMauSac; set { _ListMauSac = value; OnPropertyChanged(); } }

        private ObservableCollection<MauSac> _DisplayList;
        public ObservableCollection<MauSac> DisplayList { get => _DisplayList; set { _DisplayList = value; OnPropertyChanged(); } }

        private MauSac _SelectedItem;
        public MauSac SelectedItem
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
                    foreach (MauSac kc in ListMauSac)
                    {
                        var a = new MauSac() { IDMauSac = kc.IDMauSac, TenMauSac = kc.TenMauSac };
                        DisplayList.Add(a);
                    }
                }
            }
        }

        public ICommand TimKiem { get; set; }
        public ICommand Them { get; set; }
        public ICommand CapNhat { get; set; }
        public ICommand Xoa { get; set; }
        public QuanLyMauSacViewModel()
        {
            LoadData();

            CapNhat = new RelayCommand<object>((p) => {
                if (SelectedItem != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                CapNhatMauSacWindow window = new CapNhatMauSacWindow(SelectedItem);
                window.Owner = (p as QuanLyMauSacWindow);
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

                foreach (MauSac kc in ListMauSac)
                {
                    if (kc.TenMauSac.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || kc.TenMauSac.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || kc.IDMauSac.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var a = new MauSac() { IDMauSac = kc.IDMauSac, TenMauSac = kc.TenMauSac };
                        DisplayList.Add(a);
                    }
                }
            });

            Them = new RelayCommand<Window>((p) => true, (p) =>
            {
                ThemMauSacWindow window = new ThemMauSacWindow();
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
                    var MS = DataProvider.GetInstance.DB.MauSacs.Where(x => x.IDMauSac == SelectedItem.IDMauSac).SingleOrDefault();
                    DataProvider.GetInstance.DB.MauSacs.Remove(MS);
                    DataProvider.GetInstance.DB.SaveChanges();
                    LoadData();
                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã xoá thành công", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
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
                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã xảy ra lỗi", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Dữ liệu này không thể xóa", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Dữ liệu này không thể xóa", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                }
            });
        }
        public void LoadData()
        {
            ListMauSac = new ObservableCollection<MauSac>(DataProvider.GetInstance.DB.MauSacs);

            DisplayList = new ObservableCollection<MauSac>();
            foreach (MauSac kc in ListMauSac)
                DisplayList.Add(kc);
        }
    }
}
