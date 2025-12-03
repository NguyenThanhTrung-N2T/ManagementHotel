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
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public int TongTien { get; set; }
        public string? TrangThaiThanhToan { get; set; } = "Chưa thanh toán";

        // HoaDon thuoc DatPhong (1-1)
        [ForeignKey("MaDatPhong")]
        public DatPhong? DatPhong { get; set; }
        // HoaDon co nhieu ChiTietHoaDon (1-N)
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
    }
}
