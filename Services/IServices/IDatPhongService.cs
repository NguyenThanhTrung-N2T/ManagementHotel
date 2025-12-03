using ManagementHotel.DTOs.DatPhong;
namespace ManagementHotel.Services.IServices
{
    public interface IDatPhongService
    {
        // lấy tất cả đặt phòng
        Task<IEnumerable<DatPhongListResponseDto>> GetAllDatPhongsAsync();

        // lấy chi tiết đặt phòng theo mã đặt phòng
        Task<DatPhongResponseDto?> GetDatPhongByIdAsync(int maDatPhong);

        // tạo đặt phòng mới 
        Task<DatPhongResponseDto> CreateDatPhongAsync(CreateDatPhongRequestDto createDatPhongRequestDto);

        // cập nhật trạng thái đặt phòng
        Task<DatPhongResponseDto?> UpdateDatPhongStatusAsync(int maDatPhong, string trangThai);

    }
}
