using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs.NhanVien
{
    public class CreateNhanVienRequestDto
    {
        [Required(ErrorMessage = "Họ tên không được để trống !")]
        [MaxLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự !")]
        public string? HoTen { get; set; }

        [Required(ErrorMessage = "Chức vụ không được để trống !")]
        [MaxLength(50, ErrorMessage = "Chức vụ không được vượt quá 50 ký tự !")]
        public string? ChucVu { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống !")]
        [MaxLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự !")]
        public string? SoDienThoai { get; set; }

        [Required(ErrorMessage = "CCCD không được để trống !")]
        [MaxLength(20, ErrorMessage = "CCCD không được vượt quá 20 ký tự !")]
        public string? CCCD { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ !")]
        public string? Email { get; set; }

        public string? DiaChi { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống !")]
        public DateTime NgaySinh { get; set; }


        [Required(ErrorMessage = "Lương không được để trống !")]
        [Range(0, int.MaxValue, ErrorMessage = "Lương phải lớn hơn 0 !")]
        public int Luong { get; set; }
    }
}