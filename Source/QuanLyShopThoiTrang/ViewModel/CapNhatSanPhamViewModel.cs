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
    public class CapNhatSanPhamViewModel : BaseViewModel
    {
        private SanPham _SanPham;
        public SanPham SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }

        private List<KichCo> _ListKichCo;
        public List<KichCo> ListKichCo { get => _ListKichCo; set { _ListKichCo = value; OnPropertyChanged(); } }

        private List<MauSac> _ListMauSac;
        public List<MauSac> ListMauSac { get => _ListMauSac; set { _ListMauSac = value; OnPropertyChanged(); } }

        private List<LoaiSanPham> _ListLoaiSanPham;
        public List<LoaiSanPham> ListLoaiSanPham { get => _ListLoaiSanPham; set { _ListLoaiSanPham = value; OnPropertyChanged(); } }

        public ICommand CapNhatCommand { get; set; }
        public ICommand XoaCommand { get; set; }
        public CapNhatSanPhamViewModel(SanPham sanPham)
        {
            ListKichCo = DataProvider.GetInstance.DB.KichCoes.ToList();
            ListMauSac = DataProvider.GetInstance.DB.MauSacs.ToList();
            ListLoaiSanPham = DataProvider.GetInstance.DB.LoaiSanPhams.ToList();

            SanPham = new SanPham { IDSanPham = sanPham.IDSanPham, IDKichCo = sanPham.IDKichCo, IDMauSac = sanPham.IDMauSac, IDLoaiSanPham = sanPham.IDLoaiSanPham, DonGia = sanPham.DonGia };

            CapNhatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    var sp = DataProvider.GetInstance.DB.SanPhams.Where(x => x.IDSanPham == sanPham.IDSanPham).SingleOrDefault();
                    sp.IDKichCo = SanPham.IDKichCo;
                    sp.IDMauSac = SanPham.IDMauSac;
                    sp.IDLoaiSanPham = SanPham.IDLoaiSanPham;
                    sp.DonGia = SanPham.DonGia;

                    DataProvider.GetInstance.DB.SaveChanges();
                    (p.Owner as QuanLySanPhamWindow).LoadData();
                    DXMessageBox.Show(caption: "THÔNG BÁO", messageBoxText: "Đã cập nhật thành công", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
                    p.Close();
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
    }
}
