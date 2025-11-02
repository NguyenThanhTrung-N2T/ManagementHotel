namespace ManagementHotel.Models
{
    public class Phong
    {
        public int MaPhong { get; set; }
        public int SoPhong { get; set; }
        public int MaLoaiPhong { get; set; }
        public string? TrangThai { get; set; } = "Trống";
        public string? GhiChu { get; set; }

        // Phong thuộc Loai Phong (N-1)
        public virtual LoaiPhong LoaiPhong { get; set; } = null!;
        // Phong có nhiều Đặt Phòng (1-N)
        public virtual ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();
    }
}
