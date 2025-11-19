using ManagementHotel.DTOs.DichVu;
using ManagementHotel.DTOs.KhachHang;

namespace ManagementHotel.Services.IServices
{
    public interface IDichVuService
    {
        // lay tat ca dich vu
        Task<IEnumerable<DichVuResponseDto>> GetAllDichVuAsync();

        // lay dich vu theo id 
        Task<DichVuResponseDto?> GetDichVuByIdAsync(int maDichVu);

        // them dich vu 
        Task<DichVuResponseDto> AddDichVuAsync(CreateDichVuRequestDto requestDto);

        // Cập nhật thông tin dịch vụ
        Task<DichVuResponseDto> UpdateDichVuAsync(int maDichVu,UpdateDichVuRequestDto requestDto);

        // Xóa dich vu
        Task<bool> DeleteDichVuAsync(int maDichVu);

        // lọc dịch vụ theo tên , đon giá và đơn vị
        Task<IEnumerable<DichVuResponseDto>> FilterDichVuAsync(FilterDichVuRequestDto filterDto);
    }
}
