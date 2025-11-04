using ManagementHotel.DTOs.Phong;
namespace ManagementHotel.Services
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
    }
}
