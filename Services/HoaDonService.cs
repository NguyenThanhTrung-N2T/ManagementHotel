using ManagementHotel.Services.IServices;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.DTOs.HoaDon;
namespace ManagementHotel.Services
{
    public class HoaDonService : IHoaDonService
    {
        private readonly IHoaDonRepository _hoaDonRepository;
        public HoaDonService(IHoaDonRepository hoaDonRepository)
        {
            _hoaDonRepository = hoaDonRepository;
        }
        // lọc hóa đơn theo trạng thái thanh toán
        public async Task<IEnumerable<HoaDonResponseDto>> FilterHoaDonByStatusAsync(string trangThai)
        {
            return await _hoaDonRepository.FilterHoaDonByStatusAsync(trangThai);
        }

        // lấy danh sách hóa đơn
        public async Task<IEnumerable<HoaDonResponseDto>> GetAllHoaDonsAsync()
        {
            return await _hoaDonRepository.GetAllHoaDonsAsync();
        }
    }
}
