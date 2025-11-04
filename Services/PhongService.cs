using ManagementHotel.DTOs.Phong;
using ManagementHotel.Repositories;
namespace ManagementHotel.Services
{
    public class PhongService : IPhongService
    {
        private readonly IPhongRepository _phongRepository;
        public PhongService(IPhongRepository phongRepository)
        {
            _phongRepository = phongRepository;
        }

        // Lấy tất cả phòng
        public async Task<IEnumerable<PhongResponseDto>> GetAllPhongAsync()
        {
            return await _phongRepository.GetAllPhongAsync();
        }

        // Lấy phòng theo mã phòng
        public async Task<PhongResponseDto> GetPhongByIdAsync(int maPhong)
        {
            return await _phongRepository.GetPhongByIdAsync(maPhong);
        }

        // Thêm phòng mới
        public async Task<PhongResponseDto> AddPhongAsync(CreatePhongRequestDto phong)
        {
            try
            {
                // Kiểm tra sự tồn tại của số phòng
                var existingPhong = await _phongRepository.IsPhongNumberExistsAsync(phong.SoPhong);
                // Nếu số phòng đã tồn tại, ném ngoại lệ
                if (existingPhong)
                {
                    throw new Exception("Số phòng đã tồn tại.");
                }
                // Thêm phòng mới
                return await _phongRepository.AddPhongAsync(phong);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi kiểm tra số phòng: " + ex.Message);
            }
        }

        // Cập nhật thông tin phòng
        public async Task<PhongResponseDto> UpdatePhongAsync(int maPhong, UpdatePhongRequestDto phong)
        {
            try
            {
                // Lấy thông tin phòng hiện tại
                var phongUpdate = await _phongRepository.GetPhongByIdAsync(maPhong);
                // Nếu số phòng được cập nhật, kiểm tra sự tồn tại của số phòng mới
                if (phongUpdate.SoPhong != phong.SoPhong)
                {
                    // Kiểm tra sự tồn tại của số phòng
                    var existingPhong = await _phongRepository.IsPhongNumberExistsAsync(phong.SoPhong);
                    // Nếu số phòng đã tồn tại, ném ngoại lệ
                    if (existingPhong)
                    {
                        throw new Exception("Số phòng đã tồn tại.");
                    }
                }
                // Cập nhật thông tin phòng
                return await _phongRepository.UpdatePhongAsync(maPhong, phong);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật phòng: " + ex.Message);
            }
        }
    }
}
