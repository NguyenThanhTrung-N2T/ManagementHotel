using ManagementHotel.DTOs.HoaDon;
namespace ManagementHotel.Services.IServices
{
    public interface IHoaDonService
    {
        // lọc học đơn theo trạng thái thanh toán
        Task<IEnumerable<HoaDonResponseDto>> FilterHoaDonByStatusAsync(string trangThai);

        // lấy danh sách hóa đơn
        Task<IEnumerable<HoaDonResponseDto>> GetAllHoaDonsAsync();

        // lấy hóa đơn theo mã hóa đơn
        Task<HoaDonDetailResponseDto?> GetHoaDonDetailByIdAsync(int maHoaDon);
    }
}
