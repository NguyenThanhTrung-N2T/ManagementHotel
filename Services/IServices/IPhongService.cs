using ManagementHotel.DTOs.Phong;
namespace ManagementHotel.Services.IServices
{
    public interface IPhongService
    {
        // Lấy tất cả phòng
        Task<IEnumerable<PhongResponseDto>> GetAllPhongAsync();

        // Lấy phòng theo mã phòng
        Task<PhongResponseDto> GetPhongByIdAsync(int maPhong);

        // Thêm phòng mới
        Task<PhongResponseDto> AddPhongAsync(CreatePhongRequestDto phong);

        // Cập nhật thông tin phòng
        Task<PhongResponseDto> UpdatePhongAsync(int maPhong, UpdatePhongRequestDto phong);

        // Xóa phòng
        Task<bool> DeletePhongAsync(int maPhong);

        // Lọc phòng theo trạng thái
        Task<IEnumerable<PhongResponseDto>> FilterPhongByStatusAsync(FilterPhongRequest filter);
    }
}
