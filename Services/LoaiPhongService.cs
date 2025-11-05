using ManagementHotel.DTOs;
using ManagementHotel.DTOs.LoaiPhong;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services.IServices;

namespace ManagementHotel.Services
{
    public class LoaiPhongService : ILoaiPhongService
    {
        private readonly ILoaiPhongRepository _loaiPhongRepository;
        public LoaiPhongService(ILoaiPhongRepository loaiPhongRepository)
        {
            _loaiPhongRepository = loaiPhongRepository;
        }

        // Lấy tất cả loại phòng
        public async Task<IEnumerable<LoaiPhongResponseDto>> GetAllLoaiPhongAsync()
        {
            return await _loaiPhongRepository.GetAllLoaiPhongAsync();
        }

        // Thêm loại phòng mới
        public async Task<LoaiPhongResponseDto> AddLoaiPhongAsync(CreateLoaiPhongRequestDto loaiPhong)
        {
            try
            {
                // Kiểm tra tên loại phòng đã tồn tại chưa
                var existLoaiPhong = await _loaiPhongRepository.IsLoaiPhongNameExistsAsync(loaiPhong.TenLoaiPhong);
                // Nếu tồn tại, ném ngoại lệ
                if (existLoaiPhong)
                {
                    throw new Exception("Tên loại phòng đã tồn tại.");
                }
                // Thêm loại phòng mới
                return await _loaiPhongRepository.AddLoaiPhongAsync(loaiPhong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm loại phòng: {ex.Message}");
            }
        }

        // Lấy thông tin loại phòng theo mã loại phòng
        public async Task<LoaiPhongResponseDto> GetLoaiPhongByIdAsync(int maLoaiPhong)
        {
            return await _loaiPhongRepository.GetLoaiPhongByIdAsync(maLoaiPhong);
        }

        // Cập nhật loại phòng
        public async Task<LoaiPhongResponseDto> UpdateLoaiPhongAsync(int maLoaiPhong, UpdateLoaiPhongRequestDto loaiPhong)
        {
            try
            {
                // Lấy thông tin loại phòng hiện tại
                var loaiphongUpdate = await _loaiPhongRepository.GetLoaiPhongByIdAsync(maLoaiPhong);
                // So sánh tên loại phòng mới với tên hiện tại
                if (loaiphongUpdate.TenLoaiPhong != loaiPhong.TenLoaiPhong)
                {
                    // Kiểm tra tên loại phòng đã tồn tại chưa
                    var existLoaiPhong = await _loaiPhongRepository.IsLoaiPhongNameExistsAsync(loaiPhong.TenLoaiPhong);
                    // Nếu tồn tại, ném ngoại lệ
                    if (existLoaiPhong)
                    {
                        throw new Exception("Tên loại phòng đã tồn tại.");
                    }
                }
                // Cập nhật loại phòng
                return await _loaiPhongRepository.UpdateLoaiPhongAsync(maLoaiPhong, loaiPhong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật loại phòng: {ex.Message}");
            }
        }

        // Xóa loại phòng
        public async Task<bool> DeleteLoaiPhongAsync(int maLoaiPhong)
        {
            try
            {
                // Xóa loại phòng
                return await _loaiPhongRepository.DeleteLoaiPhongAsync(maLoaiPhong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa loại phòng: {ex.Message}");
            }
        }

        // Lọc loại phòng theo khoảng giá
        public async Task<IEnumerable<LoaiPhongResponseDto>> FilterLoaiPhongByPriceAsync(FilterLoaiPhongRequest filter)
        {
            return await _loaiPhongRepository.FilterLoaiPhongByPriceAsync(filter);
        }
    }
}
