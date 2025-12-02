using ManagementHotel.DTOs.NhanVien;
using ManagementHotel.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace ManagementHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanViensController : ControllerBase
    {
        private readonly INhanVienService _nhanVienService;

        public NhanViensController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        // Get : api/nhanviens 
        [Authorize(Policy = "ActiveUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllNhanVien()
        {
            // lấy tất cả nhân viên
            var nhanviens = await _nhanVienService.GetAllNhanVienAsync();
            // trả về kết quả cho client
            return Ok(nhanviens);
        }

        // Get : api/nhanviens/{maNhanVien}
        [Authorize(Policy = "ActiveUser")]
        [HttpGet("{maNhanVien}")]
        public async Task<IActionResult> GetNhanVienById(int maNhanVien)
        {
            // lấy nhân viên theo mã nhân viên 
            var nhanvien = await _nhanVienService.GetNhanVienByIdAsync(maNhanVien);
            // nếu không có nhân viên
            if (nhanvien == null)
            {
                return NotFound();
            }
            // trả về cho client
            return Ok(nhanvien);
        }

        // Post : api/nhanviens
        [Authorize(Policy = "AdminActive")]
        [HttpPost]
        public async Task<IActionResult> CreateNhanVien(CreateNhanVienRequestDto nhanviendto) 
        {
            // Kiểm tra thông tin dto hợp lệ
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Thêm nhân viên mới 
            var nhanVienNew = await _nhanVienService.AddNhanVienAsync(nhanviendto);
            // Trả về cho client 201 Created
            return CreatedAtAction(nameof(GetNhanVienById), new { maNhanVien = nhanVienNew.MaNhanVien }, nhanVienNew);
        }


        // Put : api/nhanviens/{maNhanVien}
        [Authorize(Policy = "AdminActive")]
        [HttpPut("{maNhanVien}")]
        public async Task<IActionResult> UpdateNhanVien(int maNhanVien, [FromBody] UpdateNhanVienRequestDto updateNhanVien)
        {
            // kiểm tra thông tin đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // cập nhật thông tin 
            var nhanVienUpdate = await _nhanVienService.UpdateNhanVienAsync(maNhanVien, updateNhanVien);
            // trả về client
            return Ok(nhanVienUpdate);

        }

        // Delete : api/nhanviens/{maNhanVien}
        [Authorize(Policy = "AdminActive")]
        [HttpDelete("{maNhanVien}")]
        public async Task<IActionResult> DeleteNhanVien(int maNhanVien)
        {
            // xóa nhân viên 
            var response = await _nhanVienService.DeleteNhanVienAsync(maNhanVien);
            // kiểm tra kết quả
            if (response)
            {
                // trả về kết quả
                return NoContent();
            }
            // nếu không , trả về 404
            return NotFound();
        }

        // Get : api/nhanviens/filter
        [Authorize(Policy = "ActiveUser")]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterNhanVien([FromQuery] FilterNhanVienRequestDto filter)
        {
            // lọc lấy nhân viên 
            var nhanviens = await _nhanVienService.FilterNhanVienAsync(filter);
            // trả về client
            return Ok(nhanviens);
        }

    }
}