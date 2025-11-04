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

    }
}
