using ManagementHotel.DTOs;
using ManagementHotel.DTOs.LoaiPhong;
using Microsoft.AspNetCore.Mvc;

namespace ManagementHotel.Repositories.IRepositories
{
    public interface ILoaiPhongRepository
    {
        // Lấy tất cả loại phòng
        Task<IEnumerable<LoaiPhongResponseDto>> GetAllLoaiPhongAsync();

        // Thêm loại phòng mới
        Task<LoaiPhongResponseDto> AddLoaiPhongAsync(CreateLoaiPhongRequestDto loaiPhong);

        // Lấy thông tin loại phòng theo mã loại phòng
        Task<LoaiPhongResponseDto> GetLoaiPhongByIdAsync(int maLoaiPhong);

        // Cập nhật loại phòng
        Task<LoaiPhongResponseDto> UpdateLoaiPhongAsync(int maLoaiPhong, UpdateLoaiPhongRequestDto loaiPhong);

        // Xóa loại phòng
        Task<bool> DeleteLoaiPhongAsync(int maLoaiPhong);

        // Lọc loại phòng theo khoảng giá
        Task<IEnumerable<LoaiPhongResponseDto>> FilterLoaiPhongByPriceAsync(FilterLoaiPhongRequest filter);

        // Kiểm tra tồn tại tên loại phòng
        Task<bool> IsLoaiPhongNameExistsAsync(string? tenLoaiPhong);

        // kiểm tra loại phòng có đang được sử dụng không
        Task<bool> IsLoaiPhongInUseAsync(int maLoaiPhong);
    }
}
