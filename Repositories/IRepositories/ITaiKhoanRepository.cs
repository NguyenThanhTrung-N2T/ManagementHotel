using ManagementHotel.DTOs.TaiKhoan;
using Microsoft.AspNetCore.Identity.Data;
using System.Diagnostics.Eventing.Reader;

namespace ManagementHotel.Repositories.IRepositories
{
    public interface ITaiKhoanRepository
    {
        // lấy tất cả tài khoản 
        Task<IEnumerable<TaiKhoanResponseDto>> GetAllTaiKhoanAsync();

        // Thêm tài khoản mới 
        Task<TaiKhoanResponseDto> AddTaiKhoanAsync(CreateTaiKhoanRequestDto taiKhoanRequestDto);

        // Kiểm tra tên đang nhập tồn tại 
        Task<bool> IsTenDangNhapExist(string? tenDangNhap);

        // Lấy tài khoản theo mã tài khoản 
        Task<TaiKhoanResponseDto> GetTaiKhoanByIdAsync(int maTaiKhoan);

        // Xóa tài khoản 
        Task<bool> DeleteTaiKhoanAsync(int maTaiKhoan);

        // kiểm tài khoản theo tên đăng nhập
        Task<bool> IsExistTaiKhoan(string? tenDangNhap);

        // đăng nhập 
        Task<bool> LoginTaiKhoanAsync(LoginTaiKhoanRequestDto requestDto);
    }
}
