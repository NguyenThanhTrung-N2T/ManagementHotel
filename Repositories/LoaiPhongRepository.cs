using ManagementHotel.Data;
using ManagementHotel.DTOs;
using ManagementHotel.DTOs.LoaiPhong;
using ManagementHotel.Models;
using ManagementHotel.Repositories.IRepositories;
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
            // Lấy tất cả loại phòng từ cơ sở dữ liệu
            var loaiPhongs = await _context.loaiPhongs.ToListAsync();
            // chuyển sang dto và trả về cho client
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
            try
            {
                // Tạo đối tượng LoaiPhong từ DTO
                var loaiPhong = new ManagementHotel.Models.LoaiPhong
                {
                    TenLoaiPhong = loaiPhongNew.TenLoaiPhong,
                    MoTa = loaiPhongNew.MoTa,
                    GiaTheoDem = loaiPhongNew.GiaTheoDem
                };
                // Thêm vào cơ sở dữ liệu
                _context.loaiPhongs.Add(loaiPhong);
                await _context.SaveChangesAsync();
                // Trả về DTO của loại phòng vừa tạo cho client 
                return new LoaiPhongResponseDto
                {
                    MaLoaiPhong = loaiPhong.MaLoaiPhong,
                    TenLoaiPhong = loaiPhong.TenLoaiPhong,
                    MoTa = loaiPhong.MoTa,
                    GiaTheoDem = loaiPhong.GiaTheoDem
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm loại phòng: {ex.Message}");

            }
        }

        // Lấy thông tin loại phòng theo mã loại phòng
        public async Task<LoaiPhongResponseDto> GetLoaiPhongByIdAsync(int maLoaiPhong)
        {
            // Tìm loại phòng theo mã
            var loaiPhong = await _context.loaiPhongs.FindAsync(maLoaiPhong);
            // Nếu tìm thấy, chuyển sang DTO và trả về
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
            try
            {
                // Tìm loại phòng theo mã
                var loaiPhong = await _context.loaiPhongs.FindAsync(maLoaiPhong);
                // Nếu tìm thấy, cập nhật thông tin và lưu thay đổi
                if (loaiPhong == null)
                {
                    throw new Exception("Loại phòng không tồn tại.");
                }
                // Cập nhật thông tin
                loaiPhong.TenLoaiPhong = loaiPhongUpdate.TenLoaiPhong;
                loaiPhong.MoTa = loaiPhongUpdate.MoTa;
                loaiPhong.GiaTheoDem = loaiPhongUpdate.GiaTheoDem;
                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
                // Trả về DTO của loại phòng đã cập nhật cho client
                return new LoaiPhongResponseDto
                {
                    MaLoaiPhong = loaiPhong.MaLoaiPhong,
                    TenLoaiPhong = loaiPhong.TenLoaiPhong,
                    MoTa = loaiPhong.MoTa,
                    GiaTheoDem = loaiPhong.GiaTheoDem
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật loại phòng: {ex.Message}");
            }
        }

        // Xóa loại phòng
        public async Task<bool> DeleteLoaiPhongAsync(int maLoaiPhong)
        {
            try
            {
                // Tìm loại phòng theo mã
                var loaiPhong = await _context.loaiPhongs.FindAsync(maLoaiPhong);
                // Nếu tìm thấy, xóa khỏi cơ sở dữ liệu
                if (loaiPhong != null)
                {
                    // Xóa loại phòng
                    _context.loaiPhongs.Remove(loaiPhong);
                    await _context.SaveChangesAsync();
                    // Trả về true nếu xóa thành công
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                // Trả về false nếu có lỗi xảy ra
                return false;

            }
        }

        // Lọc loại phòng theo các tiêu chí
        public async Task<IEnumerable<LoaiPhongResponseDto>> FilterLoaiPhongByPriceAsync(FilterLoaiPhongRequest filter)
        {
            // Tạo truy vấn cơ sở dữ liệu
            var query = _context.loaiPhongs.AsQueryable();
            // Lọc theo giá min nếu có
            if (filter.GiaMin.HasValue)
            {
                query = query.Where(lp => lp.GiaTheoDem >= filter.GiaMin.Value);
            }
            // Lọc theo giá max nếu có
            if (filter.GiaMax.HasValue)
            {
                query = query.Where(lp => lp.GiaTheoDem <= filter.GiaMax.Value);
            }
            // Lọc theo tên loại phòng nếu có
            if (!string.IsNullOrEmpty(filter.TenLoaiPhong))
            {
                query = query.Where(lp => lp.TenLoaiPhong!.Contains(filter.TenLoaiPhong));
            }
            // Sắp xếp kết quả nếu có
            if (!string.IsNullOrEmpty(filter.SapXepTheo))
            {
                // Sắp xếp theo giá theo đêm
                if (filter.SapXepTheo.Equals("GiaTheoDem", StringComparison.OrdinalIgnoreCase))
                {
                    // Sắp xếp theo thứ tự tăng dần hoặc giảm dần
                    query = filter.ThuTu!.Equals("DESC", StringComparison.OrdinalIgnoreCase)
                        ? query.OrderByDescending(lp => lp.GiaTheoDem)
                        : query.OrderBy(lp => lp.GiaTheoDem);
                }
                // Sắp xếp theo tên loại phòng
                else if (filter.SapXepTheo.Equals("TenLoaiPhong", StringComparison.OrdinalIgnoreCase))
                {
                    // Sắp xếp theo thứ tự tăng dần hoặc giảm dần
                    query = filter.ThuTu!.Equals("DESC", StringComparison.OrdinalIgnoreCase)
                        ? query.OrderByDescending(lp => lp.TenLoaiPhong)
                        : query.OrderBy(lp => lp.TenLoaiPhong);
                }
            }
            // Thực hiện truy vấn và lấy kết quả
            var loaiPhongs = await query.ToListAsync();
            // Chuyển sang DTO và trả về cho client
            return loaiPhongs.Select(lp => new LoaiPhongResponseDto
            {
                MaLoaiPhong = lp.MaLoaiPhong,
                TenLoaiPhong = lp.TenLoaiPhong,
                MoTa = lp.MoTa,
                GiaTheoDem = lp.GiaTheoDem
            });
        }

        // Kiểm tra tồn tại của tên loại phòng
        public async Task<bool> IsLoaiPhongNameExistsAsync(string? tenLoaiPhong)
        {
            // Kiểm tra sự tồn tại của tên loại phòng trong cơ sở dữ liệu
            return await _context.loaiPhongs.AnyAsync(lp => lp.TenLoaiPhong == tenLoaiPhong);
        }
    }
}
