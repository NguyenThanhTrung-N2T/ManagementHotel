using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ManagementHotel.Models
{
    [Table("BaoCaoDoanhThu")]
    public class BaoCaoDoanhThu
    {
        [Key]
        public int MaBaoCao { get; set; }
        public DateTime Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public int TongDoanhThu { get; set; }
        public string? GhiChu { get; set; }
    }
}
