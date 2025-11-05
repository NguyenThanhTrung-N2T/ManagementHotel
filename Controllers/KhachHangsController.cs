using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementHotel.Services.IServices;

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
    }
}
