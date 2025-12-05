using ManagementHotel.Data;
using ManagementHotel.Models;
using ManagementHotel.DTOs.BaoCaoDoanhThu;
using ManagementHotel.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
namespace ManagementHotel.Repositories
{
    public class BaoCaoDoanhThuRepository : IBaoCaoDoanhThuRepository
    {
        private readonly ManagementHotelContext _context;
        public BaoCaoDoanhThuRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // tạo báo cáo doanh thu 
        public async Task<BaoCaoDoanhThuResponseDto> CreateBaoCaoDoanhThuAsync()
        {
            // lấy ngày hiện tại
            DateTime currentDate = DateTime.Now;
            int thang = currentDate.Month;
            int nam = currentDate.Year;
            // tính tổng doanh thu trong tháng
            int tongDoanhThu = _context.hoaDons
                .Where(hd => hd.NgayLap.Month == thang && hd.NgayLap.Year == nam && hd.TrangThaiThanhToan == "Đã thanh toán")
                .Sum(hd => hd.TongTien);

            // tạo báo cáo doanh thu mới
            var baoCaoMoi = new BaoCaoDoanhThu
            {
                Ngay = currentDate,
                Thang = thang,
                Nam = nam,
                TongDoanhThu = tongDoanhThu,
                GhiChu = "Báo cáo doanh thu tháng " + thang + " năm " + nam
            };

            // lưu vào cơ sở dữ liệu
            _context.baoCaoDoanhThus.Add(baoCaoMoi);
            await _context.SaveChangesAsync();
            // trả về báo cáo doanh thu dưới dạng DTO
            return new BaoCaoDoanhThuResponseDto
            {
                MaBaoCao = baoCaoMoi.MaBaoCao,
                NgayLap = baoCaoMoi.Ngay,
                Thang = baoCaoMoi.Thang,
                Nam = baoCaoMoi.Nam,
                TongDoanhThu = baoCaoMoi.TongDoanhThu,
                GhiChu = baoCaoMoi.GhiChu
            };
        }

        // lọc báo cáo doanh thu theo tháng và năm
        public async Task<BaoCaoDoanhThuResponseDto?> GetBaoCaoDoanhThuByThangNamAsync(int thang, int nam)
        {
            var baoCao = await _context.baoCaoDoanhThus
                .FirstOrDefaultAsync(bc => bc.Thang == thang && bc.Nam == nam);
            if (baoCao == null)
            {
                return null;
            }
            return new BaoCaoDoanhThuResponseDto
            {
                MaBaoCao = baoCao.MaBaoCao,
                NgayLap = baoCao.Ngay,
                Thang = baoCao.Thang,
                Nam = baoCao.Nam,
                TongDoanhThu = baoCao.TongDoanhThu,
                GhiChu = baoCao.GhiChu
            };
        }

        // lấy tất cả báo cáo doanh thu
        public async Task<IEnumerable<BaoCaoDoanhThuResponseDto>> GetAllBaoCaoDoanhThuAsync()
        {
            var baoCaos = await _context.baoCaoDoanhThus.ToListAsync();
            return baoCaos.Select(bc => new BaoCaoDoanhThuResponseDto
            {
                MaBaoCao = bc.MaBaoCao,
                NgayLap = bc.Ngay,
                Thang = bc.Thang,
                Nam = bc.Nam,
                TongDoanhThu = bc.TongDoanhThu,
                GhiChu = bc.GhiChu
            });
        }
    }
}
