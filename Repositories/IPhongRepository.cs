using ManagementHotel.DTOs.Phong;
namespace ManagementHotel.Repositories
{
    public interface IPhongRepository
    {
        // Lấy tất cả phòng
        Task<IEnumerable<PhongResponseDto>> GetAllPhongAsync();
    }
}
