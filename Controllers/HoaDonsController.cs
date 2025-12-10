using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementHotel.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using ManagementHotel.DTOs.HoaDon;
namespace ManagementHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonsController : ControllerBase
    {
        private readonly IHoaDonService _hoaDonService;
        public HoaDonsController(IHoaDonService hoaDonService)
        {
            _hoaDonService = hoaDonService;
        }

        // Get : api/hoadons/filter?trangThai= : Lọc hóa đơn theo trạng thái thanh toán
        [HttpGet("filter")]
        public async Task<IActionResult> FilterHoaDonByStatus([FromQuery] string trangThai)
        {
            var hoaDons = await _hoaDonService.FilterHoaDonByStatusAsync(trangThai);
            return Ok(hoaDons);
        }

        // Get : api/hoadons : Lấy tất cả hóa đơn
        [Authorize(Policy = "ActiveUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllHoaDons()
        {
            var hoaDons = await _hoaDonService.GetAllHoaDonsAsync();
            return Ok(hoaDons);
        }

        // Get : api/hoadons/{maHoaDon} : Lấy thông tin hóa đơn theo mã hóa đơn
        [Authorize(Policy = "ActiveUser")]
        [HttpGet("{maHoaDon}")]
        public async Task<IActionResult> GetHoaDonDetailById(int maHoaDon)
        {
            var hoaDon = await _hoaDonService.GetHoaDonDetailByIdAsync(maHoaDon);
            if (hoaDon == null)
            {
                return NotFound();
            }
            return Ok(hoaDon);
        }

        // Put : api/hoadons/{maHoaDon}
        [Authorize(Policy = "ActiveUser")]
        [HttpPut("{maHoaDon}")]
        public async Task<IActionResult> UpdateStatusHoaDonAsunc(int maHoaDon,UpdateHoaDonRequestDto dto)
        {
            var hoaDon = await _hoaDonService.UpdateHoaDonAsync(maHoaDon,dto);
            if (hoaDon == null)
            {
                return NotFound();
            }
            return Ok(hoaDon);
        }
    }
}
