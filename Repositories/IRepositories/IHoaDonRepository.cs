using ManagementHotel.DTOs.HoaDon;

namespace ManagementHotel.Repositories.IRepositories
{
    public interface IHoaDonRepository
    {
        // tạo hóa đơn từ mã đặt phòng
        Task<HoaDonResponseDto> CreateHoaDonAsync(int maDatPhong);
    }
}
