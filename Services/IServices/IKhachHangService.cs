using ManagementHotel.DTOs.KhachHang;
namespace ManagementHotel.Services.IServices
{
    public interface IKhachHangService
    {
        // lấy tất cả khách hàng
        Task<IEnumerable<KhachHangResponseDto>> GetAllKhachHangAsync();
    }
}
