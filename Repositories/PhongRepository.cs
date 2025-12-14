using ManagementHotel.Data;
using ManagementHotel.DTOs.Phong;
using ManagementHotel.Models;
using ManagementHotel.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ManagementHotel.Repositories {
    public class PhongRepository : IPhongRepository {
        private readonly ManagementHotelContext _context;

        public PhongRepository(ManagementHotelContext context) {
            _context = context;
        }

        // =========================
        // LẤY TẤT CẢ PHÒNG
        // =========================
        public async Task<IEnumerable<PhongResponseDto>> GetAllPhongAsync() {
            // Cập nhật trạng thái phòng (SP)
            await _context.Database.ExecuteSqlRawAsync("EXEC CapNhatTrangThaiPhong");

            return await _context.phongs
                .Include(p => p.LoaiPhong)
                .Select(p => new PhongResponseDto {
                    MaPhong = p.MaPhong,
                    SoPhong = p.SoPhong,
                    TrangThai = p.TrangThai,
                    GhiChu = p.GhiChu,
                    TenLoaiPhong = p.LoaiPhong.TenLoaiPhong
                })
                .ToListAsync();
        }

        // =========================
        // LẤY PHÒNG THEO ID
        // =========================
        public async Task<PhongResponseDto?> GetPhongByIdAsync(int maPhong) {
            return await _context.phongs
                .Include(p => p.LoaiPhong)
                .Where(p => p.MaPhong == maPhong)
                .Select(p => new PhongResponseDto {
                    MaPhong = p.MaPhong,
                    SoPhong = p.SoPhong,
                    TrangThai = p.TrangThai,
                    GhiChu = p.GhiChu,
                    TenLoaiPhong = p.LoaiPhong.TenLoaiPhong
                })
                .FirstOrDefaultAsync();
        }

        // =========================
        // THÊM PHÒNG
        // =========================
        public async Task<PhongResponseDto> AddPhongAsync(CreatePhongRequestDto phong) {
            var newPhong = new Phong {
                SoPhong = phong.SoPhong,
                MaLoaiPhong = phong.MaLoaiPhong,
                TrangThai = phong.TrangThai,
                GhiChu = phong.GhiChu
            };

            _context.phongs.Add(newPhong);
            await _context.SaveChangesAsync();

            // Load navigation property
            await _context.Entry(newPhong)
                .Reference(p => p.LoaiPhong)
                .LoadAsync();

            return new PhongResponseDto {
                MaPhong = newPhong.MaPhong,
                SoPhong = newPhong.SoPhong,
                TrangThai = newPhong.TrangThai,
                GhiChu = newPhong.GhiChu,
                TenLoaiPhong = newPhong.LoaiPhong.TenLoaiPhong
            };
        }

        // =========================
        // KIỂM TRA TRÙNG SỐ PHÒNG
        // =========================
        public async Task<bool> IsPhongNumberExistsAsync(string? soPhong) {
            return await _context.phongs.AnyAsync(p => p.SoPhong == soPhong);
        }

        // =========================
        // CẬP NHẬT PHÒNG
        // =========================
        public async Task<PhongResponseDto> UpdatePhongAsync(int maPhong, UpdatePhongRequestDto phong) {
            var existingPhong = await _context.phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefaultAsync(p => p.MaPhong == maPhong);

            if (existingPhong == null) {
                throw new Exception("Phòng không tồn tại.");
            }

            existingPhong.SoPhong = phong.SoPhong;
            existingPhong.MaLoaiPhong = phong.MaLoaiPhong;
            existingPhong.TrangThai = phong.TrangThai;
            existingPhong.GhiChu = phong.GhiChu;

            await _context.SaveChangesAsync();

            return new PhongResponseDto {
                MaPhong = existingPhong.MaPhong,
                SoPhong = existingPhong.SoPhong,
                TrangThai = existingPhong.TrangThai,
                GhiChu = existingPhong.GhiChu,
                TenLoaiPhong = existingPhong.LoaiPhong.TenLoaiPhong
            };
        }

        // =========================
        // XÓA PHÒNG
        // =========================
        public async Task<bool> DeletePhongAsync(int maPhong) {
            var phong = await _context.phongs.FindAsync(maPhong);

            if (phong == null)
                return false;

            _context.phongs.Remove(phong);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // LỌC PHÒNG
        // =========================
        public async Task<IEnumerable<PhongResponseDto>> FilterPhongByStatusAsync(FilterPhongRequest filter) {
            var query = _context.phongs
                .Include(p => p.LoaiPhong)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.TrangThai)) {
                query = query.Where(p => p.TrangThai == filter.TrangThai);
            }

            if (filter.MaLoaiPhong.HasValue) {
                query = query.Where(p => p.MaLoaiPhong == filter.MaLoaiPhong.Value);
            }

            return await query
                .Select(p => new PhongResponseDto {
                    MaPhong = p.MaPhong,
                    SoPhong = p.SoPhong,
                    TrangThai = p.TrangThai,
                    GhiChu = p.GhiChu,
                    TenLoaiPhong = p.LoaiPhong.TenLoaiPhong
                })
                .ToListAsync();
        }
    }
}
