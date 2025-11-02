using ManagementHotel.DTOs;
using ManagementHotel.Repositories;

namespace ManagementHotel.Services
{
    public class LoaiPhongService : ILoaiPhongService
    {
        private readonly ILoaiPhongRepository _loaiPhongRepository;
        public LoaiPhongService(ILoaiPhongRepository loaiPhongRepository)
        {
            _loaiPhongRepository = loaiPhongRepository;
        }

        // Lấy tất cả loại phòng
        public async Task<IEnumerable<LoaiPhongResponseDto>> GetAllLoaiPhongAsync()
        {
            return await _loaiPhongRepository.GetAllLoaiPhongAsync();
        }

        // Thêm loại phòng mới
        public async Task<LoaiPhongResponseDto> AddLoaiPhongAsync(CreateLoaiPhongRequestDto loaiPhong)
        {
            return await _loaiPhongRepository.AddLoaiPhongAsync(loaiPhong);
        }


    }
}
