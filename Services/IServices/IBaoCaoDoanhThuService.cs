using ManagementHotel.DTOs.BaoCaoDoanhThu;

namespace ManagementHotel.Services.IServices
{
    public interface IBaoCaoDoanhThuService
    {
        // bao cao doanh thu 
        Task<IEnumerable<BaoCaoDoanhThuResponseDto>> GetAllReportByMonthly();
    }
}
