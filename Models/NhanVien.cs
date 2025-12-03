using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ManagementHotel.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        public int MaNhanVien { get; set; }
        public string? HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string? CCCD { get; set; }
        public string? ChucVu { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public int Luong { get; set; }
        public string? DiaChi { get; set; }

        //Nhan vien co 1 tai khoan (1-1)
        public TaiKhoan? TaiKhoan { get; set; }
    }
}
