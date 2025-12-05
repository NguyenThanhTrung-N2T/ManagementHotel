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

        // kiểm tra tài khoản còn hoạt động
        Task<bool> IsTaiKhoanActive(string? tenDangNhap);

        // Lấy tài khoản theo mã tài khoản 
        Task<TaiKhoanResponseDto> GetTaiKhoanByIdAsync(int maTaiKhoan);

        // lấy tài khoản theo tên đăng nhập
        Task<TaiKhoanResponseDto> GetTaiKhoanByTenDangNhapAsync(string? tenDangNhap);

        // Xóa tài khoản 
        Task<bool> DeleteTaiKhoanAsync(int maTaiKhoan);

        // kiểm tài khoản theo tên đăng nhập
        Task<bool> IsExistTaiKhoan(string? tenDangNhap);

        // kiểm tra nhân viên đã có tài khoản chưa
        Task<bool> IsNhanVienHasTaiKhoan(int maNhanVien);

        // đăng nhập 
        Task<bool> LoginTaiKhoanAsync(LoginTaiKhoanRequestDto requestDto);

        // kiểm tra vai trò có khớp với chức vụ của nhân viên không
        Task<bool> IsVaiTroValidForNhanVien(string? vaiTro, int maNhanVien);

        // reset mật khẩu mới
        Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request);

    }
}
