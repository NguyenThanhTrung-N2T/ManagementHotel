using ManagementHotel.DTOs.DichVu;

namespace ManagementHotel.Repositories.IRepositories
{
    public interface IDichVuRepository
    {
        // lấy danh sách tất cả dịch vụ
        Task<IEnumerable<DichVuResponseDto>> GetAllDichVuAsync();

        // lấy dịch vụ theo mã dịch vụ
        Task<DichVuResponseDto?> GetDichVuByIdAsync(int maDichVu);

        // thêm dịch vụ mới 
        Task<DichVuResponseDto> AddDichVuAsync(CreateDichVuRequestDto requestDto);

        // cập nhật thông tin dịch vu 
        Task<DichVuResponseDto> UpdateDichVuAsync(int maDichVu, UpdateDichVuRequestDto updateDto);

        // Xóa dịch vụ
        Task<bool> DeleteDichVuAsync(int maDichVu);

        // lọc dịch vụ theo tên , đơn giá , đơn vị 
        Task<IEnumerable<DichVuResponseDto>> FilterDichVuAsync(FilterDichVuRequestDto filterDto);

        // kiểm tra dịch vụ tồn tại theo tên dịch vụ 
        Task<bool> IsDichVuNameExistsAsync(string? tenDichVu);
    }
}
