using ManagementHotel.Data;
using ManagementHotel.DTOs;
using Microsoft.EntityFrameworkCore;
namespace ManagementHotel.Repositories
{
    public class LoaiPhongRepository : ILoaiPhongRepository
    {
        private readonly ManagementHotelContext _context;
        public LoaiPhongRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // Implement các phương thức từ ILoaiPhongRepository ở đây

        // Lấy tất cả loại phòng
        public async Task<IEnumerable<LoaiPhongResponseDto>> GetAllLoaiPhongAsync()
        {
            var loaiPhongs = await _context.loaiPhongs.ToListAsync();
            return loaiPhongs.Select(lp => new LoaiPhongResponseDto
            {
                MaLoaiPhong = lp.MaLoaiPhong,
                TenLoaiPhong = lp.TenLoaiPhong,
                MoTa = lp.MoTa,
                GiaTheoDem = lp.GiaTheoDem
            });
        }

        // Thêm loại phòng mới
        public async Task<LoaiPhongResponseDto> AddLoaiPhongAsync(CreateLoaiPhongRequestDto loaiPhongNew)
        {
            var loaiPhong = new ManagementHotel.Models.LoaiPhong
            {
                TenLoaiPhong = loaiPhongNew.TenLoaiPhong,
                MoTa = loaiPhongNew.MoTa,
                GiaTheoDem = loaiPhongNew.GiaTheoDem
            };
            _context.loaiPhongs.Add(loaiPhong);
            await _context.SaveChangesAsync();
            return new LoaiPhongResponseDto
            {
                MaLoaiPhong = loaiPhong.MaLoaiPhong,
                TenLoaiPhong = loaiPhong.TenLoaiPhong,
                MoTa = loaiPhong.MoTa,
                GiaTheoDem = loaiPhong.GiaTheoDem
            };
        }
    }
}
