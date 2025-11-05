using ManagementHotel.Data;
using ManagementHotel.DTOs.Phong;
using ManagementHotel.Models;
using ManagementHotel.Repositories.IRepositories;
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
            // Lấy tất cả phòng từ cơ sở dữ liệu
            var phongs = await _context.phongs.ToListAsync();
            // chuyển sang dto và trả về cho client
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
            // Tìm phòng theo mã phòng
            var phong = await _context.phongs.FindAsync(maPhong);
            // nếu tìm thấy, chuyển sang dto và trả về cho client
            if (phong != null)
            {
                // Trả về DTO của phòng
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
                // Tạo đối tượng Phong mới từ DTO
                var newPhong = new Phong
                {
                    SoPhong = phong.SoPhong,
                    MaLoaiPhong = phong.MaLoaiPhong,
                    TrangThai = phong.TrangThai,
                    GhiChu = phong.GhiChu
                };
                // Thêm phòng vào cơ sở dữ liệu
                _context.phongs.Add(newPhong);
                await _context.SaveChangesAsync();
                // Trả về DTO của phòng vừa tạo cho client
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

        // Kiểm tra sự tồn tại của số phòng
        public async Task<bool> IsPhongNumberExistsAsync(string? soPhong)
        {
            // Kiểm tra sự tồn tại của số phòng trong cơ sở dữ liệu
            return await _context.phongs.AnyAsync(p => p.SoPhong == soPhong);
        }

        // Cập nhật thông tin phòng
        public async Task<PhongResponseDto> UpdatePhongAsync(int maPhong, UpdatePhongRequestDto phong)
        {
            try
            {
                // Tìm phòng theo mã phòng
                var existingPhong = await _context.phongs.FindAsync(maPhong);
                // Nếu không tìm thấy, ném ngoại lệ
                if (existingPhong == null)
                {
                    throw new Exception("Phòng không tồn tại.");
                }
                // Cập nhật thông tin phòng
                existingPhong.SoPhong = phong.SoPhong;
                existingPhong.MaLoaiPhong = phong.MaLoaiPhong;
                existingPhong.TrangThai = phong.TrangThai;
                existingPhong.GhiChu = phong.GhiChu;
                // Lưu thay đổi vào cơ sở dữ liệu
                _context.phongs.Update(existingPhong);
                await _context.SaveChangesAsync();
                // Trả về DTO của phòng đã cập nhật cho client
                return new PhongResponseDto
                {
                    MaPhong = existingPhong.MaPhong,
                    SoPhong = existingPhong.SoPhong,
                    TrangThai = existingPhong.TrangThai,
                    GhiChu = existingPhong.GhiChu
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật phòng: " + ex.Message);
            }
        }

        // Xóa phòng
        public async Task<bool> DeletePhongAsync(int maPhong)
        {
            try
            {
                // Tìm phòng theo mã phòng
                var existingPhong = await _context.phongs.FindAsync(maPhong);
                // Nếu không tìm thấy, trả về false
                if (existingPhong == null)
                {
                    return false;
                }
                // Xóa phòng khỏi cơ sở dữ liệu
                _context.phongs.Remove(existingPhong);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa phòng: " + ex.Message);
            }
        }

        // Lọc phòng theo trạng thái
        public async Task<IEnumerable<PhongResponseDto>> FilterPhongByStatusAsync(FilterPhongRequest filter)
        {
            // Lấy các phòng có trạng thái phù hợp từ cơ sở dữ liệu
            var query = _context.phongs.AsQueryable();
            // Kiểm tra trạng thái nếu có trong filter
            if (!string.IsNullOrEmpty(filter.TrangThai))
            {
                query = query.Where(p => p.TrangThai == filter.TrangThai);
            }
            var phongs = await query.ToListAsync();
            // chuyển sang dto và trả về cho client
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
