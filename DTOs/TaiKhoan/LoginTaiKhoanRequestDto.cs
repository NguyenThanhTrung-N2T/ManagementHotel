using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs.TaiKhoan
{
    public class LoginTaiKhoanRequestDto
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string? TenDangNhap { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$", ErrorMessage = "Password phải có ít nhất 8 ký tự, gồm chữ hoa, chữ thường, số và ký tự đặc biệt")]
        public string? MatKhau { get; set; } 
    }
}
