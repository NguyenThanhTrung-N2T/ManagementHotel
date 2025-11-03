using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ManagementHotel.Models
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public string? VaiTro { get; set; }
        public string? TrangThai { get; set; } = "Hoạt động";
        public int MaNhanVien { get; set; }
        // Tai khoan thuoc Nhan vien (1-1)
        [ForeignKey("MaNhanVien")]
        public NhanVien? NhanVien { get; set; }
    }
}
