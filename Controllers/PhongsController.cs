using ManagementHotel.DTOs.Phong;
using ManagementHotel.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongsController : ControllerBase
    {
        private readonly IPhongService _phongService;
        public PhongsController(IPhongService phongService)
        {
            _phongService = phongService;
        }

        // Get : api/phongs : Lấy tất cả phòng
        [HttpGet]
        public async Task<IActionResult> GetAllPhong()
        {
            // Lấy tất cả phòng
            var phongs = await _phongService.GetAllPhongAsync();
            // Trả về kết quả
            return Ok(phongs);
        }

        // Get : api/phongs/{maPhong} : Lấy phòng theo mã phòng
        [Authorize(Policy = "ActiveUser")]
        [HttpGet("{maPhong}")]
        public async Task<IActionResult> GetPhongById(int maPhong)
        {
            // Lấy phòng theo mã
            var phong = await _phongService.GetPhongByIdAsync(maPhong);
            // Kiểm tra kết quả
            if (phong == null)
            {
                // Nếu không tìm thấy, trả về 404
                return NotFound();
            }
            // Trả về kết quả
            return Ok(phong);
        }

        // Post : api/phongs : Thêm phòng mới
        [Authorize(Policy = "AdminActive")]
        [HttpPost]
        public async Task<IActionResult> AddPhong([FromBody] CreatePhongRequestDto phong)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Thêm phòng mới
            var newPhong = await _phongService.AddPhongAsync(phong);
            // Trả về kết quả
            return CreatedAtAction(nameof(GetPhongById), new { maPhong = newPhong.MaPhong }, newPhong);
        }

        // Put : api/phongs/{maPhong} : Cập nhật phòng
        [Authorize(Policy = "ActiveUser")]
        [HttpPut("{maPhong}")]
        public async Task<IActionResult> UpdatePhong(int maPhong, [FromBody] UpdatePhongRequestDto phong)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Cập nhật phòng
            var updatedPhong = await _phongService.UpdatePhongAsync(maPhong, phong);
            // Trả về kết quả
            return Ok(updatedPhong);
        }

        // Delete : api/phongs/{maPhong} : Xóa phòng
        [Authorize(Policy = "AdminActive")]
        [HttpDelete("{maPhong}")]
        public async Task<IActionResult> DeletePhong(int maPhong)
        {
            // Xóa phòng
            var result = await _phongService.DeletePhongAsync(maPhong);
            // Kiểm tra kết quả
            if (!result)
            {
                // Nếu không tìm thấy, trả về 404
                return NotFound();
            }
            // Trả về kết quả
            return NoContent();
        }

        // Get : api/phongs/filter : Lọc phòng theo trạng thái
        [HttpGet("filter")]
        public async Task<IActionResult> FilterPhongByStatus([FromQuery] FilterPhongRequest filter)
        {
            // Lọc phòng theo trạng thái
            var phongs = await _phongService.FilterPhongByStatusAsync(filter);
            // Trả về kết quả
            return Ok(phongs);
        }
    }
}
