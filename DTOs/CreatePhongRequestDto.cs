namespace ManagementHotel.DTOs
{
    public class CreatePhongRequestDto
    {
        public int SoPhong { get; set; }
        public int MaLoaiPhong { get; set; }
        public string? TrangThai { get; set; } = "Trống";
        public string? GhiChu { get; set; }
    }
}
