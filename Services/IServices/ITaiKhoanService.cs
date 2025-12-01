using ManagementHotel.DTOs.TaiKhoan;

namespace ManagementHotel.Services.IServices
{
    public interface ITaiKhoanService
    {
        // lấy tất cả tài khoản 
        Task<IEnumerable<TaiKhoanResponseDto>> GetAllTaiKhoanAsync();

        // Thêm tài khoản mới 
        Task<TaiKhoanResponseDto> AddTaiKhoanAsync(CreateTaiKhoanRequestDto taiKhoanNew);

        // lấy tài khoản theo mã tài  khoản 
        Task<TaiKhoanResponseDto> GetTaiKhoanByIdAsync(int maTaiKhoan);

        // lấy tài khoản theo tên đăng nhập
        Task<TaiKhoanResponseDto> GetTaiKhoanByTenDangNhapAsync(string tenDangNhap);

        // Xóa tài khoản 
        Task<bool> DeleteTaiKhoanAsync(int maTaiKhoan);

        // đăng nhập tài khoản 
        Task<bool> LoginTaiKhoanAsync(LoginTaiKhoanRequestDto loginTaiKhoanRequestDto);
    }
}
