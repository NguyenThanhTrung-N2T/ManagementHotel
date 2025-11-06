using Microsoft.AspNetCore.Mvc;
using ManagementHotel.Services.IServices;
using ManagementHotel.DTOs.NhanVien;
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
        [HttpGet]
        public async Task<IActionResult> GetAllNhanVien()
        {
            // lấy tất cả nhân viên
            var nhanviens = await _nhanVienService.GetAllNhanVienAsync();
            // trả về kết quả cho client
            return Ok(nhanviens);
        }

        // Get : api/nhanviens/{maNhanVien}
        [HttpGet("{maNhanVien}")]
        public async Task<IActionResult> GetNhanVienByIdAsync(int maNhanVien)
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


    }
}