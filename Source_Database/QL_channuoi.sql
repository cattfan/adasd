CREATE DATABASE QuanLychannuoi;
GO

USE QuanLychannuoi;
GO

CREATE TABLE ToNhanVien (
    MaTo VARCHAR(10) PRIMARY KEY,
    TenTo NVARCHAR(50) NOT NULL UNIQUE,
    TenDN NVARCHAR(30) NOT NULL,
    MatKhau VARCHAR(30)
);
GO

CREATE TABLE ChucVuNhanVien (
    MaChucVu VARCHAR(10) PRIMARY KEY,
    TenChucVu NVARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE NhanVien (
    MaNhanVien VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100),
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    MaTo VARCHAR(10),
    MaChucVu VARCHAR(10),
    FOREIGN KEY (MaTo) REFERENCES ToNhanVien(MaTo),
    FOREIGN KEY (MaChucVu) REFERENCES ChucVuNhanVien(MaChucVu)
);
GO

CREATE TABLE ChuongVatNuoi (
    MaChuong VARCHAR(10) PRIMARY KEY,
    ViTri NVARCHAR(100),
    DienTich DECIMAL(10,2)
);
GO

CREATE TABLE VatNuoi (
    MaVatNuoi VARCHAR(10) PRIMARY KEY,
    TenVatNuoi NVARCHAR(100),
    NgayNhap DATE,
    SoLuong INT,
    MaChuong VARCHAR(10) NULL,
    FOREIGN KEY (MaChuong) REFERENCES ChuongVatNuoi(MaChuong)
);
GO

CREATE TABLE NhanVien_VatNuoi (
    MaNhanVien VARCHAR(10),
    MaVatNuoi VARCHAR(10),
    PRIMARY KEY (MaNhanVien, MaVatNuoi),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaVatNuoi) REFERENCES VatNuoi(MaVatNuoi)
);
GO

CREATE TABLE VatTu (
    MaVatTu VARCHAR(10) PRIMARY KEY,
    TenVatTu NVARCHAR(100),
    DonViTinh NVARCHAR(30),
    SoLuong INT,
    TrangThai NVARCHAR(30)
);
GO

CREATE TABLE NhaCungCap (
    MaNhaCungCap VARCHAR(10) PRIMARY KEY,
    TenNhaCungCap NVARCHAR(100),
    DiaChi NVARCHAR(255)
);
GO


CREATE TABLE NhaCungCap_VatTu (
    MaNhaCungCap VARCHAR(10),
    MaVatTu VARCHAR(10),
    DonGia DECIMAL(18,2),
    NgayCungCap DATE,
    PRIMARY KEY (MaNhaCungCap, MaVatTu),
    FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap),
    FOREIGN KEY (MaVatTu) REFERENCES VatTu(MaVatTu)
);
GO


CREATE TABLE HoaDon (
    MaHoaDon VARCHAR(10) PRIMARY KEY,
    NgayLap DATE
);
GO

CREATE TABLE ChiTietHoaDon (
    MaHoaDon VARCHAR(10),
    STT INT,
    LoaiMatHang NVARCHAR(50),
    MaMatHang VARCHAR(10),
    SoLuong INT,
    DonGia DECIMAL(18,2),
    MaNhanVien VARCHAR(10),
    MaNhaCungCap VARCHAR(10),
    PRIMARY KEY (MaHoaDon, STT),
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap)
);
GO

CREATE TABLE Log_LichSuChuong (
    MaLog VARCHAR(10) PRIMARY KEY,
    MaNhanVien VARCHAR(10),
    MaChuong VARCHAR(10),
    MaVatNuoi VARCHAR(10),
    LichSuSoLuongVatNuoiTrongChuong INT,
    log_thoigian DATETIME DEFAULT(GETDATE())
);
GO