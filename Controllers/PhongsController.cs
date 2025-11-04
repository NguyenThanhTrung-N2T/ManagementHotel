using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementHotel.Services;
using ManagementHotel.DTOs.Phong;

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
            var phongs = await _phongService.GetAllPhongAsync();
            return Ok(phongs);
        }

        // Get : api/phongs/{maPhong} : Lấy phòng theo mã phòng
        [HttpGet("{maPhong}")]
        public async Task<IActionResult> GetPhongById(int maPhong)
        {
            var phong = await _phongService.GetPhongByIdAsync(maPhong);
            if (phong == null)
            {
                return NotFound();
            }
            return Ok(phong);
        }

        // Post : api/phongs : Thêm phòng mới
        [HttpPost]
        public async Task<IActionResult> AddPhong([FromBody] CreatePhongRequestDto phong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newPhong = await _phongService.AddPhongAsync(phong);
            return CreatedAtAction(nameof(GetPhongById), new { maPhong = newPhong.MaPhong }, newPhong);
        }
    }
}
