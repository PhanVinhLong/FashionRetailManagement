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
    public class QuanLyNhanVienViewModel : BaseViewModel
    {
        private ObservableCollection<HienThiNhanVien> _ListNhanVien;
        public ObservableCollection<HienThiNhanVien> ListNhanVien { get => _ListNhanVien; set { _ListNhanVien = value; OnPropertyChanged(); } }

        private ObservableCollection<HienThiNhanVien> _DisplayList;
        public ObservableCollection<HienThiNhanVien> DisplayList { get => _DisplayList; set { _DisplayList = value; OnPropertyChanged(); } }

        private HienThiNhanVien _SelectedItem;
        public HienThiNhanVien SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        private string _Keyword;
        public string Keyword
        {
            get => _Keyword; set
            {
                _Keyword = value; OnPropertyChanged();
                if (_Keyword == "")
                {
                    for (int i = DisplayList.Count - 1; i >= 0; i--) DisplayList.RemoveAt(i);
                    foreach (HienThiNhanVien nv in ListNhanVien)
                    {
                        var a = new HienThiNhanVien() { NhanVien = nv.NhanVien, VaiTro = nv.VaiTro };
                        DisplayList.Add(a);
                    }
                }
            }
        }

        public ICommand TimKiem { get; set; }
        public ICommand Them { get; set; }
        public ICommand CapNhat { get; set; }
        public ICommand Xoa { get; set; }
        public QuanLyNhanVienViewModel()
        {
            LoadData();

            TimKiem = new RelayCommand<object>((p) => Keyword!=null, (p) =>
            {
                for (int i = DisplayList.Count - 1; i >= 0; i--)
                    DisplayList.RemoveAt(i);

                foreach (HienThiNhanVien nv in ListNhanVien)
                {
                    if (nv.NhanVien.HoTen.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || nv.NhanVien.HoTen.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0 || nv.NhanVien.IDNhanVien.ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var a = new HienThiNhanVien() { NhanVien = nv.NhanVien, VaiTro = nv.VaiTro };
                        DisplayList.Add(a);
                    }
                }
            });

            Them = new RelayCommand<object>((p) => true, (p) =>
            {
                ThemNhanVienWindow window = new ThemNhanVienWindow();
                window.Owner = (p as QuanLyNhanVienWindow);
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
                CapNhatNhanVienWindow window = new CapNhatNhanVienWindow(SelectedItem.NhanVien);
                window.Owner = (p as QuanLyNhanVienWindow);
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
                    var NV = DataProvider.GetInstance.DB.NhanViens.Where(x => x.IDNhanVien == SelectedItem.NhanVien.IDNhanVien).SingleOrDefault();
                    DataProvider.GetInstance.DB.NhanViens.Remove(NV);
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
            ListNhanVien = new ObservableCollection<HienThiNhanVien>();
            DisplayList = new ObservableCollection<HienThiNhanVien>();
            var tmp = DataProvider.GetInstance.DB.NhanViens;

            foreach (var item in tmp)
            {
                HienThiNhanVien tmpNhanVien = new HienThiNhanVien();
                VaiTro tmpVaiTro = DataProvider.GetInstance.DB.VaiTroes.Where((p) => p.IDVaiTro == item.IDVaiTro).FirstOrDefault();
                NhanVien tmpNV = new NhanVien
                {
                    IDNhanVien = item.IDNhanVien,
                    IDVaiTro = item.IDVaiTro,
                    MatKhau = item.MatKhau,
                    HoTen = item.HoTen,
                    ChungMinhNhanDan = item.ChungMinhNhanDan,
                    SoDienThoai = item.SoDienThoai,
                    Email = item.Email,
                    DiaChi = item.DiaChi,
                    GioiTinh = item.GioiTinh
                };
                tmpNhanVien.NhanVien = item;
                tmpNhanVien.VaiTro = tmpVaiTro.TenVaiTro;
                ListNhanVien.Add(tmpNhanVien);
            }
            foreach (var nv in ListNhanVien)
                DisplayList.Add(nv);
        }
    }
}
