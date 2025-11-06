using ManagementHotel.DTOs.KhachHang;
namespace ManagementHotel.Services.IServices
{
    public interface IKhachHangService
    {
        // lấy tất cả khách hàng
        Task<IEnumerable<KhachHangResponseDto>> GetAllKhachHangAsync();

        // lấy khách hàng theo mã khách hàng
        Task<KhachHangResponseDto?> GetKhachHangByIdAsync(int maKhachHang);

        // Thêm khách hàng mới
        Task<KhachHangResponseDto> AddKhachHangAsync(CreateKhachHangRequestDto khackhangnew);

        // Cập nhật thông tin khách hàng
        Task<KhachHangResponseDto> UpdateKhachHangAsync(int maKhachHang,UpdateKhachHangRequest khachHang);

        // Xóa khách hàng
        Task<bool> DeleteKhachHangAsync(int maKhachHang);

        // Lọc khách hàng theo tên hoặc CCCD , số điện thoại
        Task<IEnumerable<KhachHangResponseDto>> FilterKhachHangAsync(FilterKhachHangRequestDto filter);
    }
}
