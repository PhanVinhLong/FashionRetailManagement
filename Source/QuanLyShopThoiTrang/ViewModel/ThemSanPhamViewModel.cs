using QuanLyShopThoiTrang.Model;
using QuanLyShopThoiTrang.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class ThemSanPhamViewModel : BaseViewModel
    {
        private SanPham _SanPham;
        public SanPham SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }

        private List<LoaiSanPham> _ListLoaiSanPham;
        public List<LoaiSanPham> ListLoaiSanPham { get => _ListLoaiSanPham; set { _ListLoaiSanPham = value; OnPropertyChanged(); } }

        private List<MauSac> _ListMauSac;
        public List<MauSac> ListMauSac { get => _ListMauSac; set { _ListMauSac = value; OnPropertyChanged(); } }

        private List<KichCo> _ListKichCo;
        public List<KichCo> ListKichCo { get => _ListKichCo; set { _ListKichCo = value; OnPropertyChanged(); } }

        private LoaiSanPham _SelectedLoaiSanPham;
        public LoaiSanPham SelectedLoaiSanPham { get => _SelectedLoaiSanPham; set { _SelectedLoaiSanPham = value; OnPropertyChanged(); SanPham.IDLoaiSanPham = SelectedLoaiSanPham.IDLoaiSanPham; } }
        public ICommand ThemCommand { get; set; }

        public ThemSanPhamViewModel()
        {
            ListLoaiSanPham = DataProvider.GetInstance.DB.LoaiSanPhams.ToList();
            ListMauSac = DataProvider.GetInstance.DB.MauSacs.ToList();
            ListKichCo = DataProvider.GetInstance.DB.KichCoes.ToList();

            SanPham = new SanPham();
            SanPham.IDSanPham = TaoIDSanPham();

            ThemCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (SanPham.IDKichCo == 0 || SanPham.IDLoaiSanPham == 0 || SanPham.IDMauSac == 0)
                    {
                        MessageBox.Show("Vui lòng kiểm tra thông tin đã nhập.", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        double a = SanPham.DonGia / 1000;
                        if ( a >= 1)
                            SanPham.DonGia /= 1000;
                        DataProvider.GetInstance.DB.SanPhams.Add(SanPham);
                        DataProvider.GetInstance.DB.SaveChanges();
                        MessageBox.Show("Đã thêm thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                        SanPham = new SanPham();
                        SanPham.IDSanPham = TaoIDSanPham();
                       
                        (p.Owner as QuanLySanPhamWindow).LoadData();
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

        public int TaoIDSanPham()
        {
            int IDSanPham = 1000000001;
            SanPham lstSP = DataProvider.GetInstance.DB.SanPhams.OrderByDescending(u => u.IDSanPham).FirstOrDefault();
            if (lstSP != null)
                IDSanPham = lstSP.IDSanPham + 1;
            return IDSanPham;
        }   
    }
}
