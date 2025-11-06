using ManagementHotel.DTOs.NhanVien;
using ManagementHotel.Models;

namespace ManagementHotel.Repositories.IRepositories
{
    public interface INhanVienRepository
    {
        // Lấy tất cả nhân viên
        Task<IEnumerable<NhanVienResponseDto>> GetAllNhanVienAsync();

        // lấy nhân viên qua mã nhân viên
        Task<NhanVienResponseDto> GetNhanVienByIdAsync(int maNhanVien);
    }
}