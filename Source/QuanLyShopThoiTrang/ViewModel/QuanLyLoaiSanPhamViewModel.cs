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
    public class QuanLyLoaiSanPhamViewModel : BaseViewModel
    {
        private ObservableCollection<HienThiLoaiSanPham> _ListLoaiSanPham;
        public ObservableCollection<HienThiLoaiSanPham> ListLoaiSanPham { get => _ListLoaiSanPham; set { _ListLoaiSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<HienThiLoaiSanPham> _DisplayList;
        public ObservableCollection<HienThiLoaiSanPham> DisplayList { get => _DisplayList; set { _DisplayList = value; OnPropertyChanged(); } }

        private HienThiLoaiSanPham _SelectedItem;
        public HienThiLoaiSanPham SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        private string _Keyword;
        public string Keyword
        {
            get => _Keyword; set
            {
                _Keyword = value; OnPropertyChanged();
                if (_Keyword == "")
                {
                    for (int i = DisplayList.Count - 1; i >= 0; i--) DisplayList.RemoveAt(i);
                    foreach (HienThiLoaiSanPham nv in ListLoaiSanPham)
                    {
                        var a = new HienThiLoaiSanPham() { LoaiSanPham = nv.LoaiSanPham, NhaCungCap = nv.NhaCungCap };
                        DisplayList.Add(a);
                    }
                }
            }
        }

        public ICommand TimKiem { get; set; }
        public ICommand Them { get; set; }
        public ICommand CapNhat { get; set; }
        public ICommand Xoa { get; set; }
        public QuanLyLoaiSanPhamViewModel()
        {
            LoadData();

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

                foreach (HienThiLoaiSanPham nv in ListLoaiSanPham)
                {
                    if (nv.LoaiSanPham.TenLoaiSanPham.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || nv.LoaiSanPham.TenLoaiSanPham.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || nv.LoaiSanPham.IDLoaiSanPham.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var tLoaiSanPham = new LoaiSanPham { IDLoaiSanPham = nv.LoaiSanPham.IDLoaiSanPham, IDNhaCungCap = nv.LoaiSanPham.IDNhaCungCap, TenLoaiSanPham = nv.LoaiSanPham.TenLoaiSanPham };
                        var a = new HienThiLoaiSanPham() { LoaiSanPham = tLoaiSanPham, NhaCungCap = nv.NhaCungCap };
                        DisplayList.Add(a);
                    }
                }
            });

            Them = new RelayCommand<Window>((p) => true, (p) =>
            {
                ThemLoaiSanPhamWindow window = new ThemLoaiSanPhamWindow();
                window.Owner = p;
                window.ShowDialog();
            });


            CapNhat = new RelayCommand<object>((p) => {
                if (SelectedItem != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                CapNhatLoaiSanPhamWindow window = new CapNhatLoaiSanPhamWindow(SelectedItem.LoaiSanPham);
                window.Owner = (p as QuanLyLoaiSanPhamWindow);
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
                    var NV = DataProvider.GetInstance.DB.LoaiSanPhams.Where(x => x.IDLoaiSanPham == SelectedItem.LoaiSanPham.IDLoaiSanPham).SingleOrDefault();
                    DataProvider.GetInstance.DB.LoaiSanPhams.Remove(NV);
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
            ListLoaiSanPham = new ObservableCollection<HienThiLoaiSanPham>();
            var tmp = DataProvider.GetInstance.DB.LoaiSanPhams;
            DisplayList = new ObservableCollection<HienThiLoaiSanPham>();
            foreach (var item in tmp)
            {
                HienThiLoaiSanPham tmpLoaiSanPham = new HienThiLoaiSanPham();
                NhaCungCap tmpNhaCungCap = DataProvider.GetInstance.DB.NhaCungCaps.Where((p) => p.IDNhaCungCap == item.IDNhaCungCap).FirstOrDefault();
                tmpLoaiSanPham.LoaiSanPham = item;
                tmpLoaiSanPham.NhaCungCap = tmpNhaCungCap.TenNhaCungCap;
                ListLoaiSanPham.Add(tmpLoaiSanPham);
            }
            foreach (var nv in ListLoaiSanPham)
                DisplayList.Add(nv);
        }
    }
}
