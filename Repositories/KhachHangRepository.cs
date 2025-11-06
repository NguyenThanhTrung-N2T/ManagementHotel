using ManagementHotel.Data;
using ManagementHotel.DTOs.KhachHang;
using Microsoft.EntityFrameworkCore;
using ManagementHotel.Repositories.IRepositories;
namespace ManagementHotel.Repositories
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly ManagementHotelContext _context;
        public KhachHangRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // Implement các phương thức từ IKhachHangRepository ở đây

        // Lấy tất cả khách hàng
        public async Task<IEnumerable<KhachHangResponseDto>> GetAllKhachHangAsync()
        {
            // Lấy tất cả khách hàng từ cơ sở dữ liệu
            var khachHangs = await _context.khachHangs.ToListAsync();
            // chuyển sang dto và trả về cho client
            return khachHangs.Select(kh => new KhachHangResponseDto
            {
                MaKhachHang = kh.MaKhachHang,
                HoTen = kh.HoTen,
                CCCD = kh.CCCD,
                SoDienThoai = kh.SoDienThoai,
                Email = kh.Email,
                DiaChi = kh.DiaChi
            });
        }

        // Lấy khách hàng theo mã khách hàng
        public async Task<KhachHangResponseDto?> GetKhachHangByIdAsync(int maKhachHang)
        {
            // Tìm khách hàng theo mã khách hàng
            var khachHang = await _context.khachHangs.FindAsync(maKhachHang);
            // nếu không tìm thấy trả về null
            if (khachHang == null)
            {
                // không tìm thấy khách hàng
                return null;
            }
            // chuyển sang dto và trả về cho client
            return new KhachHangResponseDto
            {
                MaKhachHang = khachHang.MaKhachHang,
                HoTen = khachHang.HoTen,
                CCCD = khachHang.CCCD,
                SoDienThoai = khachHang.SoDienThoai,
                Email = khachHang.Email,
                DiaChi = khachHang.DiaChi
            };
        }

        // Thêm khách hàng mới
        public async Task<KhachHangResponseDto> AddKhachHangAsync(CreateKhachHangRequestDto khachHangNew)
        {
            try
            {
                // Tạo đối tượng KhachHang từ DTO
                var khachHang = new ManagementHotel.Models.KhachHang
                {
                    HoTen = khachHangNew.HoTen,
                    CCCD = khachHangNew.CCCD,
                    SoDienThoai = khachHangNew.SoDienThoai,
                    Email = khachHangNew.Email,
                    DiaChi = khachHangNew.DiaChi
                };
                // Thêm vào cơ sở dữ liệu
                _context.khachHangs.Add(khachHang);
                await _context.SaveChangesAsync();
                // Trả về DTO của khách hàng vừa tạo cho client 
                return new KhachHangResponseDto
                {
                    MaKhachHang = khachHang.MaKhachHang,
                    HoTen = khachHang.HoTen,
                    CCCD = khachHang.CCCD,
                    SoDienThoai = khachHang.SoDienThoai,
                    Email = khachHang.Email,
                    DiaChi = khachHang.DiaChi
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm khách hàng: {ex.Message}");
            }
        }

        // Kiểm tra sự tồn tại của khách hàng theo CCCD
        public async Task<bool> IsKhachHangExistsByCCCDAsync(string? cccd)
        {
            // Kiểm tra sự tồn tại của khách hàng với CCCD đã cho
            return await _context.khachHangs.AnyAsync(kh => kh.CCCD == cccd);
        }

        // Cập nhật thông tin khách hàng
        public async Task<KhachHangResponseDto> UpdateKhachHangAsync(int maKhachHang, UpdateKhachHangRequest khachHangUpdate)
        {
            try
            {
                // Tìm khách hàng theo mã khách hàng
                var khachHang = await _context.khachHangs.FindAsync(maKhachHang);
                if (khachHang == null)
                {
                    throw new Exception("Khách hàng không tồn tại.");
                }
                // Cập nhật thông tin khách hàng
                khachHang.HoTen = khachHangUpdate.HoTen;
                khachHang.SoDienThoai = khachHangUpdate.SoDienThoai;
                khachHang.Email = khachHangUpdate.Email;
                khachHang.DiaChi = khachHangUpdate.DiaChi;
                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
                // Trả về DTO của khách hàng đã cập nhật cho client
                return new KhachHangResponseDto
                {
                    MaKhachHang = khachHang.MaKhachHang,
                    HoTen = khachHang.HoTen,
                    CCCD = khachHang.CCCD,
                    SoDienThoai = khachHang.SoDienThoai,
                    Email = khachHang.Email,
                    DiaChi = khachHang.DiaChi
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật khách hàng: {ex.Message}");
            }
        }

        // Xóa khách hàng
        public async Task<bool> DeleteKhachHangAsync(int maKhachHang)
        {
            try
            {
                // Tìm khách hàng theo mã khách hàng
                var khachHang = await _context.khachHangs.FindAsync(maKhachHang);
                if (khachHang == null)
                {
                    return false; // Khách hàng không tồn tại
                }
                // Xóa khách hàng khỏi cơ sở dữ liệu
                _context.khachHangs.Remove(khachHang);
                await _context.SaveChangesAsync();
                return true; // Xóa thành công
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa khách hàng: {ex.Message}");
            }
        }

        // Lọc khách hàng theo tên hoặc CCCD , số điện thoại
        public async Task<IEnumerable<KhachHangResponseDto>> FilterKhachHangAsync(FilterKhachHangRequestDto filter)
        {
            // Tạo truy vấn cơ sở dữ liệu
            var query = _context.khachHangs.AsQueryable();
            // Lọc theo tên nếu có
            if (!string.IsNullOrEmpty(filter.TenKhachHang))
            {
                query = query.Where(kh => kh.HoTen!.Contains(filter.TenKhachHang));
            }
            // Lọc theo CCCD nếu có
            if (!string.IsNullOrEmpty(filter.CCCD))
            {
                query = query.Where(kh => kh.CCCD!.Contains(filter.CCCD));
            }
            // Lọc theo số điện thoại nếu có
            if (!string.IsNullOrEmpty(filter.SoDienThoai))
            {
                query = query.Where(kh => kh.SoDienThoai!.Contains(filter.SoDienThoai));
            }
            // Thực hiện truy vấn và lấy kết quả
            var khachHangs = await query.ToListAsync();
            // Chuyển sang DTO và trả về cho client
            return khachHangs.Select(kh => new KhachHangResponseDto
            {
                MaKhachHang = kh.MaKhachHang,
                HoTen = kh.HoTen,
                CCCD = kh.CCCD,
                SoDienThoai = kh.SoDienThoai,
                Email = kh.Email,
                DiaChi = kh.DiaChi
            });
        }
    }
}
