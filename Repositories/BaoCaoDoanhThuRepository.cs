using ManagementHotel.Data;
using ManagementHotel.Models;
using ManagementHotel.DTOs.BaoCaoDoanhThu;
using ManagementHotel.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
namespace ManagementHotel.Repositories
{
    public class BaoCaoDoanhThuRepository : IBaoCaoDoanhThuRepository
    {
        private readonly ManagementHotelContext _context;
        public BaoCaoDoanhThuRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BaoCaoDoanhThuResponseDto>> GetAllReportByMonthly()
        {
            var data = await _context.hoaDons
                .Where(x => x.TrangThaiThanhToan == "Đã thanh toán")
                .GroupBy(x => new { x.NgayLap.Year, x.NgayLap.Month })
                .Select(g => new BaoCaoDoanhThuResponseDto
                {
                    Thang = g.Key.Month,
                    Nam = g.Key.Year,
                    DoanhThu = g.Sum(x => x.TongTien)
                })
                .OrderBy(x => x.Nam).ThenBy(x => x.Thang)
                .ToListAsync();
            return data;

        }

    }
}
