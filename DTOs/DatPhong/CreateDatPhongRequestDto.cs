using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs.DatPhong
{
    public class CreateDatPhongRequestDto
    {
        [Required(ErrorMessage = "Mã khách hàng không được để trống !")]
        public int MaKhachHang { get; set; }
        [Required(ErrorMessage = "Mã phòng không được để trống !")]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Ngày nhận phòng không được để trống !")]
        public DateTime NgayNhanPhong { get; set; }
        [Required(ErrorMessage = "Ngày trả phòng không được để trống !")]
        public DateTime NgayTraPhong { get; set; }

        // "Đã đặt" | "Đang ở"
        [Required(ErrorMessage = "Trạng thái không được để trống !")]
        public string TrangThai { get; set; } = "Đã đặt";

        public string? GhiChu { get; set; }
    }
}
