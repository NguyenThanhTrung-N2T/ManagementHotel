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
    }
}
