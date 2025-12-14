using ManagementHotel.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaoCaoDoanhThusController : ControllerBase
    {
        private readonly IBaoCaoDoanhThuService _baocaoService;
        public BaoCaoDoanhThusController(IBaoCaoDoanhThuService baocaoService)
        {
            _baocaoService = baocaoService;
        }

        // Get : api/baocaodoanhthus/monthly: lấy tất cả báo cáo doanh thu
        [Authorize(Policy = "AdminActive")]
        [HttpGet("monthly")]
        public async Task<IActionResult> GetAllBaoCaoDoanhThus()
        {
            try
            {
                var baocaos = await _baocaoService.GetAllReportByMonthly();
                return Ok(baocaos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy báo cáo doanh thu: {ex.Message}");
            }
        }
    }
}
