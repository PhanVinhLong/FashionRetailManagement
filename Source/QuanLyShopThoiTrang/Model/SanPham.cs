//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyShopThoiTrang.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            this.ChiTietPhieuNhaps = new HashSet<ChiTietPhieuNhap>();
            this.ChiTietPhieuTras = new HashSet<ChiTietPhieuTra>();
        }
    
        public int IDSanPham { get; set; }
        public int IDMauSac { get; set; }
        public int IDKichCo { get; set; }
        public int IDLoaiSanPham { get; set; }
        public double DonGia { get; set; }
        public int SoLuongTon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuTra> ChiTietPhieuTras { get; set; }
        public virtual KichCo KichCo { get; set; }
        public virtual LoaiSanPham LoaiSanPham { get; set; }
        public virtual MauSac MauSac { get; set; }
    }
}
