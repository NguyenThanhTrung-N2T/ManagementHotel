using ManagementHotel.DTOs.DatPhong;

namespace ManagementHotel.Repositories.IRepositories
{
    public interface IDatPhongRepository
    {
        // Lấy danh sách đặt phòng
        Task<IEnumerable<DatPhongListResponseDto>> GetAllDatPhongsAsync();

        // lấy chi tiết đặt phòng theo mã đặt phòng
        Task<DatPhongResponseDto?> GetDatPhongByIdAsync(int maDatPhong);

        // tạo đặt phòng mới
        Task<DatPhongResponseDto> CreateDatPhongAsync(CreateDatPhongRequestDto createDatPhongRequestDto);

        // kiểm tra phòng còn trống trong thời gian đặt hay không
        Task<bool> IsPhongAvailableAsync(int maPhong, DateTime ngayNhanPhong, DateTime ngayTraPhong);

        // cập nhật trạng thái đặt phòng
        Task<DatPhongResponseDto?> UpdateDatPhongStatusAsync(int maDatPhong, string trangThai);

    }
}
