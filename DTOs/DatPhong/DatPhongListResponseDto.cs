namespace ManagementHotel.DTOs.DatPhong
{
    public class DatPhongListResponseDto
    {
        public int MaDatPhong { get; set; }
        public string? TenKhachHang { get; set; }
        public string? SoDienThoai { get; set; }
        public string? TenPhong { get; set; }
        public string? LoaiPhong { get; set; }

        public DateTime NgayNhanPhong { get; set; }
        public DateTime NgayTraPhong { get; set; }

        public string? TrangThai { get; set; }
    }

}
