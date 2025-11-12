using ManagementHotel.DTOs.NhanVien;
using System.Runtime.CompilerServices;
namespace ManagementHotel.Services.IServices
{
    public interface INhanVienService
    {
        // lấy tất cả nhân viên
        Task<IEnumerable<NhanVienResponseDto>> GetAllNhanVienAsync();

        // lấy nhân viên qua mã nhân viên
        Task<NhanVienResponseDto> GetNhanVienByIdAsync(int maNhanVien);

        // Tạo nhân viên mới 
        Task<NhanVienResponseDto> AddNhanVienAsync(CreateNhanVienRequestDto nhanVienRequestDto);

        // Cập nhật thông tin 
        Task<NhanVienResponseDto> UpdateNhanVienAsync(int maNhanVien, UpdateNhanVienRequestDto nhanVienRequestDto);

        // Xóa nhân viên qua mã nhân viên
        Task<bool> DeleteNhanVienAsync(int maNhanVien);

        // lọc nhân viên theo các thuộc tính 
        Task<IEnumerable<NhanVienResponseDto>> FilterNhanVienAsync(FilterNhanVienRequestDto filter);
    }
}