using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs.LoaiPhong
{
    public class FilterLoaiPhongRequest
    {
        [Range(0, int.MaxValue, ErrorMessage = "Giá phải là số nguyên không âm.")]
        public int? GiaMin { get; set; } = null;
        [Range(0, int.MaxValue, ErrorMessage = "Giá phải là số nguyên không âm.")]
        public int? GiaMax { get; set; } = null;
        public string? TenLoaiPhong { get; set; }
        public string? SapXepTheo { get; set; }
        public string? ThuTu { get; set; } = "ASC";
    }
}
