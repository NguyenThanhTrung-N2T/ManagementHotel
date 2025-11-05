using ManagementHotel.DTOs.KhachHang;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services.IServices;
namespace ManagementHotel.Services
{
    public class KhachHangService : IKhachHangService
    {
        private readonly IKhachHangRepository _khachHangRepository;
        public KhachHangService(IKhachHangRepository khachHangRepository)
        {
            _khachHangRepository = khachHangRepository;
        }
        // lấy tất cả khách hàng
        public async Task<IEnumerable<KhachHangResponseDto>> GetAllKhachHangAsync()
        {
            return await _khachHangRepository.GetAllKhachHangAsync();
        }
    }
}
