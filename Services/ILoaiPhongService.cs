using ManagementHotel.DTOs;

namespace ManagementHotel.Services
{
    public interface ILoaiPhongService
    {
        // Lấy tất cả loại phòng
        Task<IEnumerable<LoaiPhongResponseDto>> GetAllLoaiPhongAsync();
        // Thêm loại phòng mới
        Task<LoaiPhongResponseDto> AddLoaiPhongAsync(CreateLoaiPhongRequestDto loaiPhong);
    }
}
