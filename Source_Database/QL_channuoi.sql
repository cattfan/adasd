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


-- Bảng Chức Vụ
INSERT INTO [dbo].[ChucVuNhanVien] ([MaChucVu], [TenChucVu]) VALUES
('CV01', N'Quản Lý'),
('CV02', N'Nhân Viên'),
('CV03', N'Bác sĩ thú y');
GO

-- Bảng Tổ Nhân Viên (Thêm dữ liệu cho các cột bắt buộc TenDN và MatKhau)
INSERT INTO [dbo].[ToNhanVien] ([MaTo], [TenTo], [TenDN], [MatKhau]) VALUES
('T01', N'Tổ Chăm Sóc Bò Sữa', 'tcsbs', '123456'),
('T02', N'Tổ Chăm Sóc Gia Cầm', 'tcscg', '123456'),
('T03', N'Tổ Kỹ Thuật & Thú Y', 'tkt', '123456'),
('T04', N'Tổ Chăm Sóc Heo', 'tcsheo', '123456');
GO

-- Bảng Nhân Viên
INSERT INTO [dbo].[NhanVien] ([MaNhanVien], [HoTen], [NgaySinh], [GioiTinh], [MaTo], [MaChucVu]) VALUES
('NV01', N'Nguyễn Văn An', '1985-05-20', N'Nam', 'T01', 'CV01'),
('NV02', N'Trần Thị Bích', '1992-11-15', N'Nữ', 'T01', 'CV02'),
('NV03', N'Lê Văn Cường', '1990-09-01', N'Nam', 'T02', 'CV02'),
('NV04', N'Phạm Thị Dung', '1995-02-25', N'Nữ', 'T03', 'CV03'),
('NV05', N'Hoàng Văn E', '1998-07-11', N'Nam', 'T04', 'CV02');
GO

-- Bảng Chuồng Nuôi
INSERT INTO [dbo].[ChuongVatNuoi] ([MaChuong], [ViTri], [DienTich]) VALUES
('C01', N'Khu A1 - Bò Sữa', 1500.50),
('C02', N'Khu B1 - Gà Đẻ', 800.00),
('C03', N'Khu B2 - Gà Thịt', 1200.75),
('C04', N'Khu C1 - Heo Nái', 950.00),
('C05', N'Khu C2 - Heo Thịt', 1100.00);
GO

-- Bảng Nhà Cung Cấp
INSERT INTO [dbo].[NhaCungCap] ([MaNhaCungCap], [TenNhaCungCap], [DiaChi]) VALUES
('NCC01', N'Công ty GreenFeed Việt Nam', N'KCN Nhơn Trạch, Đồng Nai'),
('NCC02', N'Vemedim Corp', N'KCN Sóng Thần, Bình Dương'),
('NCC03', N'Tập đoàn De Heus', N'Long An, Việt Nam');
GO

-- Bảng Vật Tư
INSERT INTO [dbo].[VatTu] ([MaVatTu], [TenVatTu], [DonViTinh], [SoLuong], [TrangThai]) VALUES
('VT01', N'Cám Bò Sữa Cao Sản', N'Bao 25kg', 200, N'Còn hàng'),
('VT02', N'Vắc-xin Tụ Huyết Trùng', N'Lọ', 500, N'Còn hàng'),
('VT03', N'Cám Gà Đẻ Trứng', N'Bao 25kg', 350, N'Còn hàng'),
('VT04', N'Thuốc Bổ sung Canxi', N'Chai 1L', 120, N'Còn hàng'),
('VT05', N'Cám Heo Con', N'Bao 25kg', 400, N'Còn hàng'),
('VT06', N'Thuốc Tẩy Giun', N'Gói 100g', 300, N'Còn hàng');
GO

-- Bảng liên kết Nhà Cung Cấp và Vật Tư (Thêm DonGia, NgayCungCap)
INSERT INTO [dbo].[NhaCungCap_VatTu] ([MaNhaCungCap], [MaVatTu], [DonGia], [NgayCungCap]) VALUES
('NCC01', 'VT01', 350000.00, '2024-05-01'),
('NCC03', 'VT01', 355000.00, '2024-05-02'),
('NCC02', 'VT02', 15000.00, '2024-04-20'),
('NCC03', 'VT03', 320000.00, '2024-06-10'),
('NCC02', 'VT04', 95000.00, '2024-06-15'),
('NCC01', 'VT05', 410000.00, '2024-06-18'),
('NCC02', 'VT06', 25000.00, '2024-06-01');
GO

-- Bảng Vật Nuôi
INSERT INTO [dbo].[VatNuoi] ([MaVatNuoi], [TenVatNuoi], [NgayNhap], [MaChuong], [SoLuong]) VALUES
('VN01', N'Đàn Bò Sữa Holstein', '2024-01-15', 'C01', 50),
('VN02', N'Đàn Gà Đẻ Lông Trắng', '2024-02-20', 'C02', 2000),
('VN03', N'Đàn Gà Thịt Lương Phượng', '2024-05-10', 'C03', 3000),
('VN04', N'Đàn Heo Nái Landrace', '2024-03-01', 'C04', 100);
GO

-- Bảng liên kết Nhân Viên và Vật Nuôi
INSERT INTO [dbo].[NhanVien_VatNuoi] ([MaNhanVien], [MaVatNuoi]) VALUES
('NV02', 'VN01'),
('NV03', 'VN02'),
('NV03', 'VN03'),
('NV05', 'VN04');
GO

-- Bảng Hóa Đơn và Chi Tiết Hóa Đơn
DECLARE @MaHD1 NVARCHAR(10) = 'HD' + UPPER(SUBSTRING(REPLACE(CAST(NEWID() AS NVARCHAR(36)), '-', ''), 1, 8));
INSERT INTO [dbo].[HoaDon] ([MaHoaDon], [NgayLap]) VALUES (@MaHD1, '2024-05-01');
INSERT INTO [dbo].[ChiTietHoaDon] ([MaHoaDon], [STT], [LoaiMatHang], [MaMatHang], [SoLuong], [DonGia], [MaNhanVien], [MaNhaCungCap]) VALUES
(@MaHD1, 1, N'Vật tư', 'VT01', 100, 350000, 'NV01', 'NCC01'),
(@MaHD1, 2, N'Vật tư', 'VT02', 200, 15000, 'NV01', 'NCC02');

DECLARE @MaHD2 NVARCHAR(10) = 'HD' + UPPER(SUBSTRING(REPLACE(CAST(NEWID() AS NVARCHAR(36)), '-', ''), 1, 8));
INSERT INTO [dbo].[HoaDon] ([MaHoaDon], [NgayLap]) VALUES (@MaHD2, '2024-06-10');
INSERT INTO [dbo].[ChiTietHoaDon] ([MaHoaDon], [STT], [LoaiMatHang], [MaMatHang], [SoLuong], [DonGia], [MaNhanVien], [MaNhaCungCap]) VALUES
(@MaHD2, 1, N'Vật tư', 'VT03', 150, 320000, 'NV01', 'NCC03');
GO
CREATE TABLE [dbo].[LichSuTangTruong] (
    [ID] INT IDENTITY(1,1) PRIMARY KEY,   -- Khóa chính, tự động tăng, tương ứng với ID trong C#
    [MaVatNuoi] NVARCHAR(50) NOT NULL,     -- NVARCHAR(50) cho mã vật nuôi (có thể cần khớp với độ dài trong C# nếu là string)
    [NgayKiemTra] DATETIME NOT NULL,       -- DATETIME cho DateTime trong C#, nếu bạn chỉ cần ngày thì dùng DATE
    [SoLuongMau] INT,                      -- INT cho int?, cho phép NULL
    [TongCanNangMau] FLOAT,                 -- FLOAT cho double?, cho phép NULL (DOUBLE PRECISION trong một số DB khác)
    [CanNangTrungBinhMau] FLOAT,            -- FLOAT cho double?, cho phép NULL
    [SoLuongThucTeTrongDan] INT,           -- INT cho int?, cho phép NULL
    [TongTrongLuongUocTinh] FLOAT,         -- FLOAT cho double?, cho phép NULL
    [GhiChu] NVARCHAR(MAX)                  -- NVARCHAR(MAX) cho string, cho phép NULL
    -- Thêm ràng buộc khóa ngoại nếu cần, ví dụ:
    CONSTRAINT FK_LichSuTangTruong_VatNuoi FOREIGN KEY ([MaVatNuoi]) REFERENCES [dbo].[VatNuoi]([MaVatNuoi])
);
go
-- Bảng Lịch Sử Tăng Trưởng 
IF OBJECT_ID('dbo.LichSuTangTruong', 'U') IS NOT NULL
BEGIN
    INSERT INTO [dbo].[LichSuTangTruong] ([MaVatNuoi], [NgayKiemTra], [SoLuongMau], [TongCanNangMau], [CanNangTrungBinhMau], [SoLuongThucTeTrongDan], [TongTrongLuongUocTinh], [GhiChu]) VALUES

    ('VN01', '2024-03-15', 10, 2550.5, 255.05, 50, 12752.5, N'Đàn phát triển tốt, sữa ổn định'),
    ('VN01', '2024-05-15', 10, 3100.0, 310.0, 49, 15190.0, N'Giảm 1 con do bệnh. Cần theo dõi thêm.'),
    ('VN02', '2024-04-20', 50, 95.0, 1.9, 1980, 3762.0, N'Tỷ lệ đẻ trứng đạt 92%'),
    ('VN03', '2024-06-15', 50, 110.0, 2.2, 2990, 6578.0, N'Gà ăn khỏe, tăng trọng đều.'),
    ('VN01', '2024-06-18', 10, 3250.0, 325.0, 49, 15925.0, N'Sản lượng sữa tăng nhẹ.'),
    ('VN04', '2024-04-01', 20, 2200.0, 110.0, 100, 11000.0, N'Heo nái giai đoạn mang thai, sức khỏe tốt.'),
    ('VN04', '2024-05-01', 20, 2500.0, 125.0, 98, 12250.0, N'Giảm 2 con do yếu. Bổ sung dinh dưỡng.'),
    ('VN04', '2024-06-01', 20, 2900.0, 145.0, 98, 14210.0, N'Heo mẹ khỏe mạnh, dự kiến sinh vào cuối tháng.'),
    ('VN02', '2024-05-20', 50, 100.0, 2.0, 1975, 3950.0, N'Tỷ lệ đẻ ổn định.'),
    ('VN02', '2024-06-20', 50, 105.0, 2.1, 1970, 4137.0, N'Loại thải 5 con năng suất kém.'),
    ('VN03', '2024-07-15', 50, 125.0, 2.5, 2980, 7450.0, N'Chuẩn bị xuất chuồng đợt 1.'),
    ('VN01', '2024-07-18', 10, 3400.0, 340.0, 49, 16660.0, N'Sức khỏe đàn tốt.'),
    ('VN04', '2024-07-01', 20, 3100.0, 155.0, 98, 15190.0, N'Đã sinh sản, số heo con là 120.');
END
GO
