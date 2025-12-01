using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs.TaiKhoan
{
    public class CreateTaiKhoanRequestDto
    {
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống !")]
        [MaxLength(50)]
        public string? TenDangNhap {  get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống !")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",ErrorMessage = "Password phải có ít nhất 8 ký tự, gồm chữ hoa, chữ thường, số và ký tự đặc biệt")]

        public string? MatKhau { get; set; }

        [Required(ErrorMessage = "Vai trò của tài khoản không được bỏ trống !")]
        public string? VaiTro { get; set; }
        public string? TrangThai { get; set; }

        [Required(ErrorMessage = "Phải nhập mã nhân viên để bảo toàn ràng buộc !")]
        public int MaNhanVien { get; set; }
    }
}
