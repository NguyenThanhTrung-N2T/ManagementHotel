using ManagementHotel.DTOs.Phong;
namespace ManagementHotel.Repositories
{
    public interface IPhongRepository
    {
        // Lấy tất cả phòng
        Task<IEnumerable<PhongResponseDto>> GetAllPhongAsync();

        // Lấy phòng theo mã phòng
        Task<PhongResponseDto> GetPhongByIdAsync(int maPhong);

        // Thêm phòng mới
        Task<PhongResponseDto> AddPhongAsync(CreatePhongRequestDto phong);

        // Kiểm tra sự tồn tại của phòng theo số phòng
        Task<bool> IsPhongNumberExistsAsync(string? soPhong);

        // Cập nhật thông tin phòng
        Task<PhongResponseDto> UpdatePhongAsync(int maPhong, UpdatePhongRequestDto phong);
    }
}
