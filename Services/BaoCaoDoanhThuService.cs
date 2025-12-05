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

        // tạo báo cáo doanh thu 
        public async Task<BaoCaoDoanhThuResponseDto> CreateBaoCaoDoanhThuAsync()
        {
            try
            {
                return await _baoCaoDoanhThuRepository.CreateBaoCaoDoanhThuAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tạo báo cáo doanh thu: " + ex.Message);
            }
        }

        // lấy tất cả báo cáo doanh thu
        public async Task<IEnumerable<BaoCaoDoanhThuResponseDto>> GetAllBaoCaoDoanhThuAsync()
        {
            return await _baoCaoDoanhThuRepository.GetAllBaoCaoDoanhThuAsync();
        }

        // lọc báo cáo doanh thu theo tháng và năm
        public Task<BaoCaoDoanhThuResponseDto?> GetBaoCaoDoanhThuByMonthYearAsync(int month, int year)
        {
            return _baoCaoDoanhThuRepository.GetBaoCaoDoanhThuByThangNamAsync(month, year);
        }
    }
}
