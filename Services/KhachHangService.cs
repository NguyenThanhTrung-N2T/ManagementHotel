using ManagementHotel.DTOs.KhachHang;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services.IServices;
namespace ManagementHotel.Services
{
    public class KhachHangService : IKhachHangService
    {
        private readonly IKhachHangRepository _khachHangRepository;
        public KhachHangService(IKhachHangRepository khachHangRepository)
        {
            _khachHangRepository = khachHangRepository;
        }
        // lấy tất cả khách hàng
        public async Task<IEnumerable<KhachHangResponseDto>> GetAllKhachHangAsync()
        {
            return await _khachHangRepository.GetAllKhachHangAsync();
        }

        // lấy khách hàng theo mã khách hàng
        public async Task<KhachHangResponseDto?> GetKhachHangByIdAsync(int maKhachHang)
        {
            return await _khachHangRepository.GetKhachHangByIdAsync(maKhachHang);
        }

        // Thêm khách hàng mới
        public async Task<KhachHangResponseDto> AddKhachHangAsync(CreateKhachHangRequestDto khachhangnew)
        {
            try
            {
                // Kiểm tra sự tồn tại của khách hàng theo CCCD
                var existingKhachHang = await _khachHangRepository.IsKhachHangExistsByCCCDAsync(khachhangnew.CCCD);
                if (existingKhachHang)
                {
                    throw new Exception("Khách hàng với CCCD này đã tồn tại.");
                }
                // Thêm khách hàng mới
                return await _khachHangRepository.AddKhachHangAsync(khachhangnew);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                throw new Exception("Lỗi khi thêm khách hàng mới", ex);
            }
        }

        // Cập nhật khách hàng
        public async Task<KhachHangResponseDto> UpdateKhachHangAsync(int maKhachHang, UpdateKhachHangRequest khachHang)
        {
            try
            {
                // Lấy thông tin khách hàng hiện tại
                var existingKhachHang = await _khachHangRepository.GetKhachHangByIdAsync(maKhachHang);
                if (existingKhachHang == null)
                {
                    throw new Exception("Khách hàng không tồn tại.");
                }
                // Cập nhật thông tin khách hàng
                return await _khachHangRepository.UpdateKhachHangAsync(maKhachHang, khachHang);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                throw new Exception("Lỗi khi cập nhật khách hàng", ex);
            }
        }

        // Xóa khách hàng
        public async Task<bool> DeleteKhachHangAsync(int maKhachHang)
        {
            try
            {
                // Kiểm tra sự tồn tại của khách hàng
                var existingKhachHang = await _khachHangRepository.GetKhachHangByIdAsync(maKhachHang);
                if (existingKhachHang == null)
                {
                    throw new Exception("Khách hàng không tồn tại.");
                }
                // Xóa khách hàng
                return await _khachHangRepository.DeleteKhachHangAsync(maKhachHang);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                throw new Exception("Lỗi khi xóa khách hàng", ex);
            }
        }

        // Lọc khách hàng theo tên hoặc CCCD , số điện thoại
        public async Task<IEnumerable<KhachHangResponseDto>> FilterKhachHangAsync(FilterKhachHangRequestDto filter)
        {
            return await _khachHangRepository.FilterKhachHangAsync(filter);
        }
    }
}
