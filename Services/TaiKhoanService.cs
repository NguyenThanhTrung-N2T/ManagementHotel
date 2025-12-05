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

                // kiểm tra nhân viên đã có tài khoản chưa
                var isNhanVienHasTaiKhoan = await _taiKhoanRepository.IsNhanVienHasTaiKhoan(taiKhoanNew.MaNhanVien);
                if (isNhanVienHasTaiKhoan)
                {
                    throw new Exception("Nhân viên đã có tài khoản !");
                }

                // kiểm tra vai trò của tài khoản có khớp với chức vụ nhân viên không
                var isVaiTroValid = await _taiKhoanRepository.IsVaiTroValidForNhanVien(taiKhoanNew.VaiTro, taiKhoanNew.MaNhanVien);
                if (!isVaiTroValid)
                {
                    throw new Exception("Vai trò của tài khoản không khớp với chức vụ nhân viên !");
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

        // lấy tài khoản theo tên đăng nhập 
        public async Task<TaiKhoanResponseDto> GetTaiKhoanByTenDangNhapAsync(string tenDangNhap)
        {
            return await _taiKhoanRepository.GetTaiKhoanByTenDangNhapAsync(tenDangNhap);
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

        // kiểm tra tài khoản 
        public async Task<bool> LoginTaiKhoanAsync(LoginTaiKhoanRequestDto loginTaiKhoanRequestDto)
        {
            // check tồn tại tài khoản 
            bool isExistTK = await _taiKhoanRepository.IsTenDangNhapExist(loginTaiKhoanRequestDto.TenDangNhap);
            if (!isExistTK)
            {
                throw new Exception("Tài khoản không tồn tại.");
            }
            // kiểm tra tài khoản có hoạt động không
            bool isActiveTK = await _taiKhoanRepository.IsTaiKhoanActive(loginTaiKhoanRequestDto.TenDangNhap);
            if (!isActiveTK)
            {
                throw new Exception("Tài khoản không còn hoạt động.");
            }

            // đăng nhập tài khoản 
            bool isLogin = await _taiKhoanRepository.LoginTaiKhoanAsync(loginTaiKhoanRequestDto);
            if (isLogin)
            {
                return true;
            }
            return false;
        }

        // reset mật khẩu tài khoản
        public async Task<bool> ResetMatKhauTaiKhoanAsync(ResetPasswordRequestDto requestDto)
        {

            // kiểm tra tồn tại tài khoản
            var existingTaiKhoan = await _taiKhoanRepository.IsExistTaiKhoan(requestDto.TenDangNhap);
            if (!existingTaiKhoan)
            {
                throw new Exception("Tài khoản không tồn tại.");
            }
            var updateResult = await _taiKhoanRepository.ResetPasswordAsync(requestDto);
            return updateResult;

        }
    }
}
