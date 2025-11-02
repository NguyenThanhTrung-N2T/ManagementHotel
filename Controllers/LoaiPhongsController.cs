using ManagementHotel.DTOs;
using ManagementHotel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiPhongsController : ControllerBase
    {
        private readonly ILoaiPhongService _loaiPhongService;
        public LoaiPhongsController(ILoaiPhongService loaiPhongService)
        {
            _loaiPhongService = loaiPhongService;
        }

        // Get : api/LoaiPhong : Lấy tất cả loại phòng
        [HttpGet]
        public async Task<IActionResult> GetAllLoaiPhong()
        {
            var loaiPhongs = await _loaiPhongService.GetAllLoaiPhongAsync();
            return Ok(loaiPhongs);
        }

        // Post : api/LoaiPhong : Thêm loại phòng mới
        [HttpPost]
        public async Task<IActionResult> AddLoaiPhong([FromBody] CreateLoaiPhongRequestDto loaiPhong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdLoaiPhong = await _loaiPhongService.AddLoaiPhongAsync(loaiPhong);
            return CreatedAtAction(nameof(GetAllLoaiPhong), new { id = createdLoaiPhong.MaLoaiPhong }, createdLoaiPhong);
        }

    }
}
