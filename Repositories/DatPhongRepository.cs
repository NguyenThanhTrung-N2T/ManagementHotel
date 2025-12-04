using ManagementHotel.Data;
using ManagementHotel.DTOs.DatPhong;
using ManagementHotel.Models;
using ManagementHotel.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

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
                    } : null
                })
                .FirstOrDefault();
            return datPhong;
        }

        // tạo đặt phòng mới
        public async Task<DatPhongResponseDto> CreateDatPhongAsync(CreateDatPhongRequestDto createDatPhongRequestDto)
        {
            var datPhongEntity = new Models.DatPhong
            {
                MaKhachHang = createDatPhongRequestDto.MaKhachHang,
                MaPhong = createDatPhongRequestDto.MaPhong,
                NgayNhanPhong = createDatPhongRequestDto.NgayNhanPhong,
                NgayTraPhong = createDatPhongRequestDto.NgayTraPhong,
                TrangThai = createDatPhongRequestDto.TrangThai,
                GhiChu = createDatPhongRequestDto.GhiChu
            };
            _context.datPhongs.Add(datPhongEntity);
            await _context.SaveChangesAsync();
            // Trả về thông tin đặt phòng vừa tạo
            var createdDatPhong = await GetDatPhongByIdAsync(datPhongEntity.MaDatPhong);
            return createdDatPhong!;
        }

        // kiểm tra đặt phòng còn trống hay không trong thời gian đặt 
        public async Task<bool> IsPhongAvailableAsync(int maPhong, DateTime ngayNhanPhong, DateTime ngayTraPhong)
        {
            // kiểm tra trong cơ sở dữ liệu có đặt phòng nào trùng với phòng và thời gian không
            var overlappingBooking = _context.datPhongs.Any(dp =>
                dp.MaPhong == maPhong &&
                dp.TrangThai != "Đã hủy" && // bỏ qua các đặt phòng đã hủy
                (
                    (ngayNhanPhong >= dp.NgayNhanPhong && ngayNhanPhong < dp.NgayTraPhong) || // ngày nhận phòng trùng
                    (ngayTraPhong > dp.NgayNhanPhong && ngayTraPhong <= dp.NgayTraPhong) ||   // ngày trả phòng trùng
                    (ngayNhanPhong <= dp.NgayNhanPhong && ngayTraPhong >= dp.NgayTraPhong)    // bao phủ toàn bộ khoảng thời gian
                )
            );
            return !overlappingBooking;
        }

        // cập nhật trạng thái đặt phòng
        public async Task<DatPhongResponseDto?> UpdateDatPhongStatusAsync(int maDatPhong, string trangThai)
        {
            var datPhong = await _context.datPhongs.FindAsync(maDatPhong);
            if (datPhong == null)
            {
                throw new Exception("Đặt phòng với mã " + maDatPhong + " không tồn tại.");
            }
            datPhong.TrangThai = trangThai;
            if (trangThai == "Đã hủy")
            {
                // nếu trạng thái là đã hủy, cập nhật trạng thái phòng về "Trống"
                var phong = await _context.phongs.FindAsync(datPhong.MaPhong);
                if (phong != null)
                {
                    phong.TrangThai = "Trống";
                    _context.phongs.Update(phong);
                }
            }
            _context.datPhongs.Update(datPhong);
            await _context.SaveChangesAsync();
            return await GetDatPhongByIdAsync(maDatPhong);
        }

        // lọc đặt phòng theo trạng thái 
        public async Task<IEnumerable<DatPhongListResponseDto>> FilterDatPhongByStatusAsync(string trangThai)
        {
            var datPhongs = _context.datPhongs
                .Where(dp => dp.TrangThai == trangThai)
                .Select(dp => new DatPhongListResponseDto
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
    }
}
