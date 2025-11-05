using ManagementHotel.DTOs.KhachHang;
namespace ManagementHotel.Repositories.IRepositories
{
    public interface IKhachHangRepository
    {
        // lấy tất cả khách hàng
        Task<IEnumerable<KhachHangResponseDto>> GetAllKhachHangAsync();
    }
}
