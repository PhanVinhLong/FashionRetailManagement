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
    public class QuanLyPhieuNhapViewModel : BaseViewModel
    {
        public ObservableCollection<object> DisplayList { get; private set; } = new ObservableCollection<object>();
        private QLShopEntities Database = DataProvider.GetInstance.DB;

        public ICommand Them { get; set; }
        public ICommand TimKiem { get; set; }
        public ICommand CapNhat { get; set; }
        public ICommand Xoa { get; set; }

        public void LoadData()
        {
            var phieuNhaps = DataProvider.GetInstance.DB.PhieuNhaps;

            // Assign to DisplayList
            foreach (var item in phieuNhaps)
            {
                var TenNhanVien = Database.NhanViens.Where(obj => obj.IDNhanVien == item.IDNhanVien).SingleOrDefault().HoTen;
                var TenNhaCungCap = Database.NhaCungCaps.Where(obj => obj.IDNhaCungCap == item.IDNhaCungCap).SingleOrDefault().TenNhaCungCap;

                DisplayList.Add(new
                {
                    ID = item.IDPhieuNhap,
                    NhanVien = TenNhanVien,
                    NhaCungCap = TenNhaCungCap,
                    NgayNhap = item.NgayNhap,
                    TinhTrang = item.DaNhapVaoKho ? "Đã vào kho" : "Chưa vào kho"
                });
            }
        }

        public QuanLyPhieuNhapViewModel()
        {

        }

        public QuanLyPhieuNhapViewModel(NhanVien nhanVien)
        {
                // Load data

                
               LoadData();
                // Setup Commands
                SetupCommands(nhanVien);
            
        }

        //public QuanLyPhieuNhapViewModel()
        //{
        //    // Load data
        //    var phieuNhaps = DataProvider.GetInstance.DB.PhieuNhaps;

        //    // Assign to DisplayList
        //    foreach (var item in phieuNhaps)
        //    {
        //        var TenNhanVien = Database.NhanViens.Where(obj => obj.IDNhanVien == item.IDNhanVien).SingleOrDefault().HoTen;
        //        var TenNhaCungCap = Database.NhaCungCaps.Where(obj => obj.IDNhaCungCap == item.IDNhaCungCap).SingleOrDefault().TenNhaCungCap;

        //        DisplayList.Add(new
        //        {
        //            ID = item.IDPhieuNhap,
        //            NhanVien = TenNhanVien,
        //            NhaCungCap = TenNhaCungCap,
        //            NgayNhap = item.NgayNhap,
        //            TinhTrang = item.DaNhapVaoKho ? "Đã vào kho" : "Chưa vào kho"
        //        });

        //        // Setup Commands
        //        //SetupCommands(nhanVien);
        //    }
        //}

        public void SetupCommands(NhanVien nhanVien)
        {
            Them = new RelayCommand<Window>((p) => true, (p) =>
            {
                ThemPhieuNhapWindow window = new ThemPhieuNhapWindow(nhanVien);
                window.Owner = p;
                window.ShowDialog();
                LoadData();

                //for (int i = 0; i < DisplayList.Count; i++)
                //    DisplayList.RemoveAt(i);

                //var phieuNhaps = DataProvider.GetInstance.DB.PhieuNhaps;

                //// Assign to DisplayList
                //foreach (var item in phieuNhaps)
                //{
                //    var TenNhanVien = Database.NhanViens.Where(obj => obj.IDNhanVien == item.IDNhanVien).SingleOrDefault().HoTen;
                //    var TenNhaCungCap = Database.NhaCungCaps.Where(obj => obj.IDNhaCungCap == item.IDNhaCungCap).SingleOrDefault().TenNhaCungCap;

                //    DisplayList.Add(new
                //    {
                //        ID = item.IDPhieuNhap,
                //        NhanVien = TenNhanVien,
                //        NhaCungCap = TenNhaCungCap,
                //        NgayNhap = item.NgayNhap,
                //        TinhTrang = item.DaNhapVaoKho ? "Đã vào kho" : "Chưa vào kho"
                //    });

                //}
            });

           

        }
    }
}
