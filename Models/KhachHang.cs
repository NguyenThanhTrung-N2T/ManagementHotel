using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ManagementHotel.Models
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        public int MaKhachHang { get; set; }
        public string? HoTen { get; set; }
        public string? SoDienThoai { get; set; }
        public string? CCCD { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }

        // Khach hang co nhieu dat phong (1-N)
        public virtual ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();
    }
}
