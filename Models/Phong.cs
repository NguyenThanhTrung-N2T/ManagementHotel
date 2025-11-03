using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ManagementHotel.Models
{
    [Table("Phong")]
    public class Phong
    {
        [Key]
        public int MaPhong { get; set; }
        public string? SoPhong { get; set; }
        public int MaLoaiPhong { get; set; }
        public string? TrangThai { get; set; } = "Trống";
        public string? GhiChu { get; set; }

        // Phong thuộc Loai Phong (N-1)
        [ForeignKey("MaLoaiPhong")]
        public virtual LoaiPhong LoaiPhong { get; set; } = null!;
        // Phong có nhiều Đặt Phòng (1-N)
        public virtual ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();
    }
}
