using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs.KhachHang
{
    public class UpdateKhachHangRequest
    {
        [Required(ErrorMessage = "Họ tên không được để trống !")]
        [MaxLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự !")]
        public string? HoTen { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống !")]
        [MaxLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự !")]
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
    }
}
