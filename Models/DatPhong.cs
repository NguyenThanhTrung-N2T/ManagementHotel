namespace ManagementHotel.Models
{
    public class DatPhong
    {
        public int MaDatPhong { get; set; }
        public int MaKhachHang { get; set; }
        public int MaPhong { get; set; }
        public DateTime NgayNhanPhong { get; set; }
        public DateTime NgayTraPhong { get; set; }
        public string? TrangThai { get; set; } = "Đã đặt";
        public string? GhiChu { get; set; }

        // DatPhong thuoc KhachHang (N-1)
        public virtual KhachHang KhachHang { get; set; } = null!;

        // DatPhong thuoc Phong (N-1)
        public virtual Phong Phong { get; set; } = null!;

        // DatPhong co 1 HoaDon (1-1)
        public virtual HoaDon? HoaDon { get; set; }
        }
}
