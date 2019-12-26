using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QuanLyShopThoiTrang.Model;
using DevExpress.Xpf.Printing;

/// <summary>
/// Summary description for HoaDonReport
/// </summary>
public class HoaDonReport : DevExpress.XtraReports.UI.XtraReport
{
    private DetailBand Detail;
    private XRTable tblMain;
    private XRTableRow detailTableRow;
    private XRTableCell txtMaSanPham;
    private XRTableCell txtTenSanPham;
    private XRTableCell unitPrice;
    private XRTableCell txtTong;
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private XRTable vendorContactsTable;
    private XRTableRow vendorContactsRow;
    private XRTableCell txtTenNhanVien;
    private XRTableCell txtTenKH2;
    private XRLine xrLine1;
    private GroupHeaderBand GroupHeader1;
    private XRLabel txtNgayLap;
    private XRPictureBox vendorLogo;
    private XRTable customerTable;
    private XRTableRow customerNameRow;
    private XRTableCell txtTenKH;
    private XRTableRow customerAddressRow;
    private XRTableCell txtMaKH;
    private XRTableRow customerCountryRow;
    private XRTableCell txtSoDienThoai;
    private SubBand SubBand1;
    private XRTable headerTable;
    private XRTableRow headerTableRow;
    private XRTableCell quantityCaption;
    private XRTableCell productNameCaption;
    private XRTableCell unitPriceCaption;
    private XRTableCell lineTotalCaption;
    private XRLabel invoiceLabel;
    private GroupFooterBand GroupFooter1;
    private XRTable totalTable;
    private XRTableRow totalRow;
    private XRTableCell totalCaption;
    private XRTableCell txtTongHoaDon;
    private XRControlStyle baseControlStyle;
    private XRTableCell txtSoLuong;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell6;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell1;
    private XRLabel xrLabel1;
    private XRLabel txtMaHoaDon;
    private XRLabel xrLabel2;
    private DevExpress.Persistent.Base.ReportsV2.CollectionDataSource dataSource;
    private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public HoaDonReport()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }
    public HoaDonReport(HoaDon hd)
    {
        InitializeComponent();

        double tongHD = 0;
        foreach(ChiTietHoaDon cthd in hd.ChiTietHoaDons)
        {
            tongHD += double.Parse(cthd.SoLuong.ToString()) * cthd.DonGia;
        }

        txtMaHoaDon.Text = hd.IDHoaDon.ToString();
        txtTenKH.Text = hd.KhachHang.HoTen.ToString();
        txtTenKH2.Text = hd.KhachHang.HoTen.ToString();
        txtMaKH.Text = hd.KhachHang.IDKhachHang.ToString();
        txtSoDienThoai.Text = hd.KhachHang.SoDienThoai.ToString();
        txtNgayLap.Text = hd.NgayHoaDon.ToShortDateString();
        txtTongHoaDon.Text = String.Format("{0:F3}", tongHD);
        txtTenNhanVien.Text = hd.NhanVien.HoTen;

        objectDataSource1.DataSource = hd.ChiTietHoaDons;
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tblMain = new DevExpress.XtraReports.UI.XRTable();
            this.detailTableRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.txtMaSanPham = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtTenSanPham = new DevExpress.XtraReports.UI.XRTableCell();
            this.unitPrice = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtSoLuong = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtTong = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.vendorContactsTable = new DevExpress.XtraReports.UI.XRTable();
            this.vendorContactsRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.txtTenNhanVien = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtTenKH2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.txtMaHoaDon = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.invoiceLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtNgayLap = new DevExpress.XtraReports.UI.XRLabel();
            this.vendorLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.customerTable = new DevExpress.XtraReports.UI.XRTable();
            this.customerNameRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.txtTenKH = new DevExpress.XtraReports.UI.XRTableCell();
            this.customerAddressRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.txtMaKH = new DevExpress.XtraReports.UI.XRTableCell();
            this.customerCountryRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.txtSoDienThoai = new DevExpress.XtraReports.UI.XRTableCell();
            this.SubBand1 = new DevExpress.XtraReports.UI.SubBand();
            this.headerTable = new DevExpress.XtraReports.UI.XRTable();
            this.headerTableRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.quantityCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.productNameCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.unitPriceCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lineTotalCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.totalTable = new DevExpress.XtraReports.UI.XRTable();
            this.totalRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.totalCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtTongHoaDon = new DevExpress.XtraReports.UI.XRTableCell();
            this.baseControlStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.dataSource = new DevExpress.Persistent.Base.ReportsV2.CollectionDataSource();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tblMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vendorContactsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblMain});
            this.Detail.HeightF = 30F;
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StyleName = "baseControlStyle";
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // tblMain
            // 
            this.tblMain.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
            this.tblMain.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.tblMain.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.tblMain.LocationFloat = new DevExpress.Utils.PointFloat(45.04174F, 0F);
            this.tblMain.Name = "tblMain";
            this.tblMain.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.detailTableRow});
            this.tblMain.Scripts.OnEvaluateBinding = "tblMain_EvaluateBinding";
            this.tblMain.SizeF = new System.Drawing.SizeF(604.9534F, 30F);
            this.tblMain.StylePriority.UseBorderColor = false;
            this.tblMain.StylePriority.UseBorders = false;
            this.tblMain.StylePriority.UseFont = false;
            // 
            // detailTableRow
            // 
            this.detailTableRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.txtMaSanPham,
            this.txtTenSanPham,
            this.unitPrice,
            this.txtSoLuong,
            this.txtTong});
            this.detailTableRow.Name = "detailTableRow";
            this.detailTableRow.Weight = 10.58D;
            // 
            // txtMaSanPham
            // 
            this.txtMaSanPham.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[IDSanPham]")});
            this.txtMaSanPham.Name = "txtMaSanPham";
            this.txtMaSanPham.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 0, 100F);
            this.txtMaSanPham.StylePriority.UsePadding = false;
            this.txtMaSanPham.StylePriority.UseTextAlignment = false;
            this.txtMaSanPham.Text = "1";
            this.txtMaSanPham.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.txtMaSanPham.Weight = 0.61173268975682882D;
            // 
            // txtTenSanPham
            // 
            this.txtTenSanPham.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SanPham].[LoaiSanPham].[TenLoaiSanPham]")});
            this.txtTenSanPham.Name = "txtTenSanPham";
            this.txtTenSanPham.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 5, 0, 100F);
            this.txtTenSanPham.StylePriority.UsePadding = false;
            this.txtTenSanPham.Text = "ProductName";
            this.txtTenSanPham.Weight = 1.1923239579309044D;
            // 
            // unitPrice
            // 
            this.unitPrice.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DonGia]")});
            this.unitPrice.Name = "unitPrice";
            this.unitPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 0, 100F);
            this.unitPrice.StylePriority.UsePadding = false;
            this.unitPrice.StylePriority.UseTextAlignment = false;
            this.unitPrice.Text = "1000";
            this.unitPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.unitPrice.TextFormatString = "{0} VNĐ";
            this.unitPrice.Weight = 0.523453057283604D;
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SoLuong]")});
            this.txtSoLuong.Multiline = true;
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 0, 100F);
            this.txtSoLuong.StylePriority.UsePadding = false;
            this.txtSoLuong.StylePriority.UseTextAlignment = false;
            this.txtSoLuong.Text = "2";
            this.txtSoLuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.txtSoLuong.Weight = 0.5521457051123575D;
            // 
            // txtTong
            // 
            this.txtTong.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DonGia]*[SoLuong]")});
            this.txtTong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtTong.Name = "txtTong";
            this.txtTong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 0, 100F);
            this.txtTong.StylePriority.UseFont = false;
            this.txtTong.StylePriority.UseForeColor = false;
            this.txtTong.StylePriority.UsePadding = false;
            this.txtTong.StylePriority.UseTextAlignment = false;
            this.txtTong.Text = "2000";
            this.txtTong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.txtTong.TextFormatString = "{0} VNĐ";
            this.txtTong.Weight = 0.6853644998076448D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 24.10715F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.StylePriority.UseBackColor = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.vendorContactsTable,
            this.xrLine1});
            this.BottomMargin.HeightF = 178.9762F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
            this.xrTable1.BorderColor = System.Drawing.Color.Gray;
            this.xrTable1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(29.99999F, 28.00005F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(629.9951F, 25F);
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UsePadding = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell4});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.CanShrink = true;
            this.xrTableCell3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Nhân viên";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 1.5770216106502231D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.CanShrink = true;
            this.xrTableCell4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Khách hàng";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 1.4229535393327089D;
            // 
            // vendorContactsTable
            // 
            this.vendorContactsTable.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
            this.vendorContactsTable.BorderColor = System.Drawing.Color.Gray;
            this.vendorContactsTable.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.vendorContactsTable.LocationFloat = new DevExpress.Utils.PointFloat(29.99998F, 132.8155F);
            this.vendorContactsTable.Name = "vendorContactsTable";
            this.vendorContactsTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.vendorContactsTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.vendorContactsRow});
            this.vendorContactsTable.SizeF = new System.Drawing.SizeF(629.9951F, 25F);
            this.vendorContactsTable.StylePriority.UseBorderColor = false;
            this.vendorContactsTable.StylePriority.UseFont = false;
            this.vendorContactsTable.StylePriority.UsePadding = false;
            // 
            // vendorContactsRow
            // 
            this.vendorContactsRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.txtTenNhanVien,
            this.txtTenKH2});
            this.vendorContactsRow.Name = "vendorContactsRow";
            this.vendorContactsRow.Weight = 1D;
            // 
            // txtTenNhanVien
            // 
            this.txtTenNhanVien.CanShrink = true;
            this.txtTenNhanVien.Name = "txtTenNhanVien";
            this.txtTenNhanVien.StylePriority.UseTextAlignment = false;
            this.txtTenNhanVien.Text = "Phan Vĩnh Long";
            this.txtTenNhanVien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.txtTenNhanVien.Weight = 1.5770216106502231D;
            // 
            // txtTenKH2
            // 
            this.txtTenKH2.CanShrink = true;
            this.txtTenKH2.Name = "txtTenKH2";
            this.txtTenKH2.StylePriority.UseBorders = false;
            this.txtTenKH2.StylePriority.UseTextAlignment = false;
            this.txtTenKH2.Text = "Nguyễn Thị Minh";
            this.txtTenKH2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.txtTenKH2.Weight = 1.4229535393327089D;
            // 
            // xrLine1
            // 
            this.xrLine1.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
            this.xrLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.xrLine1.LineWidth = 2F;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(669.6851F, 10F);
            this.xrLine1.StylePriority.UseForeColor = false;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtMaHoaDon,
            this.xrLabel2,
            this.invoiceLabel,
            this.xrLabel1,
            this.xrTable2,
            this.txtNgayLap,
            this.vendorLogo,
            this.customerTable});
            this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
            this.GroupHeader1.HeightF = 194.3392F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.StyleName = "baseControlStyle";
            this.GroupHeader1.StylePriority.UseBackColor = false;
            this.GroupHeader1.SubBands.AddRange(new DevExpress.XtraReports.UI.SubBand[] {
            this.SubBand1});
            // 
            // txtMaHoaDon
            // 
            this.txtMaHoaDon.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
            this.txtMaHoaDon.LocationFloat = new DevExpress.Utils.PointFloat(122.0817F, 47.67852F);
            this.txtMaHoaDon.Multiline = true;
            this.txtMaHoaDon.Name = "txtMaHoaDon";
            this.txtMaHoaDon.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtMaHoaDon.SizeF = new System.Drawing.SizeF(191.9643F, 22.99999F);
            this.txtMaHoaDon.StylePriority.UseTextAlignment = false;
            this.txtMaHoaDon.Text = "01234567";
            this.txtMaHoaDon.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.txtMaHoaDon.TextFormatString = "{0:d MMMM yyyy}";
            // 
            // xrLabel2
            // 
            this.xrLabel2.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(20.00005F, 47.67852F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(102.0817F, 22.99999F);
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Số hoá đơn:";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLabel2.TextFormatString = "{0:d MMMM yyyy}";
            // 
            // invoiceLabel
            // 
            this.invoiceLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.invoiceLabel.LocationFloat = new DevExpress.Utils.PointFloat(20.00005F, 0F);
            this.invoiceLabel.Name = "invoiceLabel";
            this.invoiceLabel.SizeF = new System.Drawing.SizeF(294.046F, 35.62501F);
            this.invoiceLabel.StylePriority.UseFont = false;
            this.invoiceLabel.StylePriority.UsePadding = false;
            this.invoiceLabel.StylePriority.UseTextAlignment = false;
            this.invoiceLabel.Text = "Hoá đơn mua hàng";
            this.invoiceLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(399.9951F, 111.3392F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(95.23813F, 22.99998F);
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Ngày lập:";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel1.TextFormatString = "{0:d MMMM yyyy}";
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(20.00005F, 109.3392F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow5});
            this.xrTable2.SizeF = new System.Drawing.SizeF(102.0817F, 75F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.CanShrink = true;
            this.xrTableCell5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UsePadding = false;
            this.xrTableCell5.Text = "Tên KH:";
            this.xrTableCell5.Weight = 1.1915477284685581D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.CanShrink = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "Mã KH:";
            this.xrTableCell6.Weight = 1.1915477284685581D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.CanShrink = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "Số điện thoại:";
            this.xrTableCell8.Weight = 1.1915477284685581D;
            // 
            // txtNgayLap
            // 
            this.txtNgayLap.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
            this.txtNgayLap.LocationFloat = new DevExpress.Utils.PointFloat(495.2332F, 111.3392F);
            this.txtNgayLap.Multiline = true;
            this.txtNgayLap.Name = "txtNgayLap";
            this.txtNgayLap.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtNgayLap.SizeF = new System.Drawing.SizeF(154.7619F, 22.99998F);
            this.txtNgayLap.StylePriority.UseTextAlignment = false;
            this.txtNgayLap.Text = "21/12/2019\r\n";
            this.txtNgayLap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.txtNgayLap.TextFormatString = "{0:d MMMM yyyy}";
            // 
            // vendorLogo
            // 
            this.vendorLogo.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
            this.vendorLogo.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.TopRight;
            this.vendorLogo.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource(global::QuanLyShopThoiTrang.Properties.Resources.logo, true);
            this.vendorLogo.LocationFloat = new DevExpress.Utils.PointFloat(399.9951F, 24.0774F);
            this.vendorLogo.Name = "vendorLogo";
            this.vendorLogo.SizeF = new System.Drawing.SizeF(250F, 75F);
            this.vendorLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            this.vendorLogo.StylePriority.UseBorderColor = false;
            this.vendorLogo.StylePriority.UseBorders = false;
            this.vendorLogo.StylePriority.UsePadding = false;
            // 
            // customerTable
            // 
            this.customerTable.LocationFloat = new DevExpress.Utils.PointFloat(122.0817F, 109.3392F);
            this.customerTable.Name = "customerTable";
            this.customerTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.customerNameRow,
            this.customerAddressRow,
            this.customerCountryRow});
            this.customerTable.SizeF = new System.Drawing.SizeF(250F, 75F);
            // 
            // customerNameRow
            // 
            this.customerNameRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.txtTenKH});
            this.customerNameRow.Name = "customerNameRow";
            this.customerNameRow.Weight = 1D;
            // 
            // txtTenKH
            // 
            this.txtTenKH.CanShrink = true;
            this.txtTenKH.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.StylePriority.UseFont = false;
            this.txtTenKH.StylePriority.UsePadding = false;
            this.txtTenKH.Text = "Nguyễn Thị Minh";
            this.txtTenKH.Weight = 1.1915477284685581D;
            // 
            // customerAddressRow
            // 
            this.customerAddressRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.txtMaKH});
            this.customerAddressRow.Name = "customerAddressRow";
            this.customerAddressRow.Weight = 1D;
            // 
            // txtMaKH
            // 
            this.txtMaKH.CanShrink = true;
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Text = "100002";
            this.txtMaKH.Weight = 1.1915477284685581D;
            // 
            // customerCountryRow
            // 
            this.customerCountryRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.txtSoDienThoai});
            this.customerCountryRow.Name = "customerCountryRow";
            this.customerCountryRow.Weight = 1D;
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.CanShrink = true;
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Text = "0123456789";
            this.txtSoDienThoai.Weight = 1.1915477284685581D;
            // 
            // SubBand1
            // 
            this.SubBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.headerTable});
            this.SubBand1.HeightF = 47.68457F;
            this.SubBand1.KeepTogether = true;
            this.SubBand1.Name = "SubBand1";
            // 
            // headerTable
            // 
            this.headerTable.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
            this.headerTable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(121)))), ((int)(((byte)(121)))));
            this.headerTable.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.headerTable.BorderWidth = 3F;
            this.headerTable.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.headerTable.LocationFloat = new DevExpress.Utils.PointFloat(45.04176F, 12.68455F);
            this.headerTable.Name = "headerTable";
            this.headerTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 5, 0, 100F);
            this.headerTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.headerTableRow});
            this.headerTable.SizeF = new System.Drawing.SizeF(604.9532F, 35.00002F);
            this.headerTable.StylePriority.UseBorderColor = false;
            this.headerTable.StylePriority.UseBorders = false;
            this.headerTable.StylePriority.UseBorderWidth = false;
            this.headerTable.StylePriority.UseFont = false;
            this.headerTable.StylePriority.UsePadding = false;
            // 
            // headerTableRow
            // 
            this.headerTableRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.quantityCaption,
            this.productNameCaption,
            this.unitPriceCaption,
            this.xrTableCell1,
            this.lineTotalCaption});
            this.headerTableRow.Name = "headerTableRow";
            this.headerTableRow.Weight = 11.5D;
            // 
            // quantityCaption
            // 
            this.quantityCaption.Name = "quantityCaption";
            this.quantityCaption.StylePriority.UseTextAlignment = false;
            this.quantityCaption.Text = "Mã sản phẩm";
            this.quantityCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.quantityCaption.Weight = 0.52565761784710729D;
            // 
            // productNameCaption
            // 
            this.productNameCaption.Name = "productNameCaption";
            this.productNameCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 5, 0, 100F);
            this.productNameCaption.StylePriority.UsePadding = false;
            this.productNameCaption.Text = "Tên sản phẩm";
            this.productNameCaption.Weight = 1.0245557916409809D;
            // 
            // unitPriceCaption
            // 
            this.unitPriceCaption.Name = "unitPriceCaption";
            this.unitPriceCaption.StylePriority.UseTextAlignment = false;
            this.unitPriceCaption.Text = "Đơn giá";
            this.unitPriceCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.unitPriceCaption.Weight = 0.44979962280488728D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Số lượng";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell1.Weight = 0.47445470834154524D;
            // 
            // lineTotalCaption
            // 
            this.lineTotalCaption.Name = "lineTotalCaption";
            this.lineTotalCaption.StylePriority.UseTextAlignment = false;
            this.lineTotalCaption.Text = "Thành tiền";
            this.lineTotalCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.lineTotalCaption.Weight = 0.588929023095438D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.totalTable});
            this.GroupFooter1.GroupUnion = DevExpress.XtraReports.UI.GroupFooterUnion.WithLastDetail;
            this.GroupFooter1.HeightF = 63.64277F;
            this.GroupFooter1.KeepTogether = true;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBandExceptLastEntry;
            this.GroupFooter1.StyleName = "baseControlStyle";
            // 
            // totalTable
            // 
            this.totalTable.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
            this.totalTable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.totalTable.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.totalTable.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.totalTable.ForeColor = System.Drawing.Color.Black;
            this.totalTable.LocationFloat = new DevExpress.Utils.PointFloat(200.0004F, 10F);
            this.totalTable.Name = "totalTable";
            this.totalTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 0, 100F);
            this.totalTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.totalRow});
            this.totalTable.SizeF = new System.Drawing.SizeF(449.9999F, 35F);
            this.totalTable.StylePriority.UseBorderColor = false;
            this.totalTable.StylePriority.UseBorders = false;
            this.totalTable.StylePriority.UseFont = false;
            this.totalTable.StylePriority.UseForeColor = false;
            this.totalTable.StylePriority.UsePadding = false;
            // 
            // totalRow
            // 
            this.totalRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(121)))), ((int)(((byte)(121)))));
            this.totalRow.BorderWidth = 3F;
            this.totalRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.totalCaption,
            this.txtTongHoaDon});
            this.totalRow.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.totalRow.Name = "totalRow";
            this.totalRow.StylePriority.UseBorderColor = false;
            this.totalRow.StylePriority.UseBorderWidth = false;
            this.totalRow.StylePriority.UseFont = false;
            this.totalRow.Weight = 1.5217391304347823D;
            // 
            // totalCaption
            // 
            this.totalCaption.Name = "totalCaption";
            this.totalCaption.StylePriority.UseFont = false;
            this.totalCaption.StylePriority.UseTextAlignment = false;
            this.totalCaption.Text = "Tổng";
            this.totalCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            this.totalCaption.Weight = 5.0703609369432883D;
            // 
            // txtTongHoaDon
            // 
            this.txtTongHoaDon.Name = "txtTongHoaDon";
            this.txtTongHoaDon.StylePriority.UseFont = false;
            this.txtTongHoaDon.StylePriority.UseTextAlignment = false;
            this.txtTongHoaDon.Text = "2000";
            this.txtTongHoaDon.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
            this.txtTongHoaDon.TextFormatString = "{0} VNĐ";
            this.txtTongHoaDon.Weight = 3.7904626522673168D;
            // 
            // baseControlStyle
            // 
            this.baseControlStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.baseControlStyle.Name = "baseControlStyle";
            this.baseControlStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // dataSource
            // 
            this.dataSource.Name = "dataSource";
            this.dataSource.ObjectTypeName = "QuanLyShopThoiTrang.Model.ChiTietHoaDon";
            this.dataSource.TopReturnedRecords = 0;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(QuanLyShopThoiTrang.Model.ChiTietHoaDon);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // HoaDonReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupFooter1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.dataSource,
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(90, 90, 24, 179);
            this.ScriptsSource = "\r\nprivate void tblMain_EvaluateBinding(object sender, DevExpress.XtraReports.UI.B" +
    "indingEventArgs e) {\r\n\r\n}\r\n";
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.baseControlStyle});
            this.StyleSheetPath = "";
            this.Version = "19.1";
            ((System.ComponentModel.ISupportInitialize)(this.tblMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vendorContactsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
