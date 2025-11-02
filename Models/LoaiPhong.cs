using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ManagementHotel.Models
{
    [Table("LoaiPhong")]
    public class LoaiPhong
    {
        [Key]
        public int MaLoaiPhong { get; set; }
        public string? TenLoaiPhong { get; set; }
        public string? MoTa { get; set; }
        public int GiaTheoDem { get; set; }

        // Loai Phong có nhiều Phong (1-N)
        public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();
    }
}
