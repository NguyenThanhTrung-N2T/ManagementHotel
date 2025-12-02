using ManagementHotel.Data;
using ManagementHotel.DTOs.DatPhong;
using ManagementHotel.Repositories.IRepositories;

namespace ManagementHotel.Repositories
{
    public class DatPhongRepository : IDatPhongRepository
    {
        private readonly ManagementHotelContext _context;

        public DatPhongRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // lấy tất cả đặt phòng
        public async Task<IEnumerable<DatPhongListResponseDto>> GetAllDatPhongsAsync()
        {
            // lấy danh sách đặt phòng từ cơ sở dữ liệu
            var datPhongs = _context.datPhongs.Select(dp => new DatPhongListResponseDto
            {
                MaDatPhong = dp.MaDatPhong,
                TenKhachHang = dp.KhachHang.HoTen,
                SoDienThoai = dp.KhachHang.SoDienThoai,
                TenPhong = dp.Phong.SoPhong,
                LoaiPhong = dp.Phong.LoaiPhong.TenLoaiPhong,
                NgayNhanPhong = dp.NgayNhanPhong,
                NgayTraPhong = dp.NgayTraPhong,
                TrangThai = dp.TrangThai
            });
            return datPhongs;
        }

        // lấy đặt phòng theo mã đặt phòng
        public async Task<DatPhongResponseDto?> GetDatPhongByIdAsync(int maDatPhong)
        {
            var datPhong = _context.datPhongs
                .Where(dp => dp.MaDatPhong == maDatPhong)

                .Select(dp => new DatPhongResponseDto
                {
                    MaDatPhong = dp.MaDatPhong,
                    TrangThai = dp.TrangThai,
                    NgayNhanPhong = dp.NgayNhanPhong,
                    NgayTraPhong = dp.NgayTraPhong,
                    KhachHangHoTen = dp.KhachHang.HoTen,
                    KhachHangSoDienThoai = dp.KhachHang.SoDienThoai,
                    SoPhong = dp.Phong.SoPhong,
                    TenLoaiPhong = dp.Phong.LoaiPhong.TenLoaiPhong,
                    GiaTheoDem = dp.Phong.LoaiPhong.GiaTheoDem,
                    HoaDon = dp.HoaDon != null ? new HoaDonInfoDTO
                    {
                        MaHoaDon = dp.HoaDon.MaHoaDon,
                        NgayLap = dp.HoaDon.NgayLap,
                        TongTien = dp.HoaDon.TongTien,
                        TrangThaiThanhToan = dp.HoaDon.TrangThaiThanhToan,
                        NhanVienLap = dp.HoaDon.NhanVien.HoTen,
                    } : null
                })
                .FirstOrDefault();
            return datPhong;
        }
    }
}
