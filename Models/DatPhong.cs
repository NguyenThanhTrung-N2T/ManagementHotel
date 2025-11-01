namespace ManagementHotel.Models
{
    public class DatPhong
    {
        public int MaDatPhong { get; set; }
        public int MaKhachHang { get; set; }
        public int MaPhong { get; set; }
        public DateTime NgayNhanPhong { get; set; }
        public DateTime NgayTraPhong { get; set; }
        public string? TrangThai { get; set; }
        public string? GhiChu { get; set; }
    }
}
