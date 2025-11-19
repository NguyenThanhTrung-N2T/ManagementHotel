using ManagementHotel.Controllers;
using ManagementHotel.Data;
using ManagementHotel.DTOs.KhachHang;
using ManagementHotel.DTOs.NhanVien;
using ManagementHotel.Models;
using ManagementHotel.Repositories.IRepositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace ManagementHotel.Repositories
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly ManagementHotelContext _context;
        public NhanVienRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // Lấy tất cả nhân viên
        public async Task<IEnumerable<NhanVienResponseDto>> GetAllNhanVienAsync()
        {
            // lấy danh sách nhân viên từ cơ sở dữ liệu
            var nhanViens = await _context.nhanViens.ToListAsync();
            // chuyển đổi danh sách nhân viên sang DTO
            return nhanViens.Select(nv => new NhanVienResponseDto
            {
                MaNhanVien = nv.MaNhanVien,
                HoTen = nv.HoTen,
                CCCD = nv.CCCD,
                SoDienThoai = nv.SoDienThoai,
                Email = nv.Email,
                DiaChi = nv.DiaChi,
                NgaySinh = nv.NgaySinh,
                ChucVu = nv.ChucVu,
                Luong = nv.Luong
            });
        }

        // lấy nhân viên qua mã nhân viên
        public async Task<NhanVienResponseDto> GetNhanVienByIdAsync(int maNhanVien)
        {
            // tìm nhân viên theo mã nhân viên
            var nhanVien = await _context.nhanViens.FindAsync(maNhanVien);
            if(nhanVien != null)
            {
                return new NhanVienResponseDto
                {
                    MaNhanVien = nhanVien.MaNhanVien,
                    HoTen = nhanVien.HoTen,
                    NgaySinh = nhanVien.NgaySinh,
                    CCCD = nhanVien.CCCD,
                    SoDienThoai = nhanVien.SoDienThoai,
                    Email = nhanVien.Email,
                    DiaChi = nhanVien.DiaChi,
                    Luong = nhanVien.Luong,
                    ChucVu = nhanVien.ChucVu,
                };
            }
            return null!;
        }

        // tạo nhân viên mới 
        public async Task<NhanVienResponseDto> AddNhanVienAsync(CreateNhanVienRequestDto nhanVienRequestDto)
        {
            try
            {
                // tạo nhân viên mới 
                var nhanVienNew = new NhanVien
                {
                    HoTen = nhanVienRequestDto.HoTen,
                    SoDienThoai = nhanVienRequestDto.SoDienThoai,
                    DiaChi = nhanVienRequestDto.DiaChi,
                    Email = nhanVienRequestDto.Email,
                    CCCD = nhanVienRequestDto.CCCD,
                    ChucVu = nhanVienRequestDto.ChucVu,
                    Luong = nhanVienRequestDto.Luong,
                    NgaySinh = nhanVienRequestDto.NgaySinh
                };
                // thêm vào db
                _context.Add(nhanVienNew);
                await _context.SaveChangesAsync();

                // trả về cho client 
                return new NhanVienResponseDto
                {
                    MaNhanVien = nhanVienNew.MaNhanVien,
                    CCCD = nhanVienNew.CCCD,
                    ChucVu = nhanVienNew.ChucVu,
                    DiaChi = nhanVienNew.DiaChi,
                    Email = nhanVienNew.Email,
                    SoDienThoai = nhanVienNew.SoDienThoai,
                    NgaySinh = nhanVienNew.NgaySinh,
                    Luong = nhanVienNew.Luong,
                    HoTen = nhanVienNew.HoTen,
                };

            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm nhân viên: {ex.Message}");
            }
        }

        // kiểm tra tồn tại CCCD 
        public async Task<bool> IsExistCCCDAsync(string? CCCD)
        {
            return await _context.nhanViens.AnyAsync(x => x.CCCD == CCCD);
        }

        // kiểm tra tồn tại số điện thoại 
        public async Task<bool> IsExistSoDienThoaiAsync(string? soDienThoai)
        {
            return await _context.nhanViens.AnyAsync(x => x.SoDienThoai == soDienThoai);
        }

        // cập nhật thông tin của nhân viên
        public async Task<NhanVienResponseDto> UpdateNhanVienAsync(int maNhanVien, UpdateNhanVienRequestDto update_nhanvien)
        {
            try
            {
                // lấy nhân viên trong database
                var nhanvien_exist = await _context.nhanViens.FindAsync(maNhanVien);
                // kiểm tra tồn tại của nhân viên
                if (nhanvien_exist == null) {
                    throw new Exception("Nhân viên không tồn tại.");
                }
                // cập nhật thông tin
                nhanvien_exist.HoTen = update_nhanvien.HoTen;
                nhanvien_exist.DiaChi = update_nhanvien.DiaChi;
                nhanvien_exist.ChucVu = update_nhanvien.ChucVu;
                nhanvien_exist.SoDienThoai = update_nhanvien.SoDienThoai;
                nhanvien_exist.Email = update_nhanvien.Email;
                nhanvien_exist.Luong = update_nhanvien.Luong;
                nhanvien_exist.NgaySinh = update_nhanvien.NgaySinh;
                // lưu thông tin vào db
                await _context.SaveChangesAsync();
                // trả về cho client
                return new NhanVienResponseDto
                {
                    MaNhanVien = nhanvien_exist.MaNhanVien,
                    CCCD = nhanvien_exist.CCCD,
                    ChucVu = nhanvien_exist.ChucVu,
                    DiaChi = nhanvien_exist.DiaChi,
                    Email = nhanvien_exist.Email,
                    SoDienThoai = nhanvien_exist.SoDienThoai,
                    NgaySinh = nhanvien_exist.NgaySinh,
                    Luong = nhanvien_exist.Luong,
                    HoTen = nhanvien_exist.HoTen,
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật nhân viên: {ex.Message}");
            }
        }

        // xóa nhân viên qua mã nhân viên 
        public async Task<bool> DeleteNhanVienAsync(int maNhanVien)
        {
            try
            {
                // kiểm tra nhân viên có tồn tại
                var nhanvien = await _context.nhanViens.FindAsync(maNhanVien);
                if(nhanvien == null)
                {
                    return false;
                }

                // xóa nhân viên trong db
                _context.nhanViens.Remove(nhanvien);
                await _context.SaveChangesAsync();

                // trả về true khi xóa thành công
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa phòng: " + ex.Message);
            }
        }

        // lọc nhân viên theo thuộc tính
        public async Task<IEnumerable<NhanVienResponseDto>> FilterNhanVienAsync(FilterNhanVienRequestDto filter)
        {
            // tạo truy vấn 
            var query = _context.nhanViens.AsQueryable();

            // lọc theo họ tên nhân viên nếu có 
            if (!string.IsNullOrEmpty(filter.TenNhanVien))
            {
                query = query.Where(lp => lp.HoTen!.Contains(filter.TenNhanVien));
            }
            // lọc theo CCCD nhân viên
            if (!string.IsNullOrEmpty(filter.CCCD))
            {
                query = query.Where(lp => lp.CCCD!.Contains(filter.CCCD));
            }
            // lọc nhân viên theo số điện thoại 
            if (!string.IsNullOrEmpty(filter.SoDienThoai))
            {
                query = query.Where(lp => lp.SoDienThoai!.Contains(filter.SoDienThoai));
            }
            // lọc theo chức vụ 
            if (!string.IsNullOrEmpty(filter.ChucVu))
            {
                query = query.Where(lp => lp.ChucVu!.Contains(filter.ChucVu));
            }

            // Thực hiện truy vấn và lấy kết quả
            var nhanViens = await query.ToListAsync();
            // Chuyển sang DTO và trả về cho client
            return nhanViens.Select(nv => new NhanVienResponseDto
            {
                MaNhanVien = nv.MaNhanVien,
                HoTen = nv.HoTen,
                NgaySinh = nv.NgaySinh,
                CCCD = nv.CCCD,
                SoDienThoai = nv.SoDienThoai,
                Email = nv.Email,
                DiaChi = nv.DiaChi,
                Luong = nv.Luong,
                ChucVu = nv.ChucVu,
            });
        }
    }
}
