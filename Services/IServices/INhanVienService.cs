using ManagementHotel.DTOs.NhanVien;
namespace ManagementHotel.Services.IServices
{
    public interface INhanVienService
    {
        // lấy tất cả nhân viên
        Task<IEnumerable<NhanVienResponseDto>> GetAllNhanVienAsync();

        // lấy nhân viên qua mã nhân viên
        Task<NhanVienResponseDto> GetNhanVienByIdAsync(int maNhanVien);
    }
}