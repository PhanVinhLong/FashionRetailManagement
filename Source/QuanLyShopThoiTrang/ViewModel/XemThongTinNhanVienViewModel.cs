using QuanLyShopThoiTrang.Model;
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
    public class XemThongTinNhanVienViewModel : BaseViewModel
    {
        private NhanVien _nhanvien;
        public NhanVien nhanvien { get => _nhanvien; set { _nhanvien = value; OnPropertyChanged(); } }

        private bool _AllowEdit;
        public bool AllowEdit { get => _AllowEdit; set { _AllowEdit = value; OnPropertyChanged(); } }

        private List<VaiTro> _ListVaiTro;
        public List<VaiTro> ListVaiTro { get => _ListVaiTro; set { _ListVaiTro = value; OnPropertyChanged(); } }

        private VaiTro _SelectedVaiTro;
        public VaiTro SelectedVaiTro { get => _SelectedVaiTro; set { _SelectedVaiTro = value; OnPropertyChanged(); nhanvien.IDVaiTro = SelectedVaiTro.IDVaiTro; } }

        public ICommand CapNhatCommand { get; set; }

        public XemThongTinNhanVienViewModel(NhanVien nv, bool IsEdit)
        {
            nhanvien = nv;
            AllowEdit = IsEdit;
            SelectedVaiTro = nhanvien.VaiTro;
            ListVaiTro = DataProvider.GetInstance.DB.VaiTroes.ToList();


            CapNhatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    var nMS = DataProvider.GetInstance.DB.NhanViens.Where(x => x.IDNhanVien == nhanvien.IDNhanVien).SingleOrDefault();
                    nMS.HoTen = nhanvien.HoTen;
                    nMS.GioiTinh = nhanvien.GioiTinh;
                    nMS.IDVaiTro = SelectedVaiTro.IDVaiTro;
                    nMS.SoDienThoai = nhanvien.SoDienThoai;
                    nMS.Email = nhanvien.Email;
                    nMS.DiaChi = nhanvien.DiaChi;
                    nMS.ChungMinhNhanDan = nhanvien.ChungMinhNhanDan;

                    DataProvider.GetInstance.DB.SaveChanges();
                    MessageBox.Show("Đã cập nhật thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    MessageBox.Show("Đã xảy ra lỗi", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
    }
}
