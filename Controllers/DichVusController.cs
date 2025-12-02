using ManagementHotel.DTOs.DichVu;
using ManagementHotel.DTOs.KhachHang;
using ManagementHotel.DTOs.LoaiPhong;
using ManagementHotel.Services;
using ManagementHotel.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVusController : ControllerBase
    {
        private readonly IDichVuService _dichVuService;
        public DichVusController(IDichVuService dichVuService)
        {
            _dichVuService = dichVuService;
        }

        // Get : api/dichvus : lay tat ca dich vu
        [HttpGet]
        public async Task<IActionResult> GetAllDichVus()
        {
            // lay tat ca dich vu
            var dichvus = await _dichVuService.GetAllDichVuAsync();
            // tra ve client
            return Ok(dichvus);
        }

        // Get : api/dichvus/{maDichVu} : lay dich vu theo ma dich vu
        [Authorize(Policy = "ActiveUser")]
        [HttpGet("{maDichVu}")]
        public async Task<IActionResult> GetDichVuById(int maDichVu)
        {
            // lay dich vu theo id 
            var dichvu = await _dichVuService.GetDichVuByIdAsync(maDichVu);

            // neu khong co
            if(dichvu == null)
            {
                return NotFound();
            }
            // tra ve cho client
            return Ok(dichvu);
        }

        // Post : api/dichvus : Thêm dich vu mới
        [Authorize(Policy = "AdminActive")]
        [HttpPost]
        public async Task<IActionResult> AddDichVu([FromBody] CreateDichVuRequestDto requestDto)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Thêm dịch vụ mới
            var createdDichVu = await _dichVuService.AddDichVuAsync(requestDto);
            // Trả về kết quả
            return CreatedAtAction(nameof(GetDichVuById), new { maDichVu = createdDichVu.MaDichVu }, createdDichVu);
        }

        // Put : api/dichvus/{maDichVu} : Cập nhật dich vu
        [Authorize(Policy = "ActiveUser")]
        [HttpPut("{maDichVu}")]
        public async Task<IActionResult> UpdateDichVu(int maDichVu, [FromBody] UpdateDichVuRequestDto dichvu)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Cập nhật dich vu
            var updatedDichVu = await _dichVuService.UpdateDichVuAsync(maDichVu, dichvu);
            // Trả về kết quả
            return Ok(updatedDichVu);
        }


        // Delete : api/dichvus/{maDichVu} : Xóa dịch vụ
        [Authorize(Policy = "AdminActive")]
        [HttpDelete("{maDichVu}")]
        public async Task<IActionResult> DeleteDichVu(int maDichVu)
        {
            // Xóa dich vu
            var result = await _dichVuService.DeleteDichVuAsync(maDichVu);
            // Nếu không tìm thấy, trả về 404
            if (!result)
            {
                return NotFound();
            }
            // Trả về kết quả
            return NoContent();
        }

        // Get : api/dichvus/filter?TenDichVu={tendichvu}&DonGia={DonGia}&DonVi={DonVi} : Lọc loại phòng theo khoảng giá
        [HttpGet("filter")]
        public async Task<IActionResult> FilterDichVu([FromQuery] FilterDichVuRequestDto filter)
        {
            // Lọc dịch vụ theo các tiêu chí
            var dichvus = await _dichVuService.FilterDichVuAsync(filter);
            // Trả về kết quả
            return Ok(dichvus);
        }
    }
}
