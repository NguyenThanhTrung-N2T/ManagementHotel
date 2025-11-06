using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementHotel.Services.IServices;
using ManagementHotel.DTOs.KhachHang;

namespace ManagementHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangsController : ControllerBase
    {
        private readonly IKhachHangService _khachHangService;
        public KhachHangsController(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }

        // Get : api/khachhangs : Lấy tất cả khách hàng
        [HttpGet]
        public async Task<IActionResult> GetAllKhachHang()
        {
            // Lấy tất cả khách hàng
            var khachHangs = await _khachHangService.GetAllKhachHangAsync();
            // Trả về kết quả
            return Ok(khachHangs);
        }

        // Get : api/khachhangs/{maKhachHang} : Lấy khách hàng theo mã khách hàng
        [HttpGet("{maKhachHang}")]
        public async Task<IActionResult> GetKhachHangById(int maKhachHang)
        {
            // Lấy khách hàng theo mã khách hàng
            var khachHang = await _khachHangService.GetKhachHangByIdAsync(maKhachHang);
            // Nếu không tìm thấy khách hàng, trả về 404
            if (khachHang == null)
            {
                return NotFound();
            }
            // Trả về kết quả
            return Ok(khachHang);
        }

        // Post : api/khachhangs : Thêm khách hàng mới
        [HttpPost]
        public async Task<IActionResult> AddKhachHang([FromBody] CreateKhachHangRequestDto khachHang)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Thêm khách hàng mới
            var createdKhachHang = await _khachHangService.AddKhachHangAsync(khachHang);
            // Trả về kết quả
            return CreatedAtAction(nameof(GetKhachHangById), new { maKhachHang = createdKhachHang.MaKhachHang }, createdKhachHang);
        }

        // Put : api/khachhangs/{maKhachHang} : Cập nhật khách hàng
        [HttpPut("{maKhachHang}")]
        public async Task<IActionResult> UpdateKhachHang(int maKhachHang, [FromBody] UpdateKhachHangRequest khachHang)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Cập nhật khách hàng
            var updatedKhachHang = await _khachHangService.UpdateKhachHangAsync(maKhachHang, khachHang);
            // Trả về kết quả
            return Ok(updatedKhachHang);
        }

        // Delete : api/khachhangs/{maKhachHang} : Xóa khách hàng
        [HttpDelete("{maKhachHang}")]
        public async Task<IActionResult> DeleteKhachHang(int maKhachHang)
        {
            // Xóa khách hàng
            var result = await _khachHangService.DeleteKhachHangAsync(maKhachHang);
            // Nếu không tìm thấy khách hàng, trả về 404
            if (!result)
            {
                return NotFound();
            }
            // Trả về kết quả
            return NoContent();
        }

        // Get : api/khachhangs/filter : Lọc khách hàng theo tên hoặc CCCD , số điện thoại
        [HttpGet("filter")]
        public async Task<IActionResult> FilterKhachHang([FromQuery] FilterKhachHangRequestDto filter)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Lọc khách hàng
            var khachHangs = await _khachHangService.FilterKhachHangAsync(filter);
            // Trả về kết quả
            return Ok(khachHangs);
        }
    }
}
