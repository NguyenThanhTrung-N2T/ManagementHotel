using ManagementHotel.Services.IServices;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.DTOs.HoaDon;
namespace ManagementHotel.Services
{
    public class HoaDonService : IHoaDonService
    {
        private readonly IHoaDonRepository _hoaDonRepository;
        private readonly IDatPhongRepository _datphongRepository;

        public HoaDonService(IHoaDonRepository hoaDonRepository, IDatPhongRepository datphongrepository)
        {
            _hoaDonRepository = hoaDonRepository;
            _datphongRepository = datphongrepository;
        }
        // lọc hóa đơn theo trạng thái thanh toán
        public async Task<IEnumerable<HoaDonResponseDto>> FilterHoaDonByStatusAsync(string trangThai)
        {
            return await _hoaDonRepository.FilterHoaDonByStatusAsync(trangThai);
        }

        // lấy danh sách hóa đơn
        public async Task<IEnumerable<HoaDonResponseDto>> GetAllHoaDonsAsync()
        {
            return await _hoaDonRepository.GetAllHoaDonsAsync();
        }

        // lấy hóa đơn theo mã hóa đơn
        public async Task<HoaDonDetailResponseDto?> GetHoaDonDetailByIdAsync(int maHoaDon)
        {
            return await _hoaDonRepository.GetHoaDonDetailByIdAsync(maHoaDon);
        }

        // cập nhật trạng thái hóa đơn
        public async Task<HoaDonResponseDto?> UpdateHoaDonAsync(int maHoaDon, UpdateHoaDonRequestDto dto)
        {
            var hoaDon = await _hoaDonRepository.GetHoaDonDetailByIdAsync(maHoaDon);
            if(hoaDon == null)
            {
                return null;
            }

            var datPhong = await _datphongRepository.GetDatPhongByIdAsync(hoaDon.MaDatPhong);
            if(datPhong == null)
            {
                return null;
            }

            // cập nhật trạng thái đặt phòng
            var updatedatPhong = await _datphongRepository.UpdateDatPhongStatusAsync(datPhong.MaDatPhong, "Đã hủy");
            // cập nhật hóa đơn 
            var updateHoaDOn = await _hoaDonRepository.UpdateHoaDonAsync(maHoaDon, dto.trangThai);
            if (updateHoaDOn == null) {
                return null;
            }

            return new HoaDonResponseDto
            {
                MaHoaDon = updateHoaDOn.MaHoaDon,
                NgayLap = updateHoaDOn.NgayLap,
                TongTien = updateHoaDOn.TongTien,
                TrangThaiThanhToan = updateHoaDOn.TrangThaiThanhToan
            };
        }
    }
}
