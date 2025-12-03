using ManagementHotel.Data;
using ManagementHotel.DTOs.HoaDon;
using ManagementHotel.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ManagementHotel.Repositories
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private readonly ManagementHotelContext _context;

        public HoaDonRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // tạo hóa đơn từ mã đặt phòng 
        public async Task<HoaDonResponseDto> CreateHoaDonAsync(int maDatPhong)
        {
            var datPhong = await _context.datPhongs
                .Include(dp => dp.Phong)
                    .ThenInclude(p => p.LoaiPhong)
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == maDatPhong);

            if (datPhong == null) throw new Exception("Không tìm thấy đặt phòng");

            var soNgay = (datPhong.NgayTraPhong - datPhong.NgayNhanPhong).Days;
            var giaTheoDem = datPhong.Phong.LoaiPhong.GiaTheoDem;
            var tienPhong = soNgay * giaTheoDem;

            var hoaDon = new Models.HoaDon
            {
                MaDatPhong = maDatPhong,
                TrangThaiThanhToan = "Chưa thanh toán",
                NgayLap = DateTime.Now,
                TongTien = tienPhong
            };

            _context.hoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            return new HoaDonResponseDto
            {
                MaHoaDon = hoaDon.MaHoaDon,
                TrangThaiThanhToan = hoaDon.TrangThaiThanhToan,
                TongTien = hoaDon.TongTien,
                NgayLap = hoaDon.NgayLap
            };
        }

    }
}
