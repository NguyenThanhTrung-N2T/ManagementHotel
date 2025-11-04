using System.ComponentModel.DataAnnotations;
namespace ManagementHotel.DTOs.Phong
{
    public class CreatePhongRequestDto
    {
        [Required(ErrorMessage = "Số phòng không được để trống !")]
        public string? SoPhong { get; set; }
        [Required(ErrorMessage = "Mã loại phòng không được để trống !")]
        public int MaLoaiPhong { get; set; }
        public string? TrangThai { get; set; } = "Trống";
        public string? GhiChu { get; set; }
    }
}
