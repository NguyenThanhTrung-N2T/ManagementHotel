using ManagementHotel.DTOs.NhanVien;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services.IServices;

namespace ManagementHotel.Services
{
    public class NhanVienService : INhanVienService
    {
        private readonly INhanVienRepository _nhanVienRepository;
        public NhanVienService(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }

        // lấy tất cả nhân viên
        public async Task<IEnumerable<NhanVienResponseDto>> GetAllNhanVienAsync()
        {
            return await _nhanVienRepository.GetAllNhanVienAsync();
        }


        // lấy nhân viên theo mã nhân viên
        public async Task<NhanVienResponseDto> GetNhanVienByIdAsync(int maNhanVien)
        {
            return await _nhanVienRepository.GetNhanVienByIdAsync(maNhanVien);
        }

        //Thêm nhân viên mới 
        public async Task<NhanVienResponseDto> AddNhanVienAsync(CreateNhanVienRequestDto nhanVienRequestDto)
        {
            try
            {
                // kiểm tra trùng CCCD của nhân viên
                var existCCCD = await _nhanVienRepository.IsExistCCCDAsync(nhanVienRequestDto.CCCD);
                if (existCCCD)
                {
                    throw new Exception("CCCD của nhân viên đã tồn tại !");
                }

                // kiểm tra trùng số điện thoại của nhân viên
                var exitsSDT = await _nhanVienRepository.IsExistSoDienThoaiAsync(nhanVienRequestDto.SoDienThoai);
                if (exitsSDT)
                {
                    throw new Exception("Số điện thoại của nhân viên đã tồn tại !");
                }
                
                // thêm nhân viên mới
                return await _nhanVienRepository.AddNhanVienAsync(nhanVienRequestDto);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm nhân viên !", ex);
            }
        }

        // Cập nhật thông tin nhân viên
        public async Task<NhanVienResponseDto> UpdateNhanVienAsync(int maNhanVien, UpdateNhanVienRequestDto nhanVienRequestDto)
        {
            try
            {
                // lấy nhân viên hiện tại
                var nhanvien_exist = await _nhanVienRepository.GetNhanVienByIdAsync(maNhanVien);
                // kiểm tra trùng số điện thoại
                if (nhanVienRequestDto.SoDienThoai != nhanvien_exist.SoDienThoai)
                {
                    var isExistSDT = await _nhanVienRepository.IsExistSoDienThoaiAsync(nhanVienRequestDto.SoDienThoai);
                    if (isExistSDT)
                    {
                        throw new Exception("Số điện thoại của nhân viên đã tồn tại !");
                    }
                }

                // cập nhật nhân viên 
                return await _nhanVienRepository.UpdateNhanVienAsync(maNhanVien, nhanVienRequestDto);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật nhân viên !", ex);
            }
        }

        // Xóa nhân viên qua mã nhân viên 
        public async Task<bool> DeleteNhanVienAsync(int maNhanVien)
        {
            try
            {
                // Xóa nhân viên
                return await _nhanVienRepository.DeleteNhanVienAsync(maNhanVien);
            }
            catch (Exception ex) {
                throw new Exception("Lỗi khi xóa phòng: " + ex.Message);
            }

        }

        // lọc nhân viên theo thuộc tính 
        public async Task<IEnumerable<NhanVienResponseDto>> FilterNhanVienAsync(FilterNhanVienRequestDto filter)
        {
            return await _nhanVienRepository.FilterNhanVienAsync(filter);
        }
    }
}
