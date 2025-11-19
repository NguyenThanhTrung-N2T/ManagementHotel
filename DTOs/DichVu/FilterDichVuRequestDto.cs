using System.ComponentModel.DataAnnotations;

namespace ManagementHotel.DTOs.DichVu
{
    public class FilterDichVuRequestDto
    {
        [MaxLength(100,ErrorMessage ="Tên dịch vụ không quá 100 ký tự !")]
        public string? TenDichVu { get; set; }

        [MaxLength(10)]
        public string? DonVi {  get; set; }

        [Range(0,int.MaxValue)]
        public int? DonGia { get; set; }
    }
}
