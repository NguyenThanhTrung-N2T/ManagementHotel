using ManagementHotel.DTOs.TaiKhoan;
using ManagementHotel.Helpers;
using ManagementHotel.Models;
using ManagementHotel.Services;
using ManagementHotel.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ManagementHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoansController : ControllerBase
    {
        private readonly ITaiKhoanService _taiKhoanService;
        private readonly JwtTokenService _jwtTokenService;

        public TaiKhoansController(ITaiKhoanService taiKhoanService, JwtTokenService jwtTokenService)
        {
            _taiKhoanService = taiKhoanService;
            _jwtTokenService = jwtTokenService;
        }

        // Get : api/taikhoans : lấy tất cả tài khoản
        [Authorize(Policy = "ActiveUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllTaiKhoan()
        {
            // lấy danh sách tài khoản 
            var taikhoans = await _taiKhoanService.GetAllTaiKhoanAsync();
            // trả về client 
            return Ok(taikhoans);
        }

        // Post : api/taikhoans : thêm tài khoản mới 
        [Authorize(Policy = "AdminActive")]
        [HttpPost]
        public async Task<IActionResult> AddTaiKhoan([FromBody]CreateTaiKhoanRequestDto taiKhoanRequestDto)
        {
            // kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // thêm tài khoản
            var taiKhoanNew = await _taiKhoanService.AddTaiKhoanAsync(taiKhoanRequestDto);
            // trả về client
            return CreatedAtAction(nameof(GetTaiKhoanById), new { maTaiKhoan = taiKhoanNew.MaTaiKhoan }, taiKhoanNew);
        }

        // Get : api/taikhoans/{maTaiKhoan} : lấy tài khoản qua mã tài khoản
        [Authorize(Policy = "ActiveUser")]
        [HttpGet("{maTaiKhoan}")]
        public async Task<IActionResult> GetTaiKhoanById(int maTaiKhoan)
        {
            // lấy tài khoản trong db
            var taikhoan = await _taiKhoanService.GetTaiKhoanByIdAsync(maTaiKhoan);

            // kiểm tra kết quả
            if(taikhoan == null)
            {
                return NotFound(); // trả về 404
            }

            // trả về client 
            return Ok(taikhoan);
        }

        // Delete : api/taikhoans/{maTaiKhoan} : Xóa tài khoản
        [Authorize(Policy = "AdminActive")]
        [HttpDelete("{maTaiKhoan}")]
        public async Task<IActionResult> DeleteTaiKhoan(int maTaiKhoan)
        {
            // Xóa tài khoản
            var result = await _taiKhoanService.DeleteTaiKhoanAsync(maTaiKhoan);
            // Nếu không tìm thấy tài khoản, trả về 404
            if (!result)
            {
                return NotFound();
            }
            // Trả về kết quả
            return NoContent();
        }

        // Post : api/taikhoans/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginTaiKhoan([FromBody] LoginTaiKhoanRequestDto loginTaiKhoanRequestDto)
        {
            // kiểm tra tài khoản 
            var result = await _taiKhoanService.LoginTaiKhoanAsync(loginTaiKhoanRequestDto);
            if (!result) {
                return NotFound("Mật khẩu tài khoản sai.");
            }
            var taiKhoan = await _taiKhoanService.GetTaiKhoanByTenDangNhapAsync(loginTaiKhoanRequestDto.TenDangNhap!);
            if (taiKhoan == null)
            {
                return NotFound("Tài khoản không tồn tại.");
            }
            // tạo token
            var token = _jwtTokenService.GenerateToken(maTaiKhoan: taiKhoan.MaTaiKhoan.ToString(),
                                                       tenDangNhap: taiKhoan.TenDangNhap!,
                                                        vaiTro: taiKhoan.VaiTro!,
                                                        trangThai: taiKhoan.TrangThai!);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(8)
            };
            Response.Cookies.Append("AccessToken", token, cookieOptions);

            // trả về client
            return Ok(new
            {
                message = "Đăng nhập thành công",
                role = taiKhoan.VaiTro
            });

        }
    }
}
