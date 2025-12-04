using ManagementHotel.DTOs.DatPhong;
using ManagementHotel.Models;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services.IServices;
using System.Reflection.Metadata.Ecma335;
namespace ManagementHotel.Services
{
    public class DatPhongService : IDatPhongService
    {
        private readonly IDatPhongRepository _datPhongRepository;
        private readonly IKhachHangRepository _khachHangRepository;
        public readonly IPhongRepository _phongRepository;
        private readonly IHoaDonRepository _hoaDonRepository;
        public DatPhongService(IDatPhongRepository datPhongRepository, IKhachHangRepository khachHangRepository, IHoaDonRepository hoaDonRepository, IPhongRepository phongReposiotry)
        {
            _datPhongRepository = datPhongRepository;
            _khachHangRepository = khachHangRepository;
            _hoaDonRepository = hoaDonRepository;
            _phongRepository = phongReposiotry;
        }

        // lấy tất cả đặt phòng
        public async Task<IEnumerable<DatPhongListResponseDto>> GetAllDatPhongsAsync()
        {
            return await _datPhongRepository.GetAllDatPhongsAsync();
        }

        // lấy chi tiết đặt phòng theo mã đặt phòng
        public async Task<DatPhongResponseDto?> GetDatPhongByIdAsync(int maDatPhong)
        {
            return await _datPhongRepository.GetDatPhongByIdAsync(maDatPhong);
        }

        // tạo đặt phòng mới
        public async Task<DatPhongResponseDto> CreateDatPhongAsync(CreateDatPhongRequestDto createDatPhongRequestDto)
        {
            try
            {
                // kiểm tra khách hàng tồn tại
                var khachHang = await _khachHangRepository.GetKhachHangByIdAsync(createDatPhongRequestDto.MaKhachHang);
                if (khachHang == null)
                {
                    throw new Exception("Khách hàng với mã " + createDatPhongRequestDto.MaKhachHang + " không tồn tại.");
                }
                // kiểm tra phòng tồn tại và trạng thái phòng
                var phong = await _phongRepository.GetPhongByIdAsync(createDatPhongRequestDto.MaPhong);
                if(phong == null)
                {
                    throw new Exception("Phòng với mã " + createDatPhongRequestDto.MaPhong + " không tồn tại.");
                }
                if(phong.TrangThai != "Trống")
                {
                    throw new Exception("Phòng với mã " + createDatPhongRequestDto.MaPhong + " không có trạng thái trống.");
                }
                // kiểm tra ngày nhận và ngày trả phòng
                if(createDatPhongRequestDto.NgayNhanPhong >= createDatPhongRequestDto.NgayTraPhong)
                {
                    throw new Exception("Ngày nhận phòng phải trước ngày trả phòng.");
                }

                // kiểm tra trạng thái đặt phòng
                if(createDatPhongRequestDto.TrangThai != "Đã đặt" && createDatPhongRequestDto.TrangThai != "Đang ở")
                {
                    throw new Exception("Trạng thái đặt phòng không phù hợp.");
                }

                // kiểm tra phòng còn trống trong thời gian đặt hay không 
                var check_empty = await _datPhongRepository.IsPhongAvailableAsync(createDatPhongRequestDto.MaPhong, createDatPhongRequestDto.NgayNhanPhong, createDatPhongRequestDto.NgayTraPhong);
                if (!check_empty)
                {
                    throw new Exception("Phòng đã được đặt trong thời gian dự kiến.");
                }

                // tạo đặt phòng mới
                var datPhong = await _datPhongRepository.CreateDatPhongAsync(createDatPhongRequestDto);
                // kiểm tra trạng thái đặt phòng
                if (datPhong.TrangThai == "Đang ở")
                {
                    // tạo hóa đơn 
                    var hoadon = await _hoaDonRepository.CreateHoaDonAsync(datPhong.MaDatPhong);
                }

                // trả về client 
                var datPhongDto = await _datPhongRepository.GetDatPhongByIdAsync(datPhong.MaDatPhong);
                return datPhongDto!;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tạo đặt phòng: " + ex.Message);
            }
        }

        // cập nhật trạng thái đặt phòng
        public async Task<DatPhongResponseDto?> UpdateDatPhongStatusAsync(int maDatPhong, string trangThai)
        {
            try
            {
                // Lấy thông tin đặt phòng
                var datPhong = await _datPhongRepository.GetDatPhongByIdAsync(maDatPhong);
                if (datPhong == null)
                {
                    throw new Exception($"Đặt phòng với mã {maDatPhong} không tồn tại.");
                }

                if(trangThai == "Đã hủy")
                {
                    if(datPhong.HoaDon != null)
                    {
                        // tính lại tiền phòng nếu hủy trước hạn đặt phòng
                        if(DateTime.Now != datPhong.NgayTraPhong)
                        {
                            datPhong.HoaDon.TongTien = await _hoaDonRepository.TinhTongTien(datPhong.MaDatPhong, DateTime.Now);
                            var hoaDonNew = await _hoaDonRepository.UpdateTongTienInHoaDon(datPhong.HoaDon.TongTien, datPhong.HoaDon.MaHoaDon);
                        }
                    }
                }

                // Nếu chuyển sang trạng thái "Đang ở" thì tạo hóa đơn (nếu chưa có)
                if (trangThai == "Đang ở")
                {
                    if (datPhong.HoaDon == null)
                    {
                        await _hoaDonRepository.CreateHoaDonAsync(datPhong.MaDatPhong);
                    }
                }

                // Cập nhật trạng thái đặt phòng
                var updatedDatPhong = await _datPhongRepository.UpdateDatPhongStatusAsync(maDatPhong, trangThai);

                return updatedDatPhong;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thay đổi trạng thái đặt phòng: " + ex.Message, ex);
            }
        }

        // lọc đặt phòng theo trạng thái
        public async Task<IEnumerable<DatPhongListResponseDto>> FilterDatPhongByStatusAsync(string trangThai)
        {
            return await _datPhongRepository.FilterDatPhongByStatusAsync(trangThai);
        }
    }
}
