using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs
{
    public class LoginTaiKhoanRequestDto
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string? TenDangNhap { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string? MatKhau { get; set; }
    }
}
