using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs.Phong
{
    public class FilterPhongRequest
    {
        [MaxLength(15)]
        public string? TrangThai { get; set; }
        public int? MaLoaiPhong { get; set; }
    }
}
