using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ManagementHotel.Models
{
    [Table("ChiTietHoaDon")]
    public class ChiTietHoaDon
    {
        [Key]
        public int MaChiTietHD { get; set; }
        public int MaHoaDon { get; set; }
        public int MaDichVu { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
        public string? Mota { get; set; }

        // ChiTietHoaDon thuoc HoaDon (N-1)
        [ForeignKey("MaHoaDon")]
        public virtual HoaDon HoaDon { get; set; } = null!;
        // ChiTietHoaDon thuoc DichVu (N-1)
        [ForeignKey("MaDichVu")]
        public virtual DichVu? DichVu { get; set; }
    }
}
