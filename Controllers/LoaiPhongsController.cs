using ManagementHotel.DTOs;
using ManagementHotel.DTOs.LoaiPhong;
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

        // Get : api/loaiphongs : Lấy tất cả loại phòng
        [HttpGet]
        public async Task<IActionResult> GetAllLoaiPhong()
        {
            var loaiPhongs = await _loaiPhongService.GetAllLoaiPhongAsync();
            return Ok(loaiPhongs);
        }

        // Post : api/loaiphongs : Thêm loại phòng mới
        [HttpPost]
        public async Task<IActionResult> AddLoaiPhong([FromBody] CreateLoaiPhongRequestDto loaiPhong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdLoaiPhong = await _loaiPhongService.AddLoaiPhongAsync(loaiPhong);
            return CreatedAtAction(nameof(GetLoaiPhongById),new { maloaiphong = createdLoaiPhong.MaLoaiPhong },createdLoaiPhong);
        }

        // Get : api/loaiphongs/{maloaiphong} : Lấy thông tin loại phòng theo mã loại phòng
        [HttpGet("{maloaiphong}")]
        public async Task<IActionResult> GetLoaiPhongById(int maloaiphong)
        {
            var loaiPhong = await _loaiPhongService.GetLoaiPhongByIdAsync(maloaiphong);
            if (loaiPhong == null)
            {
                return NotFound();
            }
            return Ok(loaiPhong);
        }

        // Put : api/loaiphongs/{maloaiphong} : Cập nhật loại phòng
        [HttpPut("{maloaiphong}")]
        public async Task<IActionResult> UpdateLoaiPhong(int maloaiphong, [FromBody] UpdateLoaiPhongRequestDto loaiPhong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedLoaiPhong = await _loaiPhongService.UpdateLoaiPhongAsync(maloaiphong, loaiPhong);
            if (updatedLoaiPhong == null)
            {
                return NotFound();
            }
            return Ok(updatedLoaiPhong);
        }

        // Delete : api/loaiphongs/{maloaiphong} : Xóa loại phòng
        [HttpDelete("{maloaiphong}")]
        public async Task<IActionResult> DeleteLoaiPhong(int maloaiphong)
        {
            var isDeleted = await _loaiPhongService.DeleteLoaiPhongAsync(maloaiphong);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Get : api/loaiphongs/filter?minPrice={minPrice}&maxPrice={maxPrice} : Lọc loại phòng theo khoảng giá
        [HttpGet("filter")]
        public async Task<IActionResult> FilterLoaiPhongByPrice([FromQuery] FilterLoaiPhongRequest filter)
        {
            var loaiPhongs = await _loaiPhongService.FilterLoaiPhongByPriceAsync(filter);
            return Ok(loaiPhongs);
        }
    }
}
