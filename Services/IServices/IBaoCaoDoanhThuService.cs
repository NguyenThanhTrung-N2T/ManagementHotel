using ManagementHotel.DTOs.BaoCaoDoanhThu;

namespace ManagementHotel.Services.IServices
{
    public interface IBaoCaoDoanhThuService
    {
        // tạo báo cáo doanh thu
        Task<BaoCaoDoanhThuResponseDto> CreateBaoCaoDoanhThuAsync();
        // lấy báo cáo doanh thu theo tháng và năm
        Task<BaoCaoDoanhThuResponseDto?> GetBaoCaoDoanhThuByMonthYearAsync(int month, int year);
        // lấy tất cả báo cáo doanh thu
        Task<IEnumerable<BaoCaoDoanhThuResponseDto>> GetAllBaoCaoDoanhThuAsync();
    }
}
