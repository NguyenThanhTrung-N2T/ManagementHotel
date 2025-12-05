using ManagementHotel.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietHoaDonsController : ControllerBase
    {
        private readonly IChiTietHoaDonService _chiTietHoaDonService;
        public ChiTietHoaDonsController(IChiTietHoaDonService chiTietHoaDonService)
        {
            _chiTietHoaDonService = chiTietHoaDonService;
        }

        // Post: api/ChiTietHoaDons : thêm dịch vụ vào chi tiết hóa đơn
        [Authorize(Policy = "ActiveUser")]
        [HttpPost]
        public async Task<IActionResult> AddDichVuToChiTietHoaDonAsync([FromBody] DTOs.ChiTietHoaDon.CreateChiTietHoaDonRequestDto createDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _chiTietHoaDonService.AddDichVuToChiTietHoaDonAsync(createDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Delete: api/ChiTietHoaDons/{maChiTietHD} : xóa chi tiết hóa đơn theo mã chi tiết hóa đơn
        [Authorize(Policy = "ActiveUser")]
        [HttpDelete("{maChiTietHD}")]
        public async Task<IActionResult> DeleteChiTietHoaDonAsync(int maChiTietHD)
        {
            try
            {
                var result = await _chiTietHoaDonService.DeleteChiTietHoaDonAsync(maChiTietHD);
                if (!result)
                {
                    return NotFound(new { message = "Chi tiết hóa đơn với mã " + maChiTietHD + " không tồn tại." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Put: api/ChiTietHoaDons/{maChiTietHD} : cập nhật chi tiết hóa đơn theo mã chi tiết hóa đơn
        [Authorize(Policy = "ActiveUser")]
        [HttpPut("{maChiTietHD}")]
        public async Task<IActionResult> UpdateChiTietHoaDonAsync(int maChiTietHD, [FromBody] DTOs.ChiTietHoaDon.UpdateChiTietHoaDonRequestDto updateDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _chiTietHoaDonService.UpdateChiTietHoaDonAsync(maChiTietHD, updateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
