namespace ManagementHotel.Models
{
    public class NhanVien
    {
        public int MaNhanVien { get; set; }
        public string? HoTen { get; set; }
        public string? ChucVu { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }

        //Nhan vien co 1 tai khoan (1-1)
        public virtual TaiKhoan? TaiKhoan { get; set; }
        // Nhan vien tao nhieu hoa don (1-N)
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
}
