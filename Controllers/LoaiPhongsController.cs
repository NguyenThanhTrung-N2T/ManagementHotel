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
            // Lấy tất cả loại phòng
            var loaiPhongs = await _loaiPhongService.GetAllLoaiPhongAsync();
            // Trả về kết quả
            return Ok(loaiPhongs);
        }

        // Post : api/loaiphongs : Thêm loại phòng mới
        [HttpPost]
        public async Task<IActionResult> AddLoaiPhong([FromBody] CreateLoaiPhongRequestDto loaiPhong)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Thêm loại phòng mới
            var createdLoaiPhong = await _loaiPhongService.AddLoaiPhongAsync(loaiPhong);
            // Trả về kết quả
            return CreatedAtAction(nameof(GetLoaiPhongById),new { maloaiphong = createdLoaiPhong.MaLoaiPhong },createdLoaiPhong);
        }

        // Get : api/loaiphongs/{maloaiphong} : Lấy thông tin loại phòng theo mã loại phòng
        [HttpGet("{maloaiphong}")]
        public async Task<IActionResult> GetLoaiPhongById(int maloaiphong)
        {
            // Lấy loại phòng theo mã
            var loaiPhong = await _loaiPhongService.GetLoaiPhongByIdAsync(maloaiphong);
            // Kiểm tra kết quả
            if (loaiPhong == null)
            {
                // Nếu không tìm thấy, trả về 404
                return NotFound();
            }
            // Trả về kết quả
            return Ok(loaiPhong);
        }

        // Put : api/loaiphongs/{maloaiphong} : Cập nhật loại phòng
        [HttpPut("{maloaiphong}")]
        public async Task<IActionResult> UpdateLoaiPhong(int maloaiphong, [FromBody] UpdateLoaiPhongRequestDto loaiPhong)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Cập nhật loại phòng
            var updatedLoaiPhong = await _loaiPhongService.UpdateLoaiPhongAsync(maloaiphong, loaiPhong);
            // Kiểm tra kết quả
            if (updatedLoaiPhong == null)
            {
                // Nếu không tìm thấy, trả về 404
                return NotFound();
            }
            // Trả về kết quả
            return Ok(updatedLoaiPhong);
        }

        // Delete : api/loaiphongs/{maloaiphong} : Xóa loại phòng
        [HttpDelete("{maloaiphong}")]
        public async Task<IActionResult> DeleteLoaiPhong(int maloaiphong)
        {
            // Xóa loại phòng
            var isDeleted = await _loaiPhongService.DeleteLoaiPhongAsync(maloaiphong);
            // Kiểm tra kết quả
            if (!isDeleted)
            {
                // Nếu không tìm thấy, trả về 404
                return NotFound();
            }
            // Trả về kết quả
            return NoContent();
        }

        // Get : api/loaiphongs/filter?minPrice={minPrice}&maxPrice={maxPrice} : Lọc loại phòng theo khoảng giá
        [HttpGet("filter")]
        public async Task<IActionResult> FilterLoaiPhongByPrice([FromQuery] FilterLoaiPhongRequest filter)
        {
            // Lọc loại phòng theo các tiêu chí
            var loaiPhongs = await _loaiPhongService.FilterLoaiPhongByPriceAsync(filter);
            // Trả về kết quả
            return Ok(loaiPhongs);
        }
    }
}
