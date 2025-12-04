using ManagementHotel.Data;
using ManagementHotel.DTOs.DichVu;
using ManagementHotel.Models;
using ManagementHotel.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Collections.Immutable;

namespace ManagementHotel.Repositories
{
    public class DichVuRepository : IDichVuRepository
    {
        private readonly ManagementHotelContext _context;

        public DichVuRepository(ManagementHotelContext context)
        {
            _context = context;
        }

        // lấy tất cả dịch vụ 
        public async Task<IEnumerable<DichVuResponseDto>> GetAllDichVuAsync()
        {
            var dichvu = await _context.dichVus.ToListAsync();

            return dichvu.Select(x => new DichVuResponseDto
            {
                TenDichVu = x.TenDichVu,
                MaDichVu = x.MaDichVu,
                DonGia = x.DonGia,
                TrangThai = x.TrangThai,
                DonVi = x.DonVi,
            });
        }

        // lấy dịch vụ theo mã dịch vụ
        public async Task<DichVuResponseDto?> GetDichVuByIdAsync(int maDichVu)
        {
            // lấy dịch vuj trong db
            var dichvu = await _context.dichVus.FindAsync(maDichVu);
            // neu ko co
            if (dichvu == null)
            {
                return null;
            }
            // trả về client
            return new DichVuResponseDto
            {
                MaDichVu = dichvu.MaDichVu,
                DonGia = dichvu.DonGia,
                TrangThai = dichvu.TrangThai,
                DonVi = dichvu.DonVi,
                TenDichVu = dichvu.TenDichVu
            };
        }

        // them dich vu 
        public async Task<DichVuResponseDto> AddDichVuAsync(CreateDichVuRequestDto requestDto)
        {
            try
            {
                // tạo dịch vụ mới 
                var dichVuNew = new DichVu
                {
                    TenDichVu = requestDto.TenDichVu,
                    DonGia = requestDto.DonGia,
                    TrangThai = requestDto.TrangThai,
                    DonVi = requestDto.DonVi,
                };
                // them vao db
                _context.dichVus.Add(dichVuNew);
                await _context.SaveChangesAsync();
                // tra ve client
                return new DichVuResponseDto
                {
                    MaDichVu = dichVuNew.MaDichVu,
                    TenDichVu = dichVuNew.TenDichVu,
                    DonGia = dichVuNew.DonGia,
                    TrangThai = dichVuNew.TrangThai,
                    DonVi = dichVuNew.DonVi,
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm dịch vụ : {ex.Message}");
            }
        }

        // cập nhật thông tin dịch vu 
        public async Task<DichVuResponseDto> UpdateDichVuAsync(int maDichVu, UpdateDichVuRequestDto updateDto)
        {
            try
            {
                // Tìm dịch vụ theo mã dịch vụ
                var dichvu = await _context.dichVus.FindAsync(maDichVu);
                if (dichvu == null)
                {
                    throw new Exception("Dịch vụ không tồn tại.");
                }
                // Cập nhật thông tin dịch vụ
                dichvu.TenDichVu = updateDto.TenDichVu;
                dichvu.DonVi = updateDto.DonVi;
                dichvu.TrangThai = updateDto.TrangThai;
                dichvu.DonGia = updateDto.DonGia;
                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
                // Trả về DTO của dich vu đã cập nhật cho client
                return new DichVuResponseDto
                {
                    MaDichVu = dichvu.MaDichVu,
                    TenDichVu = dichvu.TenDichVu,
                    TrangThai = dichvu.TrangThai,
                    DonGia = dichvu.DonGia,
                    DonVi = dichvu.DonVi,
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật dịch vụ : {ex.Message}");
            }
        }

        // Xóa dịch vụ
        public async Task<bool> DeleteDichVuAsync(int maDichVu)
        {
            try
            {
                // Tìm dịch vụ theo mã dịch vụ
                var DichVu = await _context.dichVus.FindAsync(maDichVu);
                if (DichVu == null)
                {
                    return false; // dich vu không tồn tại
                }
                // Xóa dich vu khỏi cơ sở dữ liệu
                _context.dichVus.Remove(DichVu);
                await _context.SaveChangesAsync();
                return true; // Xóa thành công
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa dịch vụ: {ex.Message}");
            }
        }

        // lọc dịch vụ theo tên , đơn giá , đơn vị
        public async Task<IEnumerable<DichVuResponseDto>> FilterDichVuAsync(FilterDichVuRequestDto filterDto)
        {
            // tạo truy vấn 
            var dichVus = _context.dichVus.AsQueryable();
            // lọc theo tên 
            if (!string.IsNullOrEmpty(filterDto.TenDichVu))
            {
                dichVus = dichVus.Where(dv => dv.TenDichVu!.Contains(filterDto.TenDichVu));
            }
            // lọc theo đơn vị
            if (!string.IsNullOrEmpty(filterDto.DonVi))
            {
                dichVus = dichVus.Where(dv => dv.DonVi!.Contains(filterDto.DonVi));
            }
            // lọc theo đơn giá , đơn giá nhỏ hơn filter
            if (filterDto.DonGia.HasValue)
            {
                dichVus = dichVus.Where(dv => dv.DonGia < filterDto.DonGia);
            }

            // lọc theo trạng thái
            if (!string.IsNullOrEmpty(filterDto.TrangThai))
            {
                dichVus = dichVus.Where(dv => dv.TrangThai!.Contains(filterDto.TrangThai));
            }
            // lấy danh sách dịch vụ 
            var result = await dichVus.ToListAsync();
            // tra ve dto cho client
            return result.Select(dv => new DichVuResponseDto
            {
                MaDichVu = dv.MaDichVu,
                TenDichVu = dv.TenDichVu,
                DonVi = dv.DonVi,
                TrangThai = dv.TrangThai,
                DonGia = dv.DonGia,
            });
        }

        // kiểm tra dịch vụ tồn tại theo tên dịch vụ
        public async Task<bool> IsDichVuNameExistsAsync(string? tenDichVu)
        {
            return await _context.dichVus.AnyAsync(dv => dv.TenDichVu == tenDichVu);
        }
    }
}
