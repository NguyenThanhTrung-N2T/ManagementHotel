using ManagementHotel.DTOs.ChiTietHoaDon;
namespace ManagementHotel.Repositories.IRepositories
{
    public interface IChiTietHoaDonRepository
    {
        // thêm dịch vụ vào chi tiết hóa đơn
        Task<ChiTietHoaDonResponseDto> AddDichVuToChiTietHoaDonAsync(CreateChiTietHoaDonRequestDto createDto);

        // xóa chi tiết hóa đơn theo mã chi tiết hóa đơn
        Task<bool> DeleteChiTietHoaDonAsync(int maChiTietHD);

        // cập nhật chi tiết hóa đơn theo mã chi tiết hóa đơn
        Task<ChiTietHoaDonResponseDto> UpdateChiTietHoaDonAsync(int maChiTietHD, UpdateChiTietHoaDonRequestDto updateDto);
    }
}
