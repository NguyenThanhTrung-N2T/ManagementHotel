using ManagementHotel.Data;
using ManagementHotel.DTOs.KhachHang;
using Microsoft.EntityFrameworkCore;
using ManagementHotel.Repositories.IRepositories;
namespace ManagementHotel.Repositories
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly ManagementHotelContext _context;
        public KhachHangRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // Implement các phương thức từ IKhachHangRepository ở đây

        // Lấy tất cả khách hàng
        public async Task<IEnumerable<KhachHangResponseDto>> GetAllKhachHangAsync()
        {
            // Lấy tất cả khách hàng từ cơ sở dữ liệu
            var khachHangs = await _context.khachHangs.ToListAsync();
            // chuyển sang dto và trả về cho client
            return khachHangs.Select(kh => new KhachHangResponseDto
            {
                MaKhachHang = kh.MaKhachHang,
                HoTen = kh.HoTen,
                CCCD = kh.CCCD,
                SoDienThoai = kh.SoDienThoai,
                Email = kh.Email,
                DiaChi = kh.DiaChi
            });
        }
    }
}
