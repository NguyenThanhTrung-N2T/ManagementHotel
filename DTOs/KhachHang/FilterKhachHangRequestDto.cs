using System.ComponentModel.DataAnnotations;
namespace ManagementHotel.DTOs.KhachHang
{
    public class FilterKhachHangRequestDto
    {
        [MaxLength(100,ErrorMessage = "Tên khách hàng không quá 100 ký tự !")]
        public string? TenKhachHang { get; set; }
        [MaxLength(12, ErrorMessage = "CCCD không quá 12 ký tự !")]
        public string? CCCD { get; set; }
        [MaxLength(15, ErrorMessage = "Số điện thoại không quá 15 ký tự !")]
        public string? SoDienThoai { get; set; }
        public string? TrangThai { get; set; }
    }
}
