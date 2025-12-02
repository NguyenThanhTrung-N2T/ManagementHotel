using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services.IServices;

using ManagementHotel.DTOs.DatPhong;
namespace ManagementHotel.Services
{
    public class DatPhongService : IDatPhongService
    {
        private readonly IDatPhongRepository _datPhongRepository;
        public DatPhongService(IDatPhongRepository datPhongRepository)
        {
            _datPhongRepository = datPhongRepository;
        }

        // lấy tất cả đặt phòng
        public async Task<IEnumerable<DatPhongListResponseDto>> GetAllDatPhongsAsync()
        {
            return await _datPhongRepository.GetAllDatPhongsAsync();
        }

        // lấy chi tiết đặt phòng theo mã đặt phòng
        public async Task<DatPhongResponseDto?> GetDatPhongByIdAsync(int maDatPhong)
        {
            return await _datPhongRepository.GetDatPhongByIdAsync(maDatPhong);
        }
    }
}
