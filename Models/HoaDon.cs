using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ManagementHotel.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }
        public int MaDatPhong { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public int TongTien { get; set; }

        // HoaDon thuoc DatPhong (1-1)
        public DatPhong? DatPhong { get; set; }
        public virtual NhanVien NhanVien { get; set; } = null!;
        // HoaDon co nhieu ChiTietHoaDon (1-N)
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
    }
}
