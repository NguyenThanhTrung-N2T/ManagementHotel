namespace ManagementHotel.DTOs.DatPhong
{
    // DTO chi tiết phiếu đặt phòng
    public class DatPhongResponseDto
    {
        public int MaDatPhong { get; set; }
        public string? TrangThai { get; set; } // Đã đặt, Đang ở, Đã hủy
        public DateTime NgayNhanPhong { get; set; }
        public DateTime NgayTraPhong { get; set; }

        // Thông tin khách hàng cơ bản
        public string? KhachHangHoTen { get; set; }
        public string? KhachHangSoDienThoai { get; set; }

        // Thông tin phòng cơ bản
        public string? SoPhong { get; set; }
        public string? TenLoaiPhong { get; set; }
        public int GiaTheoDem { get; set; }

        // Thông tin hóa đơn (nếu đã tạo)
        public HoaDonInfoDTO? HoaDon { get; set; }
    }

    // DTO gọn cho hóa đơn
    public class HoaDonInfoDTO
    {
        public int MaHoaDon { get; set; }
        public string? TrangThaiThanhToan { get; set; } // Chưa thanh toán, Đã thanh toán
        public int TongTien { get; set; }
        public DateTime NgayLap { get; set; }
    }


}
