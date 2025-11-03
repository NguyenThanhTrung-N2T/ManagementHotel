using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementHotel.Services;

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
    }
}
