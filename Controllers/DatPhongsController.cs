using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementHotel.Services.IServices;
using Microsoft.AspNetCore.Authorization;

namespace ManagementHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatPhongsController : ControllerBase
    {
        private readonly IDatPhongService _datPhongService;
        public DatPhongsController(IDatPhongService datPhongService)
        {
            _datPhongService = datPhongService;
        }

        // Get : api/datphongs : Lấy tất cả đặt phòng
        [Authorize(Policy = "ActiveUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllDatPhong()
        {
            // Lấy tất cả đặt phòng
            var datPhongs = await _datPhongService.GetAllDatPhongsAsync();
            // Trả về kết quả
            return Ok(datPhongs);
        }

        // Get : api/datphongs/{maDatPhong} : Lấy đặt phòng theo mã đặt phòng
        [Authorize(Policy = "ActiveUser")]
        [HttpGet("{maDatPhong}")]
        public async Task<IActionResult> GetDatPhongById(int maDatPhong)
        {
            // Lấy đặt phòng theo mã
            var datPhong = await _datPhongService.GetDatPhongByIdAsync(maDatPhong);
            // Kiểm tra kết quả
            if (datPhong == null)
            {
                // Nếu không tìm thấy, trả về 404
                return NotFound();
            }
            // Trả về kết quả
            return Ok(datPhong);
        }

        // Post : api/datphongs : tạo đặt phòng mới
        [Authorize(Policy = "ActiveUser")]
        [HttpPost]
        public async Task<IActionResult> CreateDatPhong([FromBody] ManagementHotel.DTOs.DatPhong.CreateDatPhongRequestDto createDatPhongRequestDto)
        {
            try
            {
                // Tạo đặt phòng mới
                var createdDatPhong = await _datPhongService.CreateDatPhongAsync(createDatPhongRequestDto);
                // Trả về kết quả
                return CreatedAtAction(nameof(GetDatPhongById), new { maDatPhong = createdDatPhong.MaDatPhong }, createdDatPhong);
            }
            catch (Exception ex)
            {
                // Trả về lỗi
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
