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

        // tạo nhân viên mới
        Task<NhanVienResponseDto> AddNhanVienAsync(CreateNhanVienRequestDto nhanVienRequestDto);

        // kiểm tra tồn tại CCCD 
        Task<bool> IsExistCCCDAsync(string? CCCD);

        // kiểm tra tồn tại số điện thoai 
        Task<bool> IsExistSoDienThoaiAsync(string? soDienThoai);

        // cập nhật thông tin của nhân viên 
        Task<NhanVienResponseDto> UpdateNhanVienAsync(int maNhanVien, UpdateNhanVienRequestDto update_nhanvien);

        // xóa nhân viên qua mã nhân viên
        Task<bool> DeleteNhanVienAsync(int maNhanVien);

        // lọc nhân viên theo thuộc tính 
        Task<IEnumerable<NhanVienResponseDto>> FilterNhanVienAsync(FilterNhanVienRequestDto filter);

    }
}