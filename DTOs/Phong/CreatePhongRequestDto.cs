namespace ManagementHotel.DTOs.Phong
{
    public class CreatePhongRequestDto
    {
        public string? SoPhong { get; set; }
        public int MaLoaiPhong { get; set; }
        public string? TrangThai { get; set; } = "Trống";
        public string? GhiChu { get; set; }
    }
}
