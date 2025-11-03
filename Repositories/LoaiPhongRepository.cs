using ManagementHotel.Data;
using ManagementHotel.DTOs;
using ManagementHotel.DTOs.LoaiPhong;
using Microsoft.AspNetCore.Http.HttpResults;
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

        // Lấy thông tin loại phòng theo mã loại phòng
        public async Task<LoaiPhongResponseDto> GetLoaiPhongByIdAsync(int maLoaiPhong)
        {
            var loaiPhong = await _context.loaiPhongs.FindAsync(maLoaiPhong);
            if (loaiPhong != null)
            {
                return new LoaiPhongResponseDto
                {
                    MaLoaiPhong = loaiPhong.MaLoaiPhong,
                    TenLoaiPhong = loaiPhong.TenLoaiPhong,
                    MoTa = loaiPhong.MoTa,
                    GiaTheoDem = loaiPhong.GiaTheoDem
                };
            }
            return null!;
        }

        // Cập nhật loại phòng
        public async Task<LoaiPhongResponseDto> UpdateLoaiPhongAsync(int maLoaiPhong, UpdateLoaiPhongRequestDto loaiPhongUpdate)
        {
            var loaiPhong = await _context.loaiPhongs.FindAsync(maLoaiPhong);
            if (loaiPhong != null)
            {
                loaiPhong.TenLoaiPhong = loaiPhongUpdate.TenLoaiPhong;
                loaiPhong.MoTa = loaiPhongUpdate.MoTa;
                loaiPhong.GiaTheoDem = loaiPhongUpdate.GiaTheoDem;
                await _context.SaveChangesAsync();
                return new LoaiPhongResponseDto
                {
                    MaLoaiPhong = loaiPhong.MaLoaiPhong,
                    TenLoaiPhong = loaiPhong.TenLoaiPhong,
                    MoTa = loaiPhong.MoTa,
                    GiaTheoDem = loaiPhong.GiaTheoDem
                };
            }
            return null!;
        }

        // Xóa loại phòng
        public async Task<bool> DeleteLoaiPhongAsync(int maLoaiPhong)
        {
            var loaiPhong = await _context.loaiPhongs.FindAsync(maLoaiPhong);
            if (loaiPhong != null)
            {
                _context.loaiPhongs.Remove(loaiPhong);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Lọc loại phòng theo khoảng giá
        public async Task<IEnumerable<LoaiPhongResponseDto>> FilterLoaiPhongByPriceAsync(FilterLoaiPhongRequest filter)
        {
            var query = _context.loaiPhongs.AsQueryable();
            if (filter.GiaMin.HasValue)
            {
                query = query.Where(lp => lp.GiaTheoDem >= filter.GiaMin.Value);
            }
            if (filter.GiaMax.HasValue)
            {
                query = query.Where(lp => lp.GiaTheoDem <= filter.GiaMax.Value);
            }
            if (!string.IsNullOrEmpty(filter.TenLoaiPhong))
            {
                query = query.Where(lp => lp.TenLoaiPhong!.Contains(filter.TenLoaiPhong));
            }
            if (!string.IsNullOrEmpty(filter.SapXepTheo))
            {
                if (filter.SapXepTheo.Equals("GiaTheoDem", StringComparison.OrdinalIgnoreCase))
                {
                    query = filter.ThuTu!.Equals("DESC", StringComparison.OrdinalIgnoreCase)
                        ? query.OrderByDescending(lp => lp.GiaTheoDem)
                        : query.OrderBy(lp => lp.GiaTheoDem);
                }
                else if (filter.SapXepTheo.Equals("TenLoaiPhong", StringComparison.OrdinalIgnoreCase))
                {
                    query = filter.ThuTu!.Equals("DESC", StringComparison.OrdinalIgnoreCase)
                        ? query.OrderByDescending(lp => lp.TenLoaiPhong)
                        : query.OrderBy(lp => lp.TenLoaiPhong);
                }
            }
            var loaiPhongs = await query.ToListAsync();
            return loaiPhongs.Select(lp => new LoaiPhongResponseDto
            {
                MaLoaiPhong = lp.MaLoaiPhong,
                TenLoaiPhong = lp.TenLoaiPhong,
                MoTa = lp.MoTa,
                GiaTheoDem = lp.GiaTheoDem
            });
        }
    }
}
