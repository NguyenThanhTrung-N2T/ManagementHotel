/**************************************************************
  File: QuanLyKhachSan_v5.1.sql
  Mô tả:
    - DB quản lý khách sạn nhỏ
    - Flow:
        + Đặt phòng → Phòng "Đã đặt"
        + Nhận phòng → Phòng "Đang ở" → Tạo Hóa đơn tự động
        + Hóa đơn có trạng thái thanh toán
        + Trigger cập nhật doanh thu khi thanh toán
**************************************************************/

-- =====================================================
-- 1. XÓA DATABASE CŨ VÀ TẠO MỚI
-- =====================================================
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

-- =====================================================
-- 2. TẠO CẤU TRÚC BẢNG
-- =====================================================

-- Loại phòng
CREATE TABLE LoaiPhong (
    MaLoaiPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiPhong NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255),
    GiaTheoDem INT NOT NULL CHECK (GiaTheoDem >= 0)
);

-- Phòng
CREATE TABLE Phong (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    SoPhong NVARCHAR(20) NOT NULL,
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
    CCCD NVARCHAR(20),
    Email NVARCHAR(100),
    DiaChi NVARCHAR(255)
);

-- Nhân viên
CREATE TABLE NhanVien (
    MaNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    CCCD NVARCHAR(20) UNIQUE NOT NULL,
    ChucVu NVARCHAR(50),
    SoDienThoai NVARCHAR(15),
    Email NVARCHAR(100),
    Luong INT CHECK (Luong >= 0),
    DiaChi NVARCHAR(255)
);

-- Tài khoản
CREATE TABLE TaiKhoan (
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    VaiTro NVARCHAR(20) CHECK (VaiTro IN (N'Admin', N'Nhân Viên')),
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
    TenDichVu NVARCHAR(100) NOT NULL,
    DonVi NVARCHAR(50),
    DonGia INT NOT NULL CHECK (DonGia >= 0),
    CONSTRAINT CK_DichVu_DonVi CHECK (DonVi IN (N'Suất', N'Kg', N'Ngày', N'Giờ'))
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

-- =====================================================
-- 3. TRIGGERS
-- =====================================================
DROP TRIGGER IF EXISTS trg_CapNhatTienPhongKhiTaoHoaDon;
GO
CREATE OR ALTER TRIGGER trg_CapNhatTongTienHoaDon
ON ChiTietHoaDon
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE hd
    SET TongTien =
        ISNULL((
            SELECT SUM(ct.SoLuong * ct.DonGia)
            FROM ChiTietHoaDon ct
            WHERE ct.MaHoaDon = hd.MaHoaDon
        ), 0)
        +
        ISNULL((
            SELECT DATEDIFF(DAY, dp.NgayNhanPhong, dp.NgayTraPhong) * lp.GiaTheoDem
            FROM DatPhong dp
            JOIN Phong p ON dp.MaPhong = p.MaPhong
            JOIN LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong
            WHERE dp.MaDatPhong = hd.MaDatPhong
        ), 0)
    FROM HoaDon hd
    WHERE hd.MaHoaDon IN (
        SELECT DISTINCT MaHoaDon FROM inserted
        UNION
        SELECT DISTINCT MaHoaDon FROM deleted
    );
END;
GO
CREATE OR ALTER TRIGGER trg_CapNhatTienPhongKhiSuaDatPhong
ON DatPhong
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE hd
    SET TongTien =
        ISNULL((
            SELECT SUM(ct.SoLuong * ct.DonGia)
            FROM ChiTietHoaDon ct
            WHERE ct.MaHoaDon = hd.MaHoaDon
        ), 0)
        +
        ISNULL((
            SELECT DATEDIFF(DAY, i.NgayNhanPhong, i.NgayTraPhong) * lp.GiaTheoDem
            FROM inserted i
            JOIN Phong p ON i.MaPhong = p.MaPhong
            JOIN LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong
            WHERE i.MaDatPhong = hd.MaDatPhong
        ), 0)
    FROM HoaDon hd
    WHERE hd.MaDatPhong IN (SELECT MaDatPhong FROM inserted);
END;
GO

-- 🔹 Tổng tiền hóa đơn
DROP TRIGGER IF EXISTS dbo.trg_CapNhatTongTienHoaDon;
GO
CREATE TRIGGER trg_CapNhatTongTienHoaDon
ON ChiTietHoaDon
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE hd
    SET TongTien = ISNULL((
        SELECT SUM(ct.SoLuong * ct.DonGia)
        FROM ChiTietHoaDon ct
        WHERE ct.MaHoaDon = hd.MaHoaDon
    ), 0)
    FROM HoaDon hd
    WHERE hd.MaHoaDon IN (
        SELECT DISTINCT MaHoaDon FROM inserted
        UNION
        SELECT DISTINCT MaHoaDon FROM deleted
    );
END;
GO

-- 🔹 Cập nhật doanh thu khi thanh toán
CREATE TRIGGER trg_CapNhatBaoCaoDoanhThu
ON HoaDon
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ngay DATE, @tien INT;

    SELECT @ngay = i.NgayLap, @tien = i.TongTien
    FROM inserted i
    JOIN deleted d ON i.MaHoaDon = d.MaHoaDon
    WHERE i.TrangThaiThanhToan = N'Đã thanh toán' AND d.TrangThaiThanhToan <> N'Đã thanh toán';

    IF @ngay IS NOT NULL
    BEGIN
        IF EXISTS (SELECT 1 FROM BaoCaoDoanhThu WHERE Ngay = @ngay)
            UPDATE BaoCaoDoanhThu
            SET TongDoanhThu = TongDoanhThu + @tien
            WHERE Ngay = @ngay;
        ELSE
            INSERT INTO BaoCaoDoanhThu (Ngay, Thang, Nam, TongDoanhThu)
            VALUES (@ngay, MONTH(@ngay), YEAR(@ngay), @tien);
    END
END;
GO

-- =====================================================
-- 5. Procedure đồng bộ trạng thái phòng theo ngày
-- =====================================================
CREATE OR ALTER PROCEDURE CapNhatTrangThaiPhong
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NgayHienTai DATE = CAST(GETDATE() AS DATE);

    -- 1️⃣ Các phòng đang có đặt phòng bị hủy → để nguyên trạng thái
    -- Không cần update gì

    -- 2️⃣ Các phòng trong khoảng ngày đặt phòng và trạng thái Đã đặt → Phòng hiển thị 'Đã đặt'
    UPDATE p
    SET TrangThai = N'Đã đặt'
    FROM Phong p
    JOIN DatPhong dp ON p.MaPhong = dp.MaPhong
    WHERE dp.TrangThai = N'Đã đặt'
      AND @NgayHienTai >= dp.NgayNhanPhong
      AND @NgayHienTai < dp.NgayTraPhong;

    -- 3️⃣ Các phòng trong khoảng ngày nhận phòng và trạng thái Đang ở → Phòng hiển thị 'Đang ở'
    UPDATE p
    SET TrangThai = N'Đang ở'
    FROM Phong p
    JOIN DatPhong dp ON p.MaPhong = dp.MaPhong
    WHERE dp.TrangThai = N'Đang ở'
      AND @NgayHienTai >= dp.NgayNhanPhong
      AND @NgayHienTai < dp.NgayTraPhong;
END;
GO

-- =====================================================
-- 4. DỮ LIỆU MẪU
-- =====================================================

-- Loại phòng
INSERT INTO LoaiPhong (TenLoaiPhong, MoTa, GiaTheoDem)
VALUES 
(N'Thường', N'Phòng tiêu chuẩn cho 2 người', 500000),
(N'Cao cấp', N'Phòng có view biển', 1200000),
(N'VIP', N'Phòng cao cấp có ban công riêng', 2500000);

-- Phòng
INSERT INTO Phong (SoPhong, MaLoaiPhong, TrangThai, GhiChu)
VALUES
(N'101', 1, N'Trống', N'Tầng 1'),
(N'102', 2, N'Trống', N'Tầng 1'),
(N'201', 3, N'Trống', N'Tầng 2');

-- Nhân viên
INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, ChucVu, SoDienThoai, Email, Luong, DiaChi)
VALUES
(N'Nguyễn Văn A', '1995-02-10', '012345678901', N'Lễ tân', '0901234567', 'a@hotel.vn', 8000000, N'Đà Nẵng'),
(N'Lê Thị B', '1990-07-25', '098765432109', N'Quản lý', '0909999999', 'b@hotel.vn', 12000000, N'Hà Nội');

-- Khách hàng
INSERT INTO KhachHang (HoTen, SoDienThoai, CCCD, Email, DiaChi)
VALUES
(N'Trần Quốc Cường', '0908888777', '123456789', 'cuong@gmail.com', N'Đà Nẵng'),
(N'Nguyễn Thị Hoa', '0905555444', '987654321', 'hoa@gmail.com', N'Hà Nội');

-- Dịch vụ
INSERT INTO DichVu (TenDichVu, DonVi, DonGia)
VALUES
(N'Ăn sáng', N'Suất', 100000),
(N'Giặt ủi', N'Kg', 50000),
(N'Thuê xe máy', N'Ngày', 200000);

-- Đặt phòng
INSERT INTO DatPhong (MaKhachHang, MaPhong, NgayNhanPhong, NgayTraPhong, TrangThai)
VALUES
(1, 1, '2025-12-05', '2025-12-07', N'Đã đặt'),
(2, 2, '2025-12-06', '2025-12-08', N'Đã đặt');

-- Nhận phòng → cập nhật trạng thái
UPDATE DatPhong SET TrangThai = N'Đang ở' WHERE MaDatPhong = 1;

-- Tạo hóa đơn cho đặt phòng số 1 (do constraint UNIQUE nên mỗi MaDatPhong chỉ có 1 hóa đơn)
INSERT INTO HoaDon (MaDatPhong, NgayLap, TrangThaiThanhToan)
VALUES (1, GETDATE(), N'Chưa thanh toán');

-- Thêm chi tiết hóa đơn
INSERT INTO ChiTietHoaDon (MaHoaDon, MaDichVu, SoLuong, DonGia, MoTa)
VALUES
(1, 1, 2, 100000, N'Ăn sáng 2 suất'),
(1, NULL, 1, 500000, N'Tiền phòng 1 đêm');

-- Thanh toán hóa đơn → trigger cập nhật doanh thu
UPDATE HoaDon SET TrangThaiThanhToan = N'Đã thanh toán' WHERE MaHoaDon = 1;


PRINT N'✅ Database QuanLyKhachSan_v5.1 đã tạo xong, flow đặt phòng → nhận phòng → hóa đơn → thanh toán doanh thu.';
GO

select * from DatPhong;
select * from HoaDon;
delete from HoaDon where MaHoaDon = 8;
delete from DatPhong where MaDatPhong = 16;

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