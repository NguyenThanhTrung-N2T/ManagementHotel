namespace ManagementHotel.Models
{
    public class DichVu
    {
        public int MaDichVu { get; set; }
        public string? TenDichVu { get; set; }
        public string? DonVi { get; set; }
        public int DonGia { get; set; }

        // DichVu co nhieu ChiTietHoaDon (1-N)
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
    }
}
