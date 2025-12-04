USE master;
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'QuanLyKhachSan')
BEGIN
    ALTER DATABASE QuanLyKhachSan SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyKhachSan;
END
GO

CREATE DATABASE QuanLyKhachSan;
GO
USE QuanLyKhachSan;
GO

-------tạo bảng 
-- Loại phòng
CREATE TABLE LoaiPhong (
    MaLoaiPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiPhong NVARCHAR(100) UNIQUE NOT NULL,
    MoTa NVARCHAR(255),
	TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Trống',
    GiaTheoDem INT NOT NULL CHECK (GiaTheoDem >= 0),
	CONSTRAINT CK_LoaiPhong_TrangThai check (TrangThai IN (N'Hoạt động',N'Ngưng hoạt động'))
);
-- Phòng
CREATE TABLE Phong (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    SoPhong NVARCHAR(20) UNIQUE NOT NULL,
    MaLoaiPhong INT NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Trống',
    GhiChu NVARCHAR(255),
    FOREIGN KEY (MaLoaiPhong) REFERENCES LoaiPhong(MaLoaiPhong),
    CONSTRAINT CK_Phong_TrangThai CHECK (TrangThai IN (N'Trống', N'Đang dọn', N'Đã đặt', N'Đang ở', N'Bảo trì'))
);
-- Khách hàng
CREATE TABLE KhachHang (
    MaKhachHang INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(15),
	TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Hoạt động',
    CCCD NVARCHAR(12) NOT NULL UNIQUE,
    Email NVARCHAR(100),
    DiaChi NVARCHAR(255),
	CONSTRAINT CK_KhachHang_TrangThai check (TrangThai IN (N'Hoạt động',N'Ngưng hoạt động'))
);
-- Nhân viên
CREATE TABLE NhanVien (
    MaNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    CCCD NVARCHAR(12) UNIQUE NOT NULL,
    ChucVu NVARCHAR(50) DEFAULT N'Lễ tân',
    SoDienThoai NVARCHAR(15),
    Email NVARCHAR(100),
    Luong INT CHECK (Luong >= 0),
    DiaChi NVARCHAR(255),
	CONSTRAINT CK_NhanVien_ChuVu CHECK ( ChucVu in (N'Lễ tân',N'Nhân viên',N'Quản lý'))
);
-- Tài khoản
CREATE TABLE TaiKhoan (
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    VaiTro NVARCHAR(20) CHECK (VaiTro IN (N'Lễ tân',N'Nhân viên',N'Quản lý')),
    TrangThai NVARCHAR(20) DEFAULT N'Hoạt động',
    MaNhanVien INT,
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    CONSTRAINT CK_TaiKhoan_TrangThai CHECK (TrangThai IN (N'Hoạt động', N'Bị khóa', N'Tạm ngưng'))
);
-- Đặt phòng
CREATE TABLE DatPhong (
    MaDatPhong INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT NOT NULL,
    MaPhong INT NOT NULL,
    NgayNhanPhong DATE NOT NULL,
    NgayTraPhong DATE NOT NULL,
    TrangThai NVARCHAR(50) DEFAULT N'Đã đặt',
    GhiChu NVARCHAR(255),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong),
    CONSTRAINT CK_DatPhong_TrangThai CHECK (TrangThai IN (N'Đã đặt', N'Đã hủy', N'Đang ở'))
);
-- Dịch vụ
CREATE TABLE DichVu (
    MaDichVu INT IDENTITY(1,1) PRIMARY KEY,
    TenDichVu NVARCHAR(100) NOT NULL UNIQUE,
    DonVi NVARCHAR(50),
	TrangThai NVARCHAR(50) DEFAULT N'Hoạt động',
    DonGia INT NOT NULL CHECK (DonGia >= 0),
    CONSTRAINT CK_DichVu_DonVi CHECK (DonVi IN (N'Suất', N'Kg', N'Ngày', N'Giờ')),
	CONSTRAINT CK_DichVu_TrangThai check ( TrangThai in (N'Hoạt động',N'Ngưng hoạt động'))
);

-- Hóa đơn
CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaDatPhong INT NOT NULL,
    NgayLap DATE DEFAULT GETDATE(),
    TongTien INT NOT NULL DEFAULT 0,
    TrangThaiThanhToan NVARCHAR(20) DEFAULT N'Chưa thanh toán' 
        CHECK (TrangThaiThanhToan IN (N'Chưa thanh toán', N'Đã thanh toán')),
    FOREIGN KEY (MaDatPhong) REFERENCES DatPhong(MaDatPhong),
    CONSTRAINT UQ_HoaDon_MaDatPhong UNIQUE (MaDatPhong)
);


-- Chi tiết hóa đơn
CREATE TABLE ChiTietHoaDon (
    MaChiTietHD INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon INT NOT NULL,
    MaDichVu INT NULL,
    SoLuong INT DEFAULT 1 CHECK (SoLuong >= 1),
    DonGia INT NOT NULL CHECK (DonGia >= 0),
    MoTa NVARCHAR(255),
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (MaDichVu) REFERENCES DichVu(MaDichVu)
);

-- Báo cáo doanh thu
CREATE TABLE BaoCaoDoanhThu (
    MaBaoCao INT IDENTITY(1,1) PRIMARY KEY,
    Ngay DATE NOT NULL,
    Thang INT NOT NULL,
    Nam INT NOT NULL,
    TongDoanhThu INT NOT NULL DEFAULT 0,
    GhiChu NVARCHAR(255)
);

-- 5. Procedure đồng bộ trạng thái phòng theo ngày
IF OBJECT_ID('CapNhatTrangThaiPhong', 'P') IS NOT NULL
    DROP PROCEDURE CapNhatTrangThaiPhong;
CREATE OR ALTER PROCEDURE CapNhatTrangThaiPhong
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NgayHienTai DATE = CAST(GETDATE() AS DATE);

    -- Phòng có đặt phòng bị hủy → Trống
    UPDATE p
    SET TrangThai = N'Trống'
    FROM Phong p
    JOIN DatPhong dp ON p.MaPhong = dp.MaPhong
    WHERE dp.TrangThai = N'Đã hủy';

    -- Phòng đã hết hạn trả phòng → Trống
    UPDATE p
    SET TrangThai = N'Trống'
    FROM Phong p
    JOIN DatPhong dp ON p.MaPhong = dp.MaPhong
    WHERE @NgayHienTai >= dp.NgayTraPhong;

    -- Phòng có đặt phòng nhưng chưa tới ngày nhận → Đã đặt
    UPDATE p
    SET TrangThai = N'Đã đặt'
    FROM Phong p
    JOIN DatPhong dp ON p.MaPhong = dp.MaPhong
    WHERE dp.TrangThai = N'Đã đặt'
      AND @NgayHienTai < dp.NgayNhanPhong;

    -- Phòng trong khoảng ngày nhận đến ngày trả và trạng thái Đã đặt → Đã đặt
    UPDATE p
    SET TrangThai = N'Đã đặt'
    FROM Phong p
    JOIN DatPhong dp ON p.MaPhong = dp.MaPhong
    WHERE dp.TrangThai = N'Đã đặt'
      AND @NgayHienTai >= dp.NgayNhanPhong
      AND @NgayHienTai < dp.NgayTraPhong;

    -- Phòng trong khoảng ngày nhận đến ngày trả và trạng thái Đang ở → Đang ở
    UPDATE p
    SET TrangThai = N'Đang ở'
    FROM Phong p
    JOIN DatPhong dp ON p.MaPhong = dp.MaPhong
    WHERE dp.TrangThai = N'Đang ở'
      AND @NgayHienTai >= dp.NgayNhanPhong
      AND @NgayHienTai < dp.NgayTraPhong;

    -- Phòng không có đặt phòng nào → Trống
    UPDATE p
    SET TrangThai = N'Trống'
    FROM Phong p
    WHERE NOT EXISTS (
        SELECT 1 FROM DatPhong dp
        WHERE dp.MaPhong = p.MaPhong
          AND dp.TrangThai <> N'Đã hủy'
          AND @NgayHienTai < dp.NgayTraPhong
    );
END;
GO

-- =====================================================
-- 4. DỮ LIỆU MẪU
-- =====================================================

-- Loại phòng
INSERT INTO LoaiPhong (TenLoaiPhong, MoTa, TrangThai, GiaTheoDem)
VALUES
(N'Thường', N'Phòng tiêu chuẩn cho 2 người', N'Hoạt động', 500000),
(N'Cao cấp', N'Phòng có view biển', N'Hoạt động', 1200000),
(N'VIP', N'Phòng cao cấp có ban công riêng', N'Hoạt động', 2500000),
(N'Phòng gia đình', N'Phòng rộng cho 4 người', N'Hoạt động', 1800000),
(N'Phòng đơn', N'Phòng nhỏ cho 1 người', N'Ngưng hoạt động', 300000);

-- Phòng
INSERT INTO Phong (SoPhong, MaLoaiPhong, TrangThai, GhiChu)
VALUES
(N'101', 1, N'Trống', N'Tầng 1'),
(N'102', 2, N'Đã đặt', N'Tầng 1'),
(N'103', 3, N'Đang ở', N'Tầng 1'),
(N'104', 4, N'Bảo trì', N'Tầng 1'),
(N'201', 1, N'Trống', N'Tầng 2'),
(N'202', 2, N'Đang dọn', N'Tầng 2'),
(N'203', 3, N'Trống', N'Tầng 2'),
(N'301', 4, N'Trống', N'Tầng 3'),
(N'302', 2, N'Trống', N'Tầng 3'),
(N'303', 1, N'Trống', N'Tầng 3');


-- Nhân viên
INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, ChucVu, SoDienThoai, Email, Luong, DiaChi)
VALUES
(N'Nguyễn Văn A', '1995-02-10', '012345678901', N'Lễ tân', '0901234567', 'a@hotel.vn', 8000000, N'Đà Nẵng'),
(N'Lê Thị B', '1990-07-25', '098765432109', N'Quản lý', '0909999999', 'b@hotel.vn', 12000000, N'Hà Nội'),
(N'Phạm Văn C', '1988-03-15', '111222333444', N'Nhân viên', '0911111111', 'c@hotel.vn', 6000000, N'Hồ Chí Minh'),
(N'Hoàng Thị D', '1992-11-20', '555666777888', N'Lễ tân', '0922222222', 'd@hotel.vn', 7000000, N'Huế'),
(N'Đỗ Văn E', '1985-09-05', '999888777666', N'Quản lý', '0933333333', 'e@hotel.vn', 15000000, N'Hải Phòng');


-- Khách hàng
INSERT INTO KhachHang (HoTen, SoDienThoai, TrangThai, CCCD, Email, DiaChi)
VALUES
(N'Trần Quốc Cường', '0908888777',N'Hoạt động', '123456789012', 'cuong@gmail.com', N'Đà Nẵng'),
(N'Nguyễn Thị Hoa', '0905555444',N'Hoạt động', '987654321012', 'hoa@gmail.com', N'Hà Nội'),
(N'Lê Văn Nam', '0912345678',N'Hoạt động', '111222333444', 'nam@gmail.com', N'Hồ Chí Minh'),
(N'Phạm Thị Mai', '0923456789',N'Hoạt động', '555666777888', 'mai@gmail.com', N'Huế'),
(N'Hoàng Văn Bình', '0934567890',N'Hoạt động', '999888777666', 'binh@gmail.com', N'Hải Phòng');


-- Dịch vụ
INSERT INTO DichVu (TenDichVu, DonVi, TrangThai, DonGia)
VALUES
(N'Ăn sáng', N'Suất', N'Hoạt động', 100000),
(N'Giặt ủi', N'Kg', N'Hoạt động', 50000),
(N'Thuê xe máy', N'Ngày', N'Hoạt động', 200000),
(N'Đưa đón sân bay', N'Giờ', N'Hoạt động', 300000),
(N'Spa', N'Giờ', N'Ngưng hoạt động', 400000);


-- Đặt phòng
INSERT INTO DatPhong (MaKhachHang, MaPhong, NgayNhanPhong, NgayTraPhong, TrangThai, GhiChu)
VALUES
(1, 1, '2025-12-05', '2025-12-07', N'Đã đặt', N'Khách đặt trước 2 ngày'),
(2, 2, '2025-12-06', '2025-12-08', N'Đã đặt', N'Khách VIP'),
(3, 3, '2025-12-04', '2025-12-06', N'Đang ở', N'Đã nhận phòng'),
(4, 4, '2025-12-01', '2025-12-03', N'Đã hủy', N'Hủy do bận việc'),
(5, 5, '2025-12-10', '2025-12-12', N'Đã đặt', N'Đặt online'),
(1, 6, '2025-12-02', '2025-12-04', N'Đang ở', N'Khách nước ngoài'),
(2, 7, '2025-12-07', '2025-12-09', N'Đã đặt', N'Khách quen'),
(3, 8, '2025-12-08', '2025-12-10', N'Đã đặt', N'Khách đoàn'),
(4, 9, '2025-12-03', '2025-12-05', N'Đang ở', N'Khách công tác'),
(5, 10, '2025-12-11', '2025-12-13', N'Đã đặt', N'Khách đặt qua điện thoại');

-- Hóa đơn (mỗi đặt phòng chỉ có 1 hóa đơn)
INSERT INTO HoaDon (MaDatPhong, NgayLap, TrangThaiThanhToan)
VALUES
(1, GETDATE(), N'Chưa thanh toán'),
(2, GETDATE(), N'Chưa thanh toán'),
(3, GETDATE(), N'Đã thanh toán'),
(5, GETDATE(), N'Chưa thanh toán'),
(6, GETDATE(), N'Đã thanh toán'),
(7, GETDATE(), N'Chưa thanh toán'),
(9, GETDATE(), N'Đã thanh toán');

-- Chi tiết hóa đơn
INSERT INTO ChiTietHoaDon (MaHoaDon, MaDichVu, SoLuong, DonGia, MoTa)
VALUES
(1, 1, 2, 100000, N'Ăn sáng 2 suất'),
(1, 2, 3, 50000, N'Giặt ủi 3kg'),

(2, 3, 2, 200000, N'Thuê xe máy 2 ngày'),

(3, 1, 1, 100000, N'Ăn sáng 1 suất'),
(3, 2, 2, 50000, N'Giặt ủi 2kg'),

(5, 2, 4, 50000, N'Giặt ủi 4kg'),
(5, 3, 1, 200000, N'Thuê xe máy 1 ngày'),

(6, 1, 2, 100000, N'Ăn sáng 2 suất'),

(7, 1, 2, 100000, N'Ăn sáng 2 suất'),
(7, 2, 2, 50000, N'Giặt ủi 2kg'),
(7, 4, 1, 300000, N'Đưa đón sân bay');



select * from DatPhong;
select * from HoaDon;
select * from HoaDon,ChiTietHoaDon  
where HoaDon.MaHoaDon = ChiTietHoaDon.MaHoaDon and HoaDon.MaHoaDon = 5

select *from TaiKhoan;
delete from Phong;
delete from DatPhong;
delete from HoaDon;
delete from ChiTietHoaDon
DBCC CHECKIDENT ('Phong', RESEED, 0);
DBCC CHECKIDENT ('DatPhong', RESEED, 0);
DBCC CHECKIDENT ('HoaDon', RESEED, 0);
DBCC CHECKIDENT ('ChiTietHoaDon', RESEED, 0);

delete from HoaDon where MaHoaDon = 6;
delete from DatPhong where MaDatPhong = 7;

SELECT hd.MaHoaDon,
       DATEDIFF(DAY, dp.NgayNhanPhong, dp.NgayTraPhong) * lp.GiaTheoDem AS TienPhong,
       ISNULL(SUM(ct.SoLuong * ct.DonGia),0) AS TienDichVu,
       DATEDIFF(DAY, dp.NgayNhanPhong, dp.NgayTraPhong) * lp.GiaTheoDem 
       + ISNULL(SUM(ct.SoLuong * ct.DonGia),0) AS TongTien
FROM HoaDon hd
JOIN DatPhong dp ON hd.MaDatPhong = dp.MaDatPhong
JOIN Phong p ON dp.MaPhong = p.MaPhong
JOIN LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong
LEFT JOIN ChiTietHoaDon ct ON hd.MaHoaDon = ct.MaHoaDon
GROUP BY hd.MaHoaDon, dp.NgayNhanPhong, dp.NgayTraPhong, lp.GiaTheoDem;

