using ManagementHotel.DTOs.TaiKhoan;
using ManagementHotel.Models;
using ManagementHotel.Services;
using ManagementHotel.Services.IServices;
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

        public TaiKhoansController(ITaiKhoanService taiKhoanService)
        {
            _taiKhoanService = taiKhoanService;
        }

        // Get : api/taikhoans : lấy tất cả tài khoản
        [HttpGet]
        public async Task<IActionResult> GetAllTaiKhoan()
        {
            // lấy danh sách tài khoản 
            var taikhoans = await _taiKhoanService.GetAllTaiKhoanAsync();
            // trả về client 
            return Ok(taikhoans);
        }

        // Post : api/taikhoans : thêm tài khoản mới 
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
    }
}
