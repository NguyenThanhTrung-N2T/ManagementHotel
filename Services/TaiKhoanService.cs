using ManagementHotel.DTOs.TaiKhoan;
using ManagementHotel.Models;
using ManagementHotel.Repositories;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ManagementHotel.Services
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;

        public TaiKhoanService(ITaiKhoanRepository taiKhoanRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
        }

        // lấy tất cả tài khoản 
        public async Task<IEnumerable<TaiKhoanResponseDto>> GetAllTaiKhoanAsync()
        {
            return await _taiKhoanRepository.GetAllTaiKhoanAsync();
        }

        // thêm tài khoản mới 
        public async Task<TaiKhoanResponseDto> AddTaiKhoanAsync(CreateTaiKhoanRequestDto taiKhoanNew)
        {
            try
            {
                // kiểm tra trùng tên đăng nhập 
                var isTenDangNhapExist = await _taiKhoanRepository.IsTenDangNhapExist(taiKhoanNew.TenDangNhap);
                if (isTenDangNhapExist)
                {
                    throw new Exception("Tên đăng nhập đã tồn tại !");
                }
                
                // thêm tài khoản 
                return await _taiKhoanRepository.AddTaiKhoanAsync(taiKhoanNew);
            }
            catch (Exception ex) 
            {
                throw new Exception("Lỗi khi thêm tài khoản mới !", ex);
            }
        }

        // lấy thông tin tài khoản qua mã tài khoản 
        public async Task<TaiKhoanResponseDto> GetTaiKhoanByIdAsync(int maTaiKhoan)
        {
            return await _taiKhoanRepository.GetTaiKhoanByIdAsync(maTaiKhoan);
        }

        // xóa tài khoản
        public async Task<bool> DeleteTaiKhoanAsync(int maTaiKhoan)
        {
            try
            {
                // Kiểm tra sự tồn tại của tài khoản 
                var existingTaiKhoan = await _taiKhoanRepository.GetTaiKhoanByIdAsync(maTaiKhoan);
                if (existingTaiKhoan == null)
                {
                    throw new Exception("Tài khoản không tồn tại.");
                }
                // Xóa tài khoản 
                return await _taiKhoanRepository.DeleteTaiKhoanAsync(maTaiKhoan);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                throw new Exception("Lỗi khi xóa tài khoản.", ex);
            }
        }
    }
}
