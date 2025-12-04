using System.Text.Json.Serialization;

namespace ManagementHotel.DTOs.KhachHang
{
    public class KhachHangResponseDto
    {
        public int MaKhachHang { get; set; }
        public string? HoTen { get; set; }
        public string? TrangThai { get; set; }
        public string? CCCD { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
    }
}
