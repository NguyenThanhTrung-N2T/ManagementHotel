using ManagementHotel.DTOs.DichVu;
using ManagementHotel.DTOs.KhachHang;
using ManagementHotel.Repositories;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services.IServices;

namespace ManagementHotel.Services
{
    public class DichVuService : IDichVuService
    {
        private readonly IDichVuRepository _dichVuRepository;
        public DichVuService(IDichVuRepository dichVuRepository)
        {
            _dichVuRepository = dichVuRepository;
        }

        // lay tat ca dich vu
        public async Task<IEnumerable<DichVuResponseDto>> GetAllDichVuAsync()
        {
            return await _dichVuRepository.GetAllDichVuAsync();
        }

        // lay dich vu theo ma dich vu
        public async Task<DichVuResponseDto?> GetDichVuByIdAsync(int maDichVu)
        {
            return await _dichVuRepository.GetDichVuByIdAsync(maDichVu);
        }

        // them dich vu
        public async Task<DichVuResponseDto> AddDichVuAsync(CreateDichVuRequestDto requestDto)
        {
            try
            {
                // kiem tra ten dich vu da ton tai chua
                var existingDichVu = await _dichVuRepository.IsDichVuNameExistsAsync(requestDto.TenDichVu);
                if (existingDichVu)
                {
                    throw new Exception("Tên dịch vụ đã tồn tại.");
                }
                // them dich vu moi vao db
                return await _dichVuRepository.AddDichVuAsync(requestDto);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm dịch vụ mới", ex);
            }
        }

        // Cập nhật dịch vụ
        public async Task<DichVuResponseDto> UpdateDichVuAsync(int maDichVu, UpdateDichVuRequestDto requestDto)
        {
            try
            {
                // Lấy thông tin hiện tại
                var existingDichVu = await _dichVuRepository.GetDichVuByIdAsync(maDichVu);
                if(existingDichVu!= null && existingDichVu.TenDichVu != requestDto.TenDichVu)
                {
                    // Kiểm tra tên dịch vụ đã tồn tại hay chưa
                    var isNameExists = await _dichVuRepository.IsDichVuNameExistsAsync(requestDto.TenDichVu);
                    if (isNameExists)
                    {
                        throw new Exception("Tên dịch vụ đã tồn tại.");
                    }
                }
                // Cập nhật thông tin dich vu
                return await _dichVuRepository.UpdateDichVuAsync(maDichVu, requestDto);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                throw new Exception("Lỗi khi cập nhật dịch vụ", ex);
            }
        }


        // Xóa dich vu
        public async Task<bool> DeleteDichVuAsync(int maDichVu)
        {
            try
            {
                // Kiểm tra sự tồn tại của dịch vụ
                var existingDichVu = await _dichVuRepository.GetDichVuByIdAsync(maDichVu);
                if (existingDichVu == null)
                {
                    throw new Exception("Dịch vụ không tồn tại.");
                }
                // Xóa dịch vụ
                return await _dichVuRepository.DeleteDichVuAsync(maDichVu);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                throw new Exception("Lỗi khi xóa dịch vụ", ex);
            }
        }

        // lọc dịch vụ theo tên , đơn vị và đơn giá 
        public async Task<IEnumerable<DichVuResponseDto>> FilterDichVuAsync(FilterDichVuRequestDto filterDto)
        {
            return await _dichVuRepository.FilterDichVuAsync(filterDto);
        }
    }
}
