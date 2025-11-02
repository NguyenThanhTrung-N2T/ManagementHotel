namespace ManagementHotel.Models
{
    public class LoaiPhong
    {
        public int MaLoaiPhong { get; set; }
        public string? TenLoaiPhong { get; set; }
        public string? MoTa { get; set; }
        public int GiaTheoDem { get; set; }

        // Loai Phong có nhiều Phong (1-N)
        public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();
    }
}
