using ManagementHotel.DTOs;
using ManagementHotel.DTOs.LoaiPhong;
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

        // Lấy thông tin loại phòng theo mã loại phòng
        public async Task<LoaiPhongResponseDto> GetLoaiPhongByIdAsync(int maLoaiPhong)
        {
            return await _loaiPhongRepository.GetLoaiPhongByIdAsync(maLoaiPhong);
        }

        // Cập nhật loại phòng
        public async Task<LoaiPhongResponseDto> UpdateLoaiPhongAsync(int maLoaiPhong, UpdateLoaiPhongRequestDto loaiPhong)
        {
            return await _loaiPhongRepository.UpdateLoaiPhongAsync(maLoaiPhong, loaiPhong);
        }

        // Xóa loại phòng
        public async Task<bool> DeleteLoaiPhongAsync(int maLoaiPhong)
        {
            return await _loaiPhongRepository.DeleteLoaiPhongAsync(maLoaiPhong);
        }

        // Lọc loại phòng theo khoảng giá
        public async Task<IEnumerable<LoaiPhongResponseDto>> FilterLoaiPhongByPriceAsync(FilterLoaiPhongRequest filter)
        {
            return await _loaiPhongRepository.FilterLoaiPhongByPriceAsync(filter);
        }
    }
}
