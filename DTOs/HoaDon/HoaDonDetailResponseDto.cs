using ManagementHotel.DTOs.ChiTietHoaDon;

namespace ManagementHotel.DTOs.HoaDon
{
    public class HoaDonDetailResponseDto
    {
        public int MaHoaDon { get; set; }
        public int MaDatPhong { get; set; }
        public DateTime NgayLap { get; set; }
        public string? TrangThaiThanhToan { get; set; }

        // Thông tin đặt phòng (nếu muốn hiển thị kèm)
        public string? TenKhachHang { get; set; }
        public string? SoPhong { get; set; }

        // Danh sách chi tiết hóa đơn
        public List<ChiTietHoaDonResponseDto> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDonResponseDto>();
    }
}
