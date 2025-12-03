using ManagementHotel.DTOs.HoaDon;

namespace ManagementHotel.Repositories.IRepositories
{
    public interface IHoaDonRepository
    {
        // tạo hóa đơn từ mã đặt phòng
        Task<HoaDonResponseDto> CreateHoaDonAsync(int maDatPhong);

        // tính tổng tiền bao gồm tiền phòng và các dịch vụ khác
        Task<int> TinhTongTien(int maDatPhong, DateTime ngayTraPhong);

        // cập nhật tổng tiền mới vào hóa đơn
        Task<HoaDonResponseDto> UpdateTongTienInHoaDon(int tongTien, int maHoaDon);
    }
}
