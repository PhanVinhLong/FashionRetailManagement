using QuanLyShopThoiTrang.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using QuanLyShopThoiTrang.View;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class ThemPhieuNhapViewModel : BaseViewModel
    {

        private int _SoLuong;
        public int SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }


        private int _GiaNhap;
        public int GiaNhap { get => _GiaNhap; set { _GiaNhap = value; OnPropertyChanged(); } }

        // Shortcut for Database variable
        private QLShopEntities Database = DataProvider.GetInstance.DB;

        // All the list
        public ObservableCollection<object> ListSanPham { get; set; } = new ObservableCollection<object>();       // For displaying
        public ObservableCollection<object> ListSanPhamNhap { get; set; } = new ObservableCollection<object>();   // For displaying
        public List<NhaCungCap> ListNhaCungCap { get; set; } = new List<NhaCungCap>();
        public List<MauSac> ListMauSac { get; set; } = new List<MauSac>();
        public List<LoaiSanPham> ListLoaiSanPham { get; set; } = new List<LoaiSanPham>();
        public List<KichCo> ListKichCo { get; set; } = new List<KichCo>();
        public List<object> AllSanPham;     // For keeping track all the object

        public MauSac SelectedMauSac { get; set; }
        public KichCo SelectedKichCo { get; set; }
        public LoaiSanPham SelectedLoaiSanPham { get; set; }
        public object SelectedItem { get; set; }
        public object SelectedSanPham { get; set; }
        public PhieuNhap PhieuNhap { get; set; }

        public NhaCungCap SelectedNCC { get; set; }

        // Some props for two way binding
        public String InputTen { get; set; } = "";
        public String InputID { get; set; } = "";



        public ICommand TimKiem { get; set; } = new RelayCommand<Window>((p) => { return true; },
                (p) =>
                {
                    Console.Write(p);
                }
             );
        public ICommand ThemVaoPhieuCommand { get; set; }
        public ICommand LuuPhieuCommand { get; set; }
        public ICommand CapNhat { get; set; }
        public ICommand Xoa { get; set; }
        public ICommand FilterListSanPhamCommand { get; set; }
        public ICommand XoaSanPham { get; set; }
        public ICommand BrowseCommand { get; set; }

        public ThemPhieuNhapViewModel()
        {

        }
            public ThemPhieuNhapViewModel(NhanVien nhanVien)
        {
            // Load data
            ListMauSac = Database.MauSacs.ToList();
            ListLoaiSanPham = Database.LoaiSanPhams.ToList();
            ListKichCo = Database.KichCoes.ToList();
            ListNhaCungCap = Database.NhaCungCaps.ToList();
            PhieuNhap = new PhieuNhap();
            PhieuNhap.NgayNhap = DateTime.Now;
            PhieuNhap.NhanVien = nhanVien;

            foreach (var sp in Database.SanPhams)
            {
                ListSanPham.Add(new
                {
                    SanPham = sp,
                    LoaiSanPham = ListLoaiSanPham.Where(value => value.IDLoaiSanPham == sp.IDLoaiSanPham).SingleOrDefault(),
                    KichCo = ListKichCo.Where(value => value.IDKichCo == sp.IDKichCo).SingleOrDefault(),
                    MauSac = ListMauSac.Where(value => value.IDMauSac == sp.IDMauSac).SingleOrDefault(),
                });
            }
            AllSanPham = new List<object>(ListSanPham);

            // Setup the command
            SetupCommands();
        }

        public void SetupCommands()
        {
            FilterListSanPhamCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                // Declare the anonymous type that we are using
                // for hold the display data of San Pham
                var typeHolder = new
                {
                    SanPham = new SanPham(),
                    LoaiSanPham = new LoaiSanPham(),
                    KichCo = new KichCo(),
                    MauSac = new MauSac(),
                };

                // Clear the list
                ListSanPham.Clear();

                // Filter the ListSanPham (the displaying one)
                foreach (var sp in AllSanPham)
                {
                    // Cast to the type of the displaying list
                    var sanPham = CastToType(typeHolder, sp);

                    // Some condition for filtering
                    var matchedMauSac = (SelectedMauSac == null) ? true :
                                       (sanPham.MauSac.IDMauSac == SelectedMauSac.IDMauSac);
                    var matchedKichCo = (SelectedKichCo == null) ? true :
                                       (sanPham.KichCo.IDKichCo == SelectedKichCo.IDKichCo);
                    var matchedLoaiSanPham = (SelectedLoaiSanPham == null) ? true :
                                       (sanPham.LoaiSanPham.IDLoaiSanPham == SelectedLoaiSanPham.IDLoaiSanPham);
                    var matchedID = InputID == "" ? true :
                       sanPham.SanPham.IDSanPham.ToString().IndexOf(InputID) == 0;
                    var matchedTen = InputTen == "" ? true :
                       sanPham.LoaiSanPham.TenLoaiSanPham.IndexOf(InputTen, StringComparison.OrdinalIgnoreCase) >= 0;

                    if (matchedKichCo && matchedLoaiSanPham && matchedMauSac && matchedID && matchedTen)
                        ListSanPham.Add(sanPham);
                }
            });


            ThemVaoPhieuCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                if (SoLuong <= 0)
                {
                    System.Windows.MessageBox.Show("Vui lòng nhập số lượng", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (GiaNhap <= 0)
                {
                    System.Windows.MessageBox.Show("Vui lòng nhập giá nhập", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {

                    var typeHolder = new
                    {
                        SanPham = new SanPham(),
                        LoaiSanPham = new LoaiSanPham(),
                        KichCo = new KichCo(),
                        MauSac = new MauSac(),
                    };
                    var selectedItem = CastToType(typeHolder, SelectedItem);


                    var sp = new
                    {
                        SanPham = selectedItem.SanPham,
                        LoaiSanPham = selectedItem.LoaiSanPham,
                        KichCo = selectedItem.KichCo,
                        MauSac = selectedItem.MauSac,
                    };
                    sp.SanPham.SoLuongTon = SoLuong;
                    sp.SanPham.DonGia = GiaNhap;
                    ListSanPhamNhap.Add(sp);
                    SoLuong = 0;
                    GiaNhap = 0;

                }
            });

            BrowseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {

                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string FilePath = ofd.FileName;

                    if (FilePath.Contains(".xls") == false)
                    {
                        System.Windows.MessageBox.Show("Không thể mở file", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        ObservableCollection<int> l = new ObservableCollection<int>();
                        if (DocFile(FilePath, ref l) == true)
                        {

                            int i = 0;
                            var typeHolder = new
                            {
                                SanPham = new SanPham(),
                                LoaiSanPham = new LoaiSanPham(),
                                KichCo = new KichCo(),
                                MauSac = new MauSac(),
                            };

                            while (i < l.Count)
                            {
                                int id = l.ElementAt(i);
                                SanPham spp = Database.SanPhams.Where(x => x.IDSanPham == id).SingleOrDefault();
                                if (spp != null)
                                {
                                    var sp = new
                                    {
                                        SanPham = spp,
                                        LoaiSanPham = ListLoaiSanPham.Where(value => value.IDLoaiSanPham == spp.IDLoaiSanPham).SingleOrDefault(),
                                        KichCo = ListKichCo.Where(value => value.IDKichCo == spp.IDKichCo).SingleOrDefault(),
                                        MauSac = ListMauSac.Where(value => value.IDMauSac == spp.IDMauSac).SingleOrDefault(),
                                    };
                                    sp.SanPham.SoLuongTon = l.ElementAt(i + 1);
                                    sp.SanPham.DonGia = l.ElementAt(i + 2);
                                    ListSanPhamNhap.Add(sp);
                                }
                                i = i + 3;

                            }

                        }
                    }
                }


            });

            LuuPhieuCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                try
                {
                    if (SelectedNCC == null || ListSanPhamNhap.Count == 0)
                    {
                        System.Windows.MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        PhieuNhap.IDPhieuNhap = TaoIDPhieuNhap();
                        PhieuNhap.IDNhaCungCap = SelectedNCC.IDNhaCungCap;
                        PhieuNhap.IDNhanVien = PhieuNhap.NhanVien.IDNhanVien;
                        DataProvider.GetInstance.DB.PhieuNhaps.Add(PhieuNhap);
                        DataProvider.GetInstance.DB.SaveChanges();

                        // Type holder for anonymous casting
                        var typeHolder = new
                        {
                            SanPham = new SanPham(),
                            LoaiSanPham = new LoaiSanPham(),
                            KichCo = new KichCo(),
                            MauSac = new MauSac(),
                        };

                        var listChiTietPhieuNhap = Database.ChiTietPhieuNhaps;
                        foreach (var item in ListSanPhamNhap)
                        {
                            var sp = CastToType(typeHolder, item);
                            var ctPhieuNhap = new ChiTietPhieuNhap();
                            ctPhieuNhap.DonGia = sp.SanPham.DonGia;
                            ctPhieuNhap.SoLuong = sp.SanPham.SoLuongTon;
                            ctPhieuNhap.IDPhieuNhap = PhieuNhap.IDPhieuNhap;
                            ctPhieuNhap.IDSanPham = sp.SanPham.IDSanPham;

                            listChiTietPhieuNhap.Add(ctPhieuNhap);
                        }
                        DataProvider.GetInstance.DB.SaveChanges();

                        System.Windows.MessageBox.Show("Đã thêm thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                      //  (p.Owner as QuanLyPhieuNhapWindow).LoadData();
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
                    System.Windows.MessageBox.Show("Đã xảy ra lỗi", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            XoaSanPham = new RelayCommand<Window>((p) => true, (p) =>
            {
                if (SelectedSanPham == null)
                {
                    System.Windows.MessageBox.Show("Abc");
                }
                else
                {
                    ListSanPhamNhap.Remove(SelectedSanPham);
                    SelectedSanPham = null;
                }

            });
        }

        // Type casting an instance of Object Type to an instance of Anonymous type
        public static T CastToType<T>(T typeHolder, Object obj)
        {
            return (T)obj;
        }

        public int TaoIDPhieuNhap()
        {
            int ID = 10000001;
            PhieuNhap phieuNhap = Database.PhieuNhaps.OrderByDescending(u => u.IDPhieuNhap).FirstOrDefault();
            if (phieuNhap != null)
                ID = phieuNhap.IDPhieuNhap + 1;
            return ID;
        }


        private bool DocFile(string filePath, ref ObservableCollection<int> l)
        {
            int index = 1;
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                for (int i = 3; i <= rowCount; i++)
                {
                    l.Add(Int32.Parse(xlRange.Cells[i, 1].Value2.ToString()));
                    l.Add(Int32.Parse(xlRange.Cells[i, 2].Value2.ToString()));
                    l.Add(Int32.Parse(xlRange.Cells[i, 3].Value2.ToString()));
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Đã xảy ra lỗi " + ex.Message + " ở dòng " + index.ToString(), "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
