using ManagementHotel.DTOs.DatPhong;

namespace ManagementHotel.Repositories.IRepositories
{
    public interface IDatPhongRepository
    {
        // Lấy danh sách đặt phòng
        Task<IEnumerable<DatPhongListResponseDto>> GetAllDatPhongsAsync();

        // lấy chi tiết đặt phòng theo mã đặt phòng
        Task<DatPhongResponseDto?> GetDatPhongByIdAsync(int maDatPhong);
    }
}
