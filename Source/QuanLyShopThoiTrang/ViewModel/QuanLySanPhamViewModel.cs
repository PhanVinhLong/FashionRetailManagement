using DevExpress.Xpf.Core;
using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.View;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class QuanLySanPhamViewModel : BaseViewModel
    {

        private ObservableCollection<HienThiSanPham> _ListSanPham;
        public ObservableCollection<HienThiSanPham> ListSanPham { get => _ListSanPham; set { _ListSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<HienThiSanPham> _DisplayList;
        public ObservableCollection<HienThiSanPham> DisplayList { get => _DisplayList; set { _DisplayList = value; OnPropertyChanged(); } }

        private HienThiSanPham _SelectedItem;
        public HienThiSanPham SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        private string _Keyword;
        public string Keyword
        {
            get => _Keyword; set
            {
                _Keyword = value; OnPropertyChanged();
                if (_Keyword == "")
                {
                    for (int i = DisplayList.Count - 1; i >= 0; i--) DisplayList.RemoveAt(i);
                    foreach (HienThiSanPham htsp in ListSanPham)
                    {
                        var t = new SanPham() { IDSanPham = htsp.SanPham.IDSanPham, IDMauSac = htsp.SanPham.IDMauSac, IDKichCo = htsp.SanPham.IDKichCo, IDLoaiSanPham = htsp.SanPham.IDLoaiSanPham, DonGia = htsp.SanPham.DonGia };
                        var a = new HienThiSanPham() { SanPham = htsp.SanPham, MauSac = htsp.MauSac, KichCo = htsp.KichCo, LoaiSanPham = htsp.LoaiSanPham };
                        DisplayList.Add(a);
                    }
                }
            }
        }

        public ICommand TimKiem { get; set; }
        public ICommand Them { get; set; }
        public ICommand CapNhat { get; set; }
        public ICommand Xoa { get; set; }
        public QuanLySanPhamViewModel()
        {
            LoadData();

            Them = new RelayCommand<Window>((p) => true, (p) =>
            {
                ThemSanPhamWindow window = new ThemSanPhamWindow();
                window.Owner = p;
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

                foreach (HienThiSanPham htsp in ListSanPham)
                {
                    if (htsp.LoaiSanPham.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || htsp.LoaiSanPham.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || htsp.SanPham.IDSanPham.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var t = new SanPham() { IDSanPham = htsp.SanPham.IDSanPham, IDMauSac = htsp.SanPham.IDMauSac, IDKichCo = htsp.SanPham.IDKichCo, IDLoaiSanPham = htsp.SanPham.IDLoaiSanPham, DonGia = htsp.SanPham.DonGia };
                        var a = new HienThiSanPham() { SanPham = htsp.SanPham, MauSac = htsp.MauSac, KichCo = htsp.KichCo, LoaiSanPham = htsp.LoaiSanPham };
                        DisplayList.Add(a);
                    }
                }
            });

            CapNhat = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                CapNhatSanPhamWindow window = new CapNhatSanPhamWindow(SelectedItem.SanPham);
                window.Owner = (p as QuanLySanPhamWindow);
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
                    var NV = DataProvider.GetInstance.DB.SanPhams.Where(x => x.IDSanPham == SelectedItem.SanPham.IDSanPham).SingleOrDefault();
                    DataProvider.GetInstance.DB.SanPhams.Remove(NV);
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
            ListSanPham = new ObservableCollection<HienThiSanPham>();
            var tmp = DataProvider.GetInstance.DB.SanPhams;
            DisplayList = new ObservableCollection<HienThiSanPham>();
            foreach (var item in tmp)
            {
                HienThiSanPham tSP = new HienThiSanPham();
                KichCo tKichCo = DataProvider.GetInstance.DB.KichCoes.Where((p) => p.IDKichCo == item.IDKichCo).FirstOrDefault();
                MauSac tMauSac = DataProvider.GetInstance.DB.MauSacs.Where((p) => p.IDMauSac == item.IDMauSac).FirstOrDefault();
                LoaiSanPham tLoaiSanPham = DataProvider.GetInstance.DB.LoaiSanPhams.Where((p) => p.IDLoaiSanPham == item.IDLoaiSanPham).FirstOrDefault();
                tSP.SanPham = item;
                tSP.KichCo = tKichCo.TenKichCo;
                tSP.MauSac = tMauSac.TenMauSac;
                tSP.LoaiSanPham = tLoaiSanPham.TenLoaiSanPham;
                ListSanPham.Add(tSP);
            }
            foreach (var sp in ListSanPham)
                DisplayList.Add(sp);
        }
    }
}
