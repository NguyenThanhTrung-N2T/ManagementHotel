using ManagementHotel.DTOs.Phong;
namespace ManagementHotel.Services
{
    public interface IPhongService
    {
        // Lấy tất cả phòng
        Task<IEnumerable<PhongResponseDto>> GetAllPhongAsync();
    }
}
