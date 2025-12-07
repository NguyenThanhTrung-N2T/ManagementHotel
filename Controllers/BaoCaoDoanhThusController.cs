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

        // Get : api/baocaodoanhthus: lấy tất cả báo cáo doanh thu
        [Authorize(Policy = "ActiveUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllBaoCaoDoanhThus()
        {
            try
            {
                var baocaos = await _baocaoService.GetAllBaoCaoDoanhThuAsync();
                return Ok(baocaos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy báo cáo doanh thu: {ex.Message}");
            }
        }

        // Get : api/baocaodoanhthus/nam={nam} && thang={thang}: lấy báo cáo doanh thu theo tháng và năm
        [Authorize(Policy = "ActiveUser")]
        [HttpGet("baocaotheothang-nam")]
        public async Task<IActionResult> GetBaoCaoDoanhThuByMonthYear([FromQuery] int nam, [FromQuery] int thang)
        {
            try
            {
                var baocao = await _baocaoService.GetBaoCaoDoanhThuByMonthYearAsync(nam, thang);
                if (baocao == null)
                {
                    return NotFound($"Không tìm thấy báo cáo doanh thu cho tháng {thang} năm {nam}.");
                }
                return Ok(baocao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy báo cáo doanh thu: {ex.Message}");
            }
        }

        // Post : api/baocaodoanhthus : tạo báo cáo doanh thu 
        [Authorize(Policy = "ActiveUser")]
        [HttpPost]
        public async Task<IActionResult> AddBaoCaoDoanhThu()
        {
            var baocaoDoanhthu = await _baocaoService.CreateBaoCaoDoanhThuAsync();
            return Ok( baocaoDoanhthu);
        }
    }
}
