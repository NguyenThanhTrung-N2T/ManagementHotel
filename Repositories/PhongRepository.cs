using ManagementHotel.Data;
using ManagementHotel.DTOs.Phong;
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
    }
}
