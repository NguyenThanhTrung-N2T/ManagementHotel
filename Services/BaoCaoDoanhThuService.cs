using ManagementHotel.DTOs.BaoCaoDoanhThu;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services.IServices;
namespace ManagementHotel.Services
{
    public class BaoCaoDoanhThuService : IBaoCaoDoanhThuService
    {
        private readonly IBaoCaoDoanhThuRepository _baoCaoDoanhThuRepository;
        public BaoCaoDoanhThuService(IBaoCaoDoanhThuRepository baoCaoDoanhThuRepository)
        {
            _baoCaoDoanhThuRepository = baoCaoDoanhThuRepository;
        }

        public async Task<IEnumerable<BaoCaoDoanhThuResponseDto>> GetAllReportByMonthly()
        {
            return await _baoCaoDoanhThuRepository.GetAllReportByMonthly();
        }
    }
}
