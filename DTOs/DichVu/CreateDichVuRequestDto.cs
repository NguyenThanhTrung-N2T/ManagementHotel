using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs.DichVu
{
    public class CreateDichVuRequestDto
    {
        [Required(ErrorMessage ="Tên dịch vụ không được để trống !")]
        [StringLength(100)]
        public string? TenDichVu {  get; set; }

        [Required(ErrorMessage = "Đơn vị không được để trống !")]
        [StringLength(50)]
        public  string? DonVi { get; set; }

        [Required(ErrorMessage ="Đơn giá không được để trống !")]
        [Range(0,int.MaxValue,ErrorMessage ="Đơn giá không thể nhỏ hơn 0 !")]
        public int DonGia { get; set; }
    }
}
