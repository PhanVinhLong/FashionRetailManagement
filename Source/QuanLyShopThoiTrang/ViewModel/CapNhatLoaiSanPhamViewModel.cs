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
    public class CapNhatLoaiSanPhamViewModel: BaseViewModel
    {
        private LoaiSanPham _LoaiSanPham;
        public LoaiSanPham LoaiSanPham { get => _LoaiSanPham; set { _LoaiSanPham = value; OnPropertyChanged(); } }

        private List<NhaCungCap> _ListNhaCungCap;
        public List<NhaCungCap> ListNhaCungCap { get => _ListNhaCungCap; set { _ListNhaCungCap = value; OnPropertyChanged(); } }

        private NhaCungCap _SelectedNhaCungCap;
        public NhaCungCap SelectedNhaCungCap { get => _SelectedNhaCungCap; set { _SelectedNhaCungCap = value; OnPropertyChanged(); LoaiSanPham.IDNhaCungCap = SelectedNhaCungCap.IDNhaCungCap; } }
        public ICommand CapNhatCommand { get; set; }
        public ICommand XoaCommand { get; set; }
        public CapNhatLoaiSanPhamViewModel(LoaiSanPham loaiSanPham)
        {
            LoaiSanPham = new LoaiSanPham { IDLoaiSanPham = loaiSanPham.IDLoaiSanPham, IDNhaCungCap = loaiSanPham.IDNhaCungCap, TenLoaiSanPham = loaiSanPham.TenLoaiSanPham };
            ListNhaCungCap = DataProvider.GetInstance.DB.NhaCungCaps.ToList();
            SelectedNhaCungCap = DataProvider.GetInstance.DB.NhaCungCaps.Where((p) => p.IDNhaCungCap == LoaiSanPham.IDNhaCungCap).FirstOrDefault();

            CapNhatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (LoaiSanPham.TenLoaiSanPham == "" ||LoaiSanPham.IDNhaCungCap == 0)
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        var lsp = DataProvider.GetInstance.DB.LoaiSanPhams.Where(x => x.IDLoaiSanPham == LoaiSanPham.IDLoaiSanPham).SingleOrDefault();
                        lsp.TenLoaiSanPham = LoaiSanPham.TenLoaiSanPham;
                        lsp.IDNhaCungCap = SelectedNhaCungCap.IDNhaCungCap;
                        DataProvider.GetInstance.DB.SaveChanges();
                        (p.Owner as QuanLyLoaiSanPhamWindow).LoadData();
                        MessageBox.Show("Đã cập nhật thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    MessageBox.Show("Đã xảy ra lỗi", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
    }
}
