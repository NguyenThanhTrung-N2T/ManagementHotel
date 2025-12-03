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

            var hoaDon = new Models.HoaDon
            {
                MaDatPhong = maDatPhong,
                TrangThaiThanhToan = "Chưa thanh toán",
                NgayLap = DateTime.Now,
                TongTien = TinhTongTien(maDatPhong, datPhong.NgayTraPhong).Result
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

        // tính tổng tiền gồm tiền phòng và các dịch vụ phát sinh
        public async Task<int> TinhTongTien(int maDatPhong, DateTime ngayTraPhong)
        {
            var datPhong = await _context.datPhongs
                .Include(dp => dp.Phong)
                    .ThenInclude(p => p.LoaiPhong)
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == maDatPhong);
            if (datPhong == null) throw new Exception("Không tìm thấy đặt phòng");
            
            int soNgay = (datPhong.NgayTraPhong - datPhong.NgayNhanPhong).Days;

            if (ngayTraPhong < datPhong.NgayTraPhong)
            {
                soNgay = (ngayTraPhong - datPhong.NgayNhanPhong).Days;
            }

            int tienPhong = soNgay * datPhong.Phong.LoaiPhong.GiaTheoDem;
            if (datPhong.HoaDon?.ChiTietHoaDons == null || !datPhong.HoaDon.ChiTietHoaDons.Any())
            {
                return tienPhong;
            }
            var tienDichVu = await _context.chiTietHoaDons
                .Where(cthd => cthd.HoaDon.MaDatPhong == maDatPhong)
                .SumAsync(cthd => cthd.DonGia*cthd.SoLuong);

            return tienPhong + tienDichVu;
        
        }

        // cập nhật tổng tiền vào
        public async Task<HoaDonResponseDto> UpdateTongTienInHoaDon(int tongTien, int maHoaDon)
        {
            var hoaDon = await _context.hoaDons.FindAsync(maHoaDon);
            if(hoaDon == null)
            {
                throw new Exception("Mã hóa đơn không tồn tại.");
            }
            hoaDon.TongTien = tongTien;
            _context.hoaDons.Update(hoaDon);
            await _context.SaveChangesAsync();
            return new HoaDonResponseDto
            {
                MaHoaDon = maHoaDon,
                TongTien = tongTien,
                TrangThaiThanhToan = hoaDon.TrangThaiThanhToan,
                NgayLap = hoaDon.NgayLap
            };
        }

    }
}
