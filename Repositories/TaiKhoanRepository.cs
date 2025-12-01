using BCrypt.Net;
using ManagementHotel.Data;
using ManagementHotel.DTOs.Phong;
using ManagementHotel.DTOs.TaiKhoan;
using ManagementHotel.Models;
using ManagementHotel.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ManagementHotel.Repositories
{
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly ManagementHotelContext _context;
        public TaiKhoanRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // lấy tất cả tài khoản 
        public async Task<IEnumerable<TaiKhoanResponseDto>> GetAllTaiKhoanAsync()
        {
            // lấy danh sách tài khoản 
            var taikhoans = await _context.taiKhoans.ToListAsync();
            // trả về client
            return taikhoans.Select(tk => new TaiKhoanResponseDto
            {
                MaTaiKhoan = tk.MaTaiKhoan,
                TenDangNhap = tk.TenDangNhap,
                VaiTro = tk.VaiTro,
                TrangThai = tk.TrangThai,
            });
        }

        // Thêm tài khoản mới 
        public async Task<TaiKhoanResponseDto> AddTaiKhoanAsync(CreateTaiKhoanRequestDto taiKhoanRequestDto)
        {
            try
            {
                // hash password từ client ( 19 vòng hash )
                string hashPass = BCrypt.Net.BCrypt.HashPassword(taiKhoanRequestDto.MatKhau, workFactor: 12);
                // tạo tài khoản 
                var taiKhoanNew = new TaiKhoan
                {
                    TenDangNhap = taiKhoanRequestDto.TenDangNhap,
                    VaiTro = taiKhoanRequestDto.VaiTro,
                    MatKhau = hashPass,
                    TrangThai = taiKhoanRequestDto.TrangThai,
                    MaNhanVien = taiKhoanRequestDto.MaNhanVien,
                };
                // thêm vào db và lưu 
                _context.taiKhoans.Add(taiKhoanNew);
                await _context.SaveChangesAsync();
                // trả về client
                return new TaiKhoanResponseDto
                {
                    MaTaiKhoan = taiKhoanNew.MaTaiKhoan,
                    TenDangNhap = taiKhoanNew.TenDangNhap,
                    VaiTro = taiKhoanNew.VaiTro,
                    TrangThai = taiKhoanNew.TrangThai,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm tài khoản: " + ex.Message);
            }
        }

        // Kiểm tra trùng tên đang nhập 
        public async Task<bool> IsTenDangNhapExist(string? tenDangNhap)
        {
            // kiểm tra tên đăng nhập trong db
            return await _context.taiKhoans.AnyAsync(tk => tk.TenDangNhap == tenDangNhap);
        }

        // kiểm tra tài khoản còn hoạt động
        public async Task<bool> IsTaiKhoanActive(string? tenDangNhap)
        {
            // lấy tài khoản từ db
            var taiKhoan = await _context.taiKhoans.FirstOrDefaultAsync(tk => tk.TenDangNhap == tenDangNhap);
            // kiểm tra trạng thái 
            if (taiKhoan != null && taiKhoan.TrangThai == "Hoạt động")
            {
                return true;
            }
            return false;
        }

        // lấy tài khoản theo mã tài khoản 
        public async Task<TaiKhoanResponseDto> GetTaiKhoanByIdAsync(int maTaiKhoan)
        {
            // lấy tài khoản từ db
            var taiKhoan = await _context.taiKhoans.FindAsync(maTaiKhoan);

            // nếu không có 
            if (taiKhoan == null)
            {
                return null!;
            }

            // trả về client 
            return new TaiKhoanResponseDto
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                TenDangNhap = taiKhoan.TenDangNhap,
                VaiTro = taiKhoan.VaiTro,
                TrangThai = taiKhoan.TrangThai
            };
        }

        // lấy tài khoản theo tên đăng nhập
        public async Task<TaiKhoanResponseDto> GetTaiKhoanByTenDangNhapAsync(string? tenDangNhap)
        {
            // lấy tài khoản từ db
            var taiKhoan = await _context.taiKhoans.FirstOrDefaultAsync(tk => tk.TenDangNhap == tenDangNhap);
            // nếu không có 
            if (taiKhoan == null)
            {
                return null!;
            }
            // trả về client 
            return new TaiKhoanResponseDto
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                TenDangNhap = taiKhoan.TenDangNhap,
                VaiTro = taiKhoan.VaiTro,
                TrangThai = taiKhoan.TrangThai
            };
        }

        // xóa tài khoản 
        public async Task<bool> DeleteTaiKhoanAsync(int maTaiKhoan)
        {
            try
            {
                // Tìm tài khoản theo mã tài khoản
                var taikhoan = await _context.taiKhoans.FindAsync(maTaiKhoan);
                if (taikhoan == null)
                {
                    return false; // tài khoản không tồn tại
                }
                // Xóa tài khoản khỏi cơ sở dữ liệu
                _context.taiKhoans.Remove(taikhoan);
                await _context.SaveChangesAsync();
                return true; // Xóa thành công
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa tài khoản: {ex.Message}");
            }
        }

        // lấy tài khoản theo tên đăng nhập
        public async Task<bool> IsExistTaiKhoan(string? tenDangNhap)
        {
            return await _context.taiKhoans.AnyAsync(tk => tk.TenDangNhap == tenDangNhap);
        }

        // đăng nhập tài khoản 
        public async Task<bool> LoginTaiKhoanAsync(LoginTaiKhoanRequestDto requestDto)
        {
            // lấy tài khoản trong db
            var taikhoan = await _context.taiKhoans.FirstOrDefaultAsync(tk => tk.TenDangNhap == requestDto.TenDangNhap);

            // check mật khẩu 
            bool checkMK = BCrypt.Net.BCrypt.Verify(requestDto.MatKhau, taikhoan?.MatKhau);

            if (checkMK)
            {
                return true;
            }
            return false;
        }
    }
}
