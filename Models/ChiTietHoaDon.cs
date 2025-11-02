namespace ManagementHotel.Models
{
    public class ChiTietHoaDon
    {
        public int MaChiTietHD { get; set; }
        public int MaHoaDon { get; set; }
        public int MaDichVu { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
        public string? Mota { get; set; }

        // ChiTietHoaDon thuoc HoaDon (N-1)
        public virtual HoaDon HoaDon { get; set; } = null!;
        // ChiTietHoaDon thuoc DichVu (N-1)
        public virtual DichVu? DichVu { get; set; }
    }
}
