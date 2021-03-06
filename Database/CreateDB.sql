USE [master]
GO

WHILE EXISTS(select NULL from sys.databases where name='QLShop')
BEGIN
    DECLARE @SQL varchar(max)
    SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';'
    FROM MASTER..SysProcesses
    WHERE DBId = DB_ID(N'QLShop') AND SPId <> @@SPId
    EXEC(@SQL)
    DROP DATABASE [QLShop]
END
GO

CREATE DATABASE [QLShop]
GO


USE [QLShop]
GO
/****** Object:  Database [QLShop]    Script Date: 6/7/2019 8:47:25 PM ******/

ALTER DATABASE [QLShop] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLShop] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QLShop] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [QLShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLShop] SET  MULTI_USER 
GO
ALTER DATABASE [QLShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLShop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [QLShop]
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[IDHoaDon] [int] NOT NULL,
	[IDSanPham] [int] NOT NULL,
	[DonGia] [float] NOT NULL,
	[SoLuong] [int] NOT NULL,
 CONSTRAINT [PK_ChiTietHoaDon] PRIMARY KEY CLUSTERED 
(
	[IDHoaDon] ASC,
	[IDSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietPhieuNhap]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuNhap](
	[IDPhieuNhap] [int] NOT NULL,
	[IDSanPham] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [float] NOT NULL,
 CONSTRAINT [PK_ChiTietPhieuNhap] PRIMARY KEY CLUSTERED 
(
	[IDPhieuNhap] ASC,
	[IDSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietPhieuTra]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuTra](
	[IDPhieuTra] [int] NOT NULL,
	[IDSanPham] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
 CONSTRAINT [PK_ChiTietPhieuTra] PRIMARY KEY CLUSTERED 
(
	[IDPhieuTra] ASC,
	[IDSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[IDHoaDon] [int] NOT NULL,
	[IDNhanVien] [int] NOT NULL,
	[IDKhachHang] [int] NOT NULL,
	[NgayHoaDon] [datetime] NOT NULL,
	[GiamGia] [float] NOT NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[IDHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[IDKhachHang] [int] NOT NULL,
	[HoTen] [nvarchar](80) NOT NULL,
	[NamSinh] [int] NOT NULL,
	[GioiTinh] [nvarchar](4) NOT NULL,
	[SoDienThoai] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](80) NULL,
	[TongTienTichLuy] [float] NOT NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[IDKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KichCo]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KichCo](
	[IDKichCo] [int] NOT NULL,
	[TenKichCo] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_KichCo] PRIMARY KEY CLUSTERED 
(
	[IDKichCo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiKhachHang]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiKhachHang](
	[IDLoaiKhachHang] [int] NOT NULL,
	[MoTa] [nvarchar](10) NOT NULL,
	[TichLuyToiThieu] [float] NOT NULL,
	[MucGiamGia] [float] NOT NULL,
 CONSTRAINT [PK_LoaiKhachHang] PRIMARY KEY CLUSTERED 
(
	[IDLoaiKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiSanPham]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSanPham](
	[IDLoaiSanPham] [int] NOT NULL,
	[IDNhaCungCap] [int] NOT NULL,
	[TenLoaiSanPham] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_LoaiSanPham] PRIMARY KEY CLUSTERED 
(
	[IDLoaiSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MauSac]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MauSac](
	[IDMauSac] [int] NOT NULL,
	[TenMauSac] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_MauSac] PRIMARY KEY CLUSTERED 
(
	[IDMauSac] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[IDNhaCungCap] [int] NOT NULL,
	[TenNhaCungCap] [nvarchar](50) NOT NULL,
	[SoDienThoai] [nvarchar](10) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[Email] [nvarchar](30) NULL,
 CONSTRAINT [PK_NhaCungCap] PRIMARY KEY CLUSTERED 
(
	[IDNhaCungCap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[IDNhanVien] [int] NOT NULL,
	[IDVaiTro] [int] NOT NULL,
	[MatKhau] [nvarchar](max) NOT NULL,
	[HoTen] [nvarchar](50) NOT NULL,
	[ChungMinhNhanDan] [nvarchar](12) NOT NULL,
	[SoDienThoai] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](20) NOT NULL,
	[DiaChi] [nvarchar](50) NOT NULL,
	[GioiTinh] [nvarchar](4) NOT NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[IDNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuNhap]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuNhap](
	[IDPhieuNhap] [int] NOT NULL,
	[IDNhaCungCap] [int] NOT NULL,
	[IDNhanVien] [int] NOT NULL,
	[NgayNhap] [datetime] NOT NULL,
	[DaNhapVaoKho] [bit] NOT NULL,
 CONSTRAINT [PK__PhieuNha__4A581F6D0BCD4D62] PRIMARY KEY CLUSTERED 
(
	[IDPhieuNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
 CONSTRAINT [PK__PhieuNha__NhanVien] PRIMARY KEY CLUSTERED 
(
	[IDNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuTra]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuTra](
	[IDPhieuTra] [int] NOT NULL,
	[IDHoaDon] [int] NOT NULL,
	[TongTien] [float] NOT NULL,
	[IDNhanVien] [int] NOT NULL,
 CONSTRAINT [PK_PhieuTra] PRIMARY KEY CLUSTERED 
(
	[IDPhieuTra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[IDSanPham] [int] NOT NULL,
	[IDMauSac] [int] NOT NULL,
	[IDKichCo] [int] NOT NULL,
	[IDLoaiSanPham] [int] NOT NULL,
	[DonGia] [float] NOT NULL,
	[SoLuongTon] [int] NOT NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[IDSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VaiTro]    Script Date: 6/7/2019 8:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaiTro](
	[IDVaiTro] [int] NOT NULL,
	[TenVaiTro] [nvarchar](30) NOT NULL,
	[MoTa] [nvarchar](50) NULL,
	[QLKhachHang] [bit] NOT NULL,
	[QLNhaCungCap] [bit] NOT NULL,
	[QLSanPham] [bit] NOT NULL,
	[QLHoaDon] [bit] NOT NULL,
	[QLNhanVien] [bit] NOT NULL,
	[QLLoaiKhachHang] [bit] NOT NULL,
	[LapHoaDon] [bit] NOT NULL,
	[LapPhieuTraHang] [bit] NOT NULL,
	[LapPhieuNhapHang] [bit] NOT NULL,
	[QLLoaiSanPham] [bit] NOT NULL,
	[BaoCao] [bit] NOT NULL,
	[QLSizeMau] [bit] NOT NULL,
	[QLVaiTro] [bit] NOT NULL,
 CONSTRAINT [PK_VaiTro] PRIMARY KEY CLUSTERED 
(
	[IDVaiTro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[KhachHang] ([IDKhachHang], [HoTen], [NamSinh], [GioiTinh], [SoDienThoai], [Email], [TongTienTichLuy]) VALUES (1000, N'Khách Vãng Lai', 1998, N'Nam', N'0000000000', N'', 0)
INSERT [dbo].[KhachHang] ([IDKhachHang], [HoTen], [NamSinh], [GioiTinh], [SoDienThoai], [Email], [TongTienTichLuy]) VALUES (1001, N'Nguyễn Bá Tùng', 1998, N'Nam', N'0797727895', N'batung1998@gmail.com', 1000)
INSERT [dbo].[KhachHang] ([IDKhachHang], [HoTen], [NamSinh], [GioiTinh], [SoDienThoai], [Email], [TongTienTichLuy]) VALUES (1002, N'Phạm Quang Vinh', 1998, N'Nam', N'0232786472', N'Vinh@15trieu.com', 0)
INSERT [dbo].[KhachHang] ([IDKhachHang], [HoTen], [NamSinh], [GioiTinh], [SoDienThoai], [Email], [TongTienTichLuy]) VALUES (1003, N'Phan Vĩnh Long', 1998, N'Nữ', N'0763196317', N'Long@gmail.com', 0)
INSERT [dbo].[KhachHang] ([IDKhachHang], [HoTen], [NamSinh], [GioiTinh], [SoDienThoai], [Email], [TongTienTichLuy]) VALUES (1004, N'Nguyễn Ngọc Dung', 1998, N'Nữ', N'536423149', N'Dung@ngoc.com', 0)
INSERT [dbo].[KhachHang] ([IDKhachHang], [HoTen], [NamSinh], [GioiTinh], [SoDienThoai], [Email], [TongTienTichLuy]) VALUES (1005, N'Phan Minh Toàn', 1998, N'Nam', N'536423149', N'Toan@outlook.com', 0)
INSERT [dbo].[KhachHang] ([IDKhachHang], [HoTen], [NamSinh], [GioiTinh], [SoDienThoai], [Email], [TongTienTichLuy]) VALUES (1006, N'Trần Van Bê', 1998, N'Nữ', N'536423149', N'Bebe@bo.com', 0)
INSERT [dbo].[KhachHang] ([IDKhachHang], [HoTen], [NamSinh], [GioiTinh], [SoDienThoai], [Email], [TongTienTichLuy]) VALUES (1007, N'Nguyễn Lê Anh Tú', 1998, N'Nam', N'536423149', N'Tu@nguyenle.com', 0)


INSERT [dbo].[KichCo] ([IDKichCo], [TenKichCo]) VALUES (10, N'S')
INSERT [dbo].[KichCo] ([IDKichCo], [TenKichCo]) VALUES (11, N'M')
INSERT [dbo].[KichCo] ([IDKichCo], [TenKichCo]) VALUES (12, N'L')
INSERT [dbo].[KichCo] ([IDKichCo], [TenKichCo]) VALUES (13, N'XL')
INSERT [dbo].[KichCo] ([IDKichCo], [TenKichCo]) VALUES (14, N'XXL')
INSERT [dbo].[KichCo] ([IDKichCo], [TenKichCo]) VALUES (15, N'Free Size')


INSERT [dbo].[LoaiKhachHang] ([IDLoaiKhachHang], [MoTa], [TichLuyToiThieu], [MucGiamGia]) VALUES (1, N'4New', 500, 0.1)
INSERT [dbo].[LoaiKhachHang] ([IDLoaiKhachHang], [MoTa], [TichLuyToiThieu], [MucGiamGia]) VALUES (2, N'4Silver', 1500, 0.15)
INSERT [dbo].[LoaiKhachHang] ([IDLoaiKhachHang], [MoTa], [TichLuyToiThieu], [MucGiamGia]) VALUES (3, N'4Gold', 3000, 0.2)
INSERT [dbo].[LoaiKhachHang] ([IDLoaiKhachHang], [MoTa], [TichLuyToiThieu], [MucGiamGia]) VALUES (4, N'4Diamond', 5000, 0.3)


INSERT [dbo].[LoaiSanPham] ([IDLoaiSanPham], [IDNhaCungCap], [TenLoaiSanPham]) VALUES (1000, 100, N'Áo thun Mabu S300')
INSERT [dbo].[LoaiSanPham] ([IDLoaiSanPham], [IDNhaCungCap], [TenLoaiSanPham]) VALUES (1001, 102, N'Áo khóa Adachi R3')
INSERT [dbo].[LoaiSanPham] ([IDLoaiSanPham], [IDNhaCungCap], [TenLoaiSanPham]) VALUES (1002, 101, N'Quần Jean Levis S72')
INSERT [dbo].[LoaiSanPham] ([IDLoaiSanPham], [IDNhaCungCap], [TenLoaiSanPham]) VALUES (1003, 100, N'Thắt lưng Apple 6Plus')
INSERT [dbo].[LoaiSanPham] ([IDLoaiSanPham], [IDNhaCungCap], [TenLoaiSanPham]) VALUES (1004, 102, N'Hoodie SamSung T34')


INSERT [dbo].[MauSac] ([IDMauSac], [TenMauSac]) VALUES (100, N'Xanh rêu')
INSERT [dbo].[MauSac] ([IDMauSac], [TenMauSac]) VALUES (101, N'Xanh dương')
INSERT [dbo].[MauSac] ([IDMauSac], [TenMauSac]) VALUES (102, N'Nâu')
INSERT [dbo].[MauSac] ([IDMauSac], [TenMauSac]) VALUES (103, N'Tím khói')
INSERT [dbo].[MauSac] ([IDMauSac], [TenMauSac]) VALUES (104, N'Đỏ Hồng')
INSERT [dbo].[MauSac] ([IDMauSac], [TenMauSac]) VALUES (105, N'Hồng Cánh Sen')


INSERT [dbo].[NhaCungCap] ([IDNhaCungCap], [TenNhaCungCap], [SoDienThoai], [DiaChi], [Email]) VALUES (100, N'Công Ty ABC', N'0123456753', N'Long An', N'abc@gmai.com')
INSERT [dbo].[NhaCungCap] ([IDNhaCungCap], [TenNhaCungCap], [SoDienThoai], [DiaChi], [Email]) VALUES (101, N'DNTN Tây Ninh', N'0123456', N'Tây Ninh', N'tuy@gmail.com')
INSERT [dbo].[NhaCungCap] ([IDNhaCungCap], [TenNhaCungCap], [SoDienThoai], [DiaChi], [Email]) VALUES (102, N'Becamex Bình Dương', N'01288345', N'Sài Gòn', N'emailne@gmail.com')



INSERT [dbo].[NhanVien] ([IDNhanVien], [IDVaiTro], [MatKhau], [HoTen], [ChungMinhNhanDan], [SoDienThoai], [Email], [DiaChi], [GioiTinh]) VALUES (101, 2, N'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', N'Nguyễn Công Phượng', N'301658866', N'0123456799', N'batung@abc.com', N'Long An', N'Nữ')
INSERT [dbo].[NhanVien] ([IDNhanVien], [IDVaiTro], [MatKhau], [HoTen], [ChungMinhNhanDan], [SoDienThoai], [Email], [DiaChi], [GioiTinh]) VALUES (102, 1, N'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', N'Nguyễn Văn Toàn', N'301658866', N'0123456799', N'batung@admin.com', N'Long An', N'Nữ')
INSERT [dbo].[NhanVien] ([IDNhanVien], [IDVaiTro], [MatKhau], [HoTen], [ChungMinhNhanDan], [SoDienThoai], [Email], [DiaChi], [GioiTinh]) VALUES (103, 1, N'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', N'Phạm Đức Huy', N'301230192', N'0123919239', N'1', N'Khanh Hoa', N'Nam')


INSERT [dbo].[PhieuNhap] ([IDPhieuNhap], [IDNhaCungCap], [IDNhanVien], [NgayNhap], [DaNhapVaoKho]) VALUES (10001, 100, 10000003, CAST(N'2019-05-29 00:00:00.000' AS DateTime), 0)
INSERT [dbo].[PhieuNhap] ([IDPhieuNhap], [IDNhaCungCap], [IDNhanVien], [NgayNhap], [DaNhapVaoKho]) VALUES (10002, 101, 10000003, CAST(N'2019-05-29 00:00:00.000' AS DateTime), 0)
INSERT [dbo].[PhieuNhap] ([IDPhieuNhap], [IDNhaCungCap], [IDNhanVien], [NgayNhap], [DaNhapVaoKho]) VALUES (10003, 102, 10000003, CAST(N'2019-06-07 10:00:58.687' AS DateTime), 0)
INSERT [dbo].[PhieuNhap] ([IDPhieuNhap], [IDNhaCungCap], [IDNhanVien], [NgayNhap], [DaNhapVaoKho]) VALUES (10004, 100, 10000003, CAST(N'2019-06-07 10:08:32.597' AS DateTime), 0)
INSERT [dbo].[PhieuNhap] ([IDPhieuNhap], [IDNhaCungCap], [IDNhanVien], [NgayNhap], [DaNhapVaoKho]) VALUES (10005, 102, 10000003, CAST(N'2019-06-07 11:02:50.570' AS DateTime), 0)


INSERT [dbo].[PhieuTra] ([IDPhieuTra], [IDHoaDon], [TongTien], [IDNhanVien]) VALUES (10001, 10000000, 198, 10000003)
INSERT [dbo].[SanPham] ([IDSanPham], [IDMauSac], [IDKichCo], [IDLoaiSanPham], [DonGia], [SoLuongTon]) VALUES (100001, 100, 10, 1000, 159, 10)
INSERT [dbo].[SanPham] ([IDSanPham], [IDMauSac], [IDKichCo], [IDLoaiSanPham], [DonGia], [SoLuongTon]) VALUES (100002, 102, 14, 1000, 99, 8)
INSERT [dbo].[SanPham] ([IDSanPham], [IDMauSac], [IDKichCo], [IDLoaiSanPham], [DonGia], [SoLuongTon]) VALUES (100003, 100, 12, 1001, 299, 12)
INSERT [dbo].[SanPham] ([IDSanPham], [IDMauSac], [IDKichCo], [IDLoaiSanPham], [DonGia], [SoLuongTon]) VALUES (100004, 101, 12, 1001, 149, 0)
INSERT [dbo].[SanPham] ([IDSanPham], [IDMauSac], [IDKichCo], [IDLoaiSanPham], [DonGia], [SoLuongTon]) VALUES (100005, 101, 15, 1002, 459, 5)
INSERT [dbo].[SanPham] ([IDSanPham], [IDMauSac], [IDKichCo], [IDLoaiSanPham], [DonGia], [SoLuongTon]) VALUES (100006, 102, 15, 1003, 129, 10)
INSERT [dbo].[SanPham] ([IDSanPham], [IDMauSac], [IDKichCo], [IDLoaiSanPham], [DonGia], [SoLuongTon]) VALUES (100007, 100, 12, 1004, 229, 5)
INSERT [dbo].[SanPham] ([IDSanPham], [IDMauSac], [IDKichCo], [IDLoaiSanPham], [DonGia], [SoLuongTon]) VALUES (100008, 101, 13, 1004, 79, 11)
INSERT [dbo].[SanPham] ([IDSanPham], [IDMauSac], [IDKichCo], [IDLoaiSanPham], [DonGia], [SoLuongTon]) VALUES (100009, 105, 11, 1004, 229, 2)


INSERT [dbo].[VaiTro] ([IDVaiTro], [TenVaiTro], [MoTa], [QLKhachHang], [QLNhaCungCap], [QLSanPham], [QLHoaDon], [QLNhanVien], [QLLoaiKhachHang], [LapHoaDon], [LapPhieuTraHang], [LapPhieuNhapHang], [QLLoaiSanPham], [BaoCao], [QLSizeMau], [QLVaiTro]) VALUES (1, N'Quản Lý Cửa Hàng', N'Tao quản lý nhân viên', 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT [dbo].[VaiTro] ([IDVaiTro], [TenVaiTro], [MoTa], [QLKhachHang], [QLNhaCungCap], [QLSanPham], [QLHoaDon], [QLNhanVien], [QLLoaiKhachHang], [LapHoaDon], [LapPhieuTraHang], [LapPhieuNhapHang], [QLLoaiSanPham], [BaoCao], [QLSizeMau], [QLVaiTro]) VALUES (2, N'Nhân Viên Bán Hàng', N'Tao được thuê vô đây làm', 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0)


ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([IDHoaDon])
REFERENCES [dbo].[HoaDon] ([IDHoaDon])
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([IDSanPham])
REFERENCES [dbo].[SanPham] ([IDSanPham])
GO
ALTER TABLE [dbo].[ChiTietPhieuNhap]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuNhap_PhieuNhap] FOREIGN KEY([IDPhieuNhap])
REFERENCES [dbo].[PhieuNhap] ([IDPhieuNhap])
GO
ALTER TABLE [dbo].[ChiTietPhieuNhap] CHECK CONSTRAINT [FK_ChiTietPhieuNhap_PhieuNhap]
GO
ALTER TABLE [dbo].[ChiTietPhieuNhap]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuNhap_SanPham] FOREIGN KEY([IDSanPham])
REFERENCES [dbo].[SanPham] ([IDSanPham])
GO
ALTER TABLE [dbo].[ChiTietPhieuNhap] CHECK CONSTRAINT [FK_ChiTietPhieuNhap_SanPham]
GO
ALTER TABLE [dbo].[ChiTietPhieuTra]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuTra_PhieuTra] FOREIGN KEY([IDPhieuTra])
REFERENCES [dbo].[PhieuTra] ([IDPhieuTra])
GO
ALTER TABLE [dbo].[ChiTietPhieuTra] CHECK CONSTRAINT [FK_ChiTietPhieuTra_PhieuTra]
GO
ALTER TABLE [dbo].[ChiTietPhieuTra]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuTra_SanPham] FOREIGN KEY([IDSanPham])
REFERENCES [dbo].[SanPham] ([IDSanPham])
GO
ALTER TABLE [dbo].[ChiTietPhieuTra] CHECK CONSTRAINT [FK_ChiTietPhieuTra_SanPham]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([IDKhachHang])
REFERENCES [dbo].[KhachHang] ([IDKhachHang])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([IDNhanVien])
REFERENCES [dbo].[NhanVien] ([IDNhanVien])
GO
ALTER TABLE [dbo].[LoaiSanPham]  WITH CHECK ADD FOREIGN KEY([IDNhaCungCap])
REFERENCES [dbo].[NhaCungCap] ([IDNhaCungCap])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([IDVaiTro])
REFERENCES [dbo].[VaiTro] ([IDVaiTro])
GO
ALTER TABLE [dbo].[PhieuNhap]  WITH CHECK ADD  CONSTRAINT [FK__PhieuNhap__IDNha__36B12243] FOREIGN KEY([IDNhaCungCap])
REFERENCES [dbo].[NhaCungCap] ([IDNhaCungCap])
GO
ALTER TABLE [dbo].[PhieuNhap] CHECK CONSTRAINT [FK__PhieuNhap__IDNha__36B12243]
GO
ALTER TABLE [dbo].[PhieuNhap]  WITH CHECK ADD  CONSTRAINT [FK__PhieuNhap__IDNha__37A5467C] FOREIGN KEY([IDNhanVien])
REFERENCES [dbo].[NhanVien] ([IDNhanVien])
GO
ALTER TABLE [dbo].[PhieuNhap] CHECK CONSTRAINT [FK__PhieuNhap__IDNha__37A5467C]
GO
ALTER TABLE [dbo].[PhieuTra]  WITH CHECK ADD  CONSTRAINT [FK_PhieuTra_HoaDon] FOREIGN KEY([IDNhanVien])
REFERENCES [dbo].[NhanVien] ([IDNhanVien])
GO
ALTER TABLE [dbo].[PhieuTra] CHECK CONSTRAINT [FK_PhieuTra_HoaDon]
GO
ALTER TABLE [dbo].[PhieuTra]  WITH CHECK ADD  CONSTRAINT [FK_PhieuTra_PhieuTra] FOREIGN KEY([IDPhieuTra])
REFERENCES [dbo].[PhieuTra] ([IDPhieuTra])
GO
ALTER TABLE [dbo].[PhieuTra] CHECK CONSTRAINT [FK_PhieuTra_PhieuTra]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([IDKichCo])
REFERENCES [dbo].[KichCo] ([IDKichCo])
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([IDLoaiSanPham])
REFERENCES [dbo].[LoaiSanPham] ([IDLoaiSanPham])
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([IDMauSac])
REFERENCES [dbo].[MauSac] ([IDMauSac])
GO
USE [master]
GO
ALTER DATABASE [QLShop] SET  READ_WRITE 
GO


