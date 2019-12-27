using DevExpress.Xpf.Core;
using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class ThemLoaiSanPhamViewModel : BaseViewModel
    {
        private LoaiSanPham _LoaiSanPham;
        public LoaiSanPham LoaiSanPham { get => _LoaiSanPham; set { _LoaiSanPham = value; OnPropertyChanged(); } }

        private List<NhaCungCap> _ListNhaCungCap;
        public List<NhaCungCap> ListNhaCungCap { get => _ListNhaCungCap; set { _ListNhaCungCap = value; OnPropertyChanged(); } }

        private NhaCungCap _SelectedNhaCungCap;
        public NhaCungCap SelectedNhaCungCap { get => _SelectedNhaCungCap; set { _SelectedNhaCungCap = value; OnPropertyChanged(); LoaiSanPham.IDNhaCungCap = SelectedNhaCungCap.IDNhaCungCap; } }
        public ICommand ThemCommand { get; set; }
        public ThemLoaiSanPhamViewModel()
        {
            ListNhaCungCap = DataProvider.GetInstance.DB.NhaCungCaps.ToList();
            LoaiSanPham = new LoaiSanPham();
         
            LoaiSanPham.IDLoaiSanPham = TaoID();

            ThemCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (LoaiSanPham.TenLoaiSanPham == "" || LoaiSanPham.IDNhaCungCap == 0)
                    {
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Vui lòng nhập đầy đủ thông tin", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
                    }
                    else
                    {
                        DataProvider.GetInstance.DB.LoaiSanPhams.Add(LoaiSanPham);
                        DataProvider.GetInstance.DB.SaveChanges();
                        DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã thêm thành công", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
                        LoaiSanPham = new LoaiSanPham();
                        LoaiSanPham.IDLoaiSanPham = TaoID();
                        (p.Owner as QuanLyLoaiSanPhamWindow).LoadData();

                        p.Close();
                    }
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
            });
        }

        public int TaoID()
        {
            int ID = 10000001;
            LoaiSanPham.IDNhaCungCap = ListNhaCungCap.First().IDNhaCungCap;
            SelectedNhaCungCap = ListNhaCungCap.First();
            LoaiSanPham lstLSP = DataProvider.GetInstance.DB.LoaiSanPhams.OrderByDescending(u => u.IDLoaiSanPham).FirstOrDefault();
            if (lstLSP != null)
                ID = lstLSP.IDLoaiSanPham + 1;
            return ID;
        }

    }
}
