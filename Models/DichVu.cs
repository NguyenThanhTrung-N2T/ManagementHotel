using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ManagementHotel.Models
{
    [Table("DichVu")]
    public class DichVu
    {
        [Key]
        public int MaDichVu { get; set; }
        public string? TenDichVu { get; set; }
        public string? DonVi { get; set; }
        public string? TrangThai { get; set; }
        public int DonGia { get; set; }

        // DichVu co nhieu ChiTietHoaDon (1-N)
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
    }
}
