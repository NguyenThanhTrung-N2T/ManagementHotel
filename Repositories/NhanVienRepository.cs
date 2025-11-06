using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Data;
using ManagementHotel.Controllers;
using ManagementHotel.DTOs.NhanVien;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
namespace ManagementHotel.Repositories
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly ManagementHotelContext _context;
        public NhanVienRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // Lấy tất cả nhân viên
        public async Task<IEnumerable<NhanVienResponseDto>> GetAllNhanVienAsync()
        {
            // lấy danh sách nhân viên từ cơ sở dữ liệu
            var nhanViens = await _context.nhanViens.ToListAsync();
            // chuyển đổi danh sách nhân viên sang DTO
            return nhanViens.Select(nv => new NhanVienResponseDto
            {
                MaNhanVien = nv.MaNhanVien,
                HoTen = nv.HoTen,
                CCCD = nv.CCCD,
                SoDienThoai = nv.SoDienThoai,
                Email = nv.Email,
                DiaChi = nv.DiaChi,
                NgaySinh = nv.NgaySinh,
                ChucVu = nv.ChucVu,
                Luong = nv.Luong
            });
        }

        // lấy nhân viên qua mã nhân viên
        public async Task<NhanVienResponseDto> GetNhanVienByIdAsync(int maNhanVien)
        {
            // tìm nhân viên theo mã nhân viên
            var nhanVien = await _context.nhanViens.FindAsync(maNhanVien);
            if(nhanVien != null)
            {
                return new NhanVienResponseDto
                {
                    MaNhanVien = nhanVien.MaNhanVien,
                    HoTen = nhanVien.HoTen,
                    NgaySinh = nhanVien.NgaySinh,
                    CCCD = nhanVien.CCCD,
                    SoDienThoai = nhanVien.SoDienThoai,
                    Email = nhanVien.Email,
                    DiaChi = nhanVien.DiaChi,
                    Luong = nhanVien.Luong,
                    ChucVu = nhanVien.ChucVu,
                };
            }
            return null!;
        }

    }
}
