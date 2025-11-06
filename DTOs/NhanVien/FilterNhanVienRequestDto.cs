using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs.NhanVien
{
    public class FilterNhanVienRequestDto
    {
        [MaxLength(100, ErrorMessage = "Tên nhân viên không quá 100 ký tự !")]
        public string? TenNhanVien { get; set; }

        [MaxLength(20, ErrorMessage = "CCCD không quá 20 ký tự !")]
        public string? CCCD { get; set; }

        [MaxLength(15, ErrorMessage = "Số điện thoại không quá 15 ký tự !")]
        public string? SoDienThoai { get; set; }

        [MaxLength(50, ErrorMessage = "Chức vụ không quá 50 ký tự !")]
        public string? ChucVu { get; set; }
    }
}