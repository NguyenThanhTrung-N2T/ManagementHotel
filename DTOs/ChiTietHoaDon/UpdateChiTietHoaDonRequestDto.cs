using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs.ChiTietHoaDon
{
    public class UpdateChiTietHoaDonRequestDto
    {
        [Required(ErrorMessage = "Mã hóa đơn không được để trống")]
        public int MaHoaDon { get; set; }
        [Required(ErrorMessage = "Mã dịch vụ không được để trống")]
        public int MaDichVu { get; set; }
        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Đơn giá không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0")]
        public int DonGia { get; set; }
        public string? MoTa { get; set; }
    }
}
