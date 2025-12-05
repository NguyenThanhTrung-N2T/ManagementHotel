using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services.IServices;

namespace ManagementHotel.Services
{
    public class ChiTietHoaDonService : IChiTietHoaDonService
    {
        private readonly IChiTietHoaDonRepository _chiTietHoaDonRepository;
        public ChiTietHoaDonService(IChiTietHoaDonRepository chiTietHoaDonRepository)
        {
            _chiTietHoaDonRepository = chiTietHoaDonRepository;
        }


        // thêm chi tiết hóa đơn cho hóa đơn
        public async Task<DTOs.ChiTietHoaDon.ChiTietHoaDonResponseDto> AddDichVuToChiTietHoaDonAsync(DTOs.ChiTietHoaDon.CreateChiTietHoaDonRequestDto createDto)
        {
            try
            {
                var result = await _chiTietHoaDonRepository.AddDichVuToChiTietHoaDonAsync(createDto);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm dịch vụ vào chi tiết hóa đơn: " + ex.Message);
            }
        }

        // xóa chi tiết hóa đơn theo mã chi tiết hóa đơn
        public async Task<bool> DeleteChiTietHoaDonAsync(int maChiTietHD)
        {
            try
            {
                var result = await _chiTietHoaDonRepository.DeleteChiTietHoaDonAsync(maChiTietHD);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chi tiết hóa đơn: " + ex.Message);
            }
        }

        // cập nhật chi tiết hóa đơn theo mã chi tiết hóa đơn
        public async Task<DTOs.ChiTietHoaDon.ChiTietHoaDonResponseDto> UpdateChiTietHoaDonAsync(int maChiTietHD, DTOs.ChiTietHoaDon.UpdateChiTietHoaDonRequestDto updateDto)
        {
            try
            {
                var result = await _chiTietHoaDonRepository.UpdateChiTietHoaDonAsync(maChiTietHD, updateDto);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật chi tiết hóa đơn: " + ex.Message);
            }
        }
    }
}
