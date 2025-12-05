using ManagementHotel.DTOs.BaoCaoDoanhThu;
namespace ManagementHotel.Repositories.IRepositories
{
    public interface IBaoCaoDoanhThuRepository
    {
        // tạo báo cáo doanh thu theo tháng và năm
        Task<BaoCaoDoanhThuResponseDto> CreateBaoCaoDoanhThuAsync();
        // lấy báo cáo doanh thu theo tháng và năm
        Task<BaoCaoDoanhThuResponseDto?> GetBaoCaoDoanhThuByThangNamAsync(int thang, int nam);
        //// lấy tất cả báo cáo doanh thu
        Task<IEnumerable<BaoCaoDoanhThuResponseDto>> GetAllBaoCaoDoanhThuAsync();
    }
}
