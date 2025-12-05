using ManagementHotel.Data;
using ManagementHotel.DTOs.ChiTietHoaDon;
using ManagementHotel.Repositories.IRepositories;
namespace ManagementHotel.Repositories
{
    public class ChiTietHoaDonRepository : IChiTietHoaDonRepository
    {
        private readonly ManagementHotelContext _context;
        public ChiTietHoaDonRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // thêm chi tiết hóa đơn cho hóa đơn
        public async Task<ChiTietHoaDonResponseDto> AddDichVuToChiTietHoaDonAsync(CreateChiTietHoaDonRequestDto createDto)
        {
            // kiểm tra hóa đơn tồn tại
            var hoaDon = await _context.hoaDons.FindAsync(createDto.MaHoaDon);
            if (hoaDon == null)
            {
                throw new Exception("Hóa đơn không tồn tại.");
            }

            // kiểm tra dịch vụ có tồn tại và hoạt động hay không
            var dichVu = await _context.dichVus.FindAsync(createDto.MaDichVu);
            if (dichVu == null || dichVu.TrangThai != "Hoạt động")
            {
                throw new Exception("Dịch vụ không tồn tại hoặc không hoạt động.");
            }

            // tạo chi tiết hóa đơn mới
            var chiTietHoaDon = new Models.ChiTietHoaDon
            {
                MaHoaDon = createDto.MaHoaDon,
                MaDichVu = createDto.MaDichVu,
                SoLuong = createDto.SoLuong,
                DonGia = createDto.DonGia,
                Mota = createDto.MoTa
            };
            _context.chiTietHoaDons.Add(chiTietHoaDon);
            await _context.SaveChangesAsync();
            // trả về chi tiết hóa đơn đã tạo
            return new ChiTietHoaDonResponseDto
            {
                MaChiTietHD = chiTietHoaDon.MaChiTietHD,
                MaDichVu = chiTietHoaDon.MaDichVu,
                TenDichVu = dichVu.TenDichVu,
                SoLuong = chiTietHoaDon.SoLuong,
                DonGia = chiTietHoaDon.DonGia,
                Mota = chiTietHoaDon.Mota
            };
        }

        // xóa chi tiết hóa đơn theo mã chi tiết hóa đơn
        public async Task<bool> DeleteChiTietHoaDonAsync(int maChiTietHD)
        {
            var chiTietHoaDon = await _context.chiTietHoaDons.FindAsync(maChiTietHD);
            if (chiTietHoaDon == null)
            {
                return false;
            }
            _context.chiTietHoaDons.Remove(chiTietHoaDon);
            await _context.SaveChangesAsync();
            return true;
        }

        // cập nhật chi tiết hóa đơn theo mã chi tiết hóa đơn
        public async Task<ChiTietHoaDonResponseDto> UpdateChiTietHoaDonAsync(int maChiTietHD, UpdateChiTietHoaDonRequestDto updateDto)
        {
            var chiTietHoaDon = await _context.chiTietHoaDons.FindAsync(maChiTietHD);
            if (chiTietHoaDon == null)
            {
                throw new Exception("Chi tiết hóa đơn không tồn tại.");
            }
            // kiểm tra dịch vụ có tồn tại và hoạt động hay không
            var dichVu = await _context.dichVus.FindAsync(updateDto.MaDichVu);
            if (dichVu == null || dichVu.TrangThai != "Hoạt động")
            {
                throw new Exception("Dịch vụ không tồn tại hoặc không hoạt động.");
            }
            // kiểm tra hóa đơn có tồn tại hay không
            var hoaDon = await _context.hoaDons.FindAsync(updateDto.MaHoaDon);
            if (hoaDon == null)
            {
                throw new Exception("Hóa đơn không tồn tại.");
            }
            // cập nhật thông tin chi tiết hóa đơn
            chiTietHoaDon.SoLuong = updateDto.SoLuong;
            chiTietHoaDon.DonGia = updateDto.DonGia;
            chiTietHoaDon.Mota = updateDto.MoTa;
            await _context.SaveChangesAsync();
            // lấy tên dịch vụ để trả về
            var result = await _context.dichVus.FindAsync(chiTietHoaDon.MaDichVu);
            return new ChiTietHoaDonResponseDto
            {
                MaChiTietHD = chiTietHoaDon.MaChiTietHD,
                MaDichVu = chiTietHoaDon.MaDichVu,
                TenDichVu = result!.TenDichVu,
                SoLuong = chiTietHoaDon.SoLuong,
                DonGia = chiTietHoaDon.DonGia,
                Mota = chiTietHoaDon.Mota
            };
        }
    }
}
