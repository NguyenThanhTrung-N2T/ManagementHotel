using ManagementHotel.DTOs.BaoCaoDoanhThu;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
namespace ManagementHotel.Repositories.IRepositories
{
    public interface IBaoCaoDoanhThuRepository
    {
        // bao cao doanh thu theo thang va nam
        Task<IEnumerable<BaoCaoDoanhThuResponseDto>> GetAllReportByMonthly();
    }
}
