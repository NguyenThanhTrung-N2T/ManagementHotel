using ManagementHotel.DTOs.Phong;
using ManagementHotel.Repositories;
namespace ManagementHotel.Services
{
    public class PhongService : IPhongService
    {
        private readonly IPhongRepository _phongRepository;
        public PhongService(IPhongRepository phongRepository)
        {
            _phongRepository = phongRepository;
        }

        // Lấy tất cả phòng
        public async Task<IEnumerable<PhongResponseDto>> GetAllPhongAsync()
        {
            return await _phongRepository.GetAllPhongAsync();
        }
    }
}
