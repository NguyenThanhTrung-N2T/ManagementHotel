using ManagementHotel.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ManagementHotel.Repositories
{
    public interface ILoaiPhongRepository
    {
        // Lấy tất cả loại phòng
        Task<IEnumerable<LoaiPhongResponseDto>> GetAllLoaiPhongAsync();

        // Thêm loại phòng mới
        Task<LoaiPhongResponseDto> AddLoaiPhongAsync(CreateLoaiPhongRequestDto loaiPhong);



    }
}
