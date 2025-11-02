namespace ManagementHotel.Models
{
    public class HoaDon
    {
        public int MaHoaDon { get; set; }
        public int MaDatPhong { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public int TongTien { get; set; }

        // HoaDon thuoc DatPhong (1-1)
        public virtual DatPhong DatPhong { get; set; } = null!;
        // HoaDon thuoc NhanVien (N-1)
        public virtual NhanVien NhanVien { get; set; } = null!;
        // HoaDon co nhieu ChiTietHoaDon (1-N)
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
    }
}
