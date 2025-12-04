using System.ComponentModel.DataAnnotations;
namespace ManagementHotel.DTOs
{
    public class CreateLoaiPhongRequestDto
    {
        [Required(ErrorMessage = "Tên loại phòng không được để trống !")]
        [MaxLength(100)]
        public string? TenLoaiPhong { get; set; }
        public string? MoTa { get; set; }
        [Required(ErrorMessage ="Trạng thái phòng không được để trống !")]
        public string? TrangThai { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Giá theo ngày phải là số không âm !")]
        public int GiaTheoDem { get; set; }
    }
}
