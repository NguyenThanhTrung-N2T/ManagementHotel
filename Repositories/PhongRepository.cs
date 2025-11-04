using ManagementHotel.Data;
using ManagementHotel.DTOs.Phong;
using ManagementHotel.Models;
using Microsoft.EntityFrameworkCore;
namespace ManagementHotel.Repositories
{
    public class PhongRepository : IPhongRepository
    {
        private readonly ManagementHotelContext _context;

        public PhongRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // Implement các phương thức từ IPhongRepository ở đây

        // Lấy tất cả phòng
        public async Task<IEnumerable<PhongResponseDto>> GetAllPhongAsync()
        {
            var phongs = await _context.phongs.ToListAsync();
            return phongs.Select(p => new PhongResponseDto
            {
                MaPhong = p.MaPhong,
                SoPhong = p.SoPhong,
                TrangThai = p.TrangThai,
                GhiChu = p.GhiChu
            });
        }

        // Lấy phòng theo mã phòng
        public async Task<PhongResponseDto> GetPhongByIdAsync(int maPhong)
        {
            var phong = await _context.phongs.FindAsync(maPhong);
            if(phong != null)
            {
                return new PhongResponseDto
                {
                    MaPhong = phong.MaPhong,
                    SoPhong = phong.SoPhong,
                    TrangThai = phong.TrangThai,
                    GhiChu = phong.GhiChu
                };
            }
            return null!;
        }

        // Thêm phòng mới
        public async Task<PhongResponseDto> AddPhongAsync(CreatePhongRequestDto phong)
        {
            try
            {
                var newPhong = new Phong
                {
                    SoPhong = phong.SoPhong,
                    TrangThai = phong.TrangThai,
                    GhiChu = phong.GhiChu
                };
                _context.phongs.Add(newPhong);
                await _context.SaveChangesAsync();
                return new PhongResponseDto
                {
                    MaPhong = newPhong.MaPhong,
                    SoPhong = newPhong.SoPhong,
                    TrangThai = newPhong.TrangThai,
                    GhiChu = newPhong.GhiChu
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm phòng: " + ex.Message);
            }
        }
    }
}
