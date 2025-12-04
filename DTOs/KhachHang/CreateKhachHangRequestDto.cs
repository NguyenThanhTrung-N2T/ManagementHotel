using System.ComponentModel.DataAnnotations;
namespace ManagementHotel.DTOs.KhachHang
{
    public class CreateKhachHangRequestDto
    {
        [Required(ErrorMessage = "Họ tên không được để trống !")]
        [MaxLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự !")]
        public string? HoTen { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống !")]
        [MaxLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự !")]
        public string? SoDienThoai { get; set; }
        [Required(ErrorMessage = "Trạng thái không được để trống !")]
        public string? TrangThai { get; set; }
        [Required(ErrorMessage = "CCCD không được để trống !")]
        [MaxLength(12, ErrorMessage = "CCCD không được vượt quá 12 ký tự !")]
        public string? CCCD { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ !")]
        public string? Email { get; set; }
        public string? DiaChi { get; set; }

    }
}
