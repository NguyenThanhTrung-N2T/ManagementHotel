namespace ManagementHotel.Models
{
    public class TaiKhoan
    {
        public int MaTaiKhoan { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public string? VaiTro { get; set; }
        public string? TrangThai { get; set; } = "Hoạt động";
        public int MaNhanVien { get; set; }

        // Tai khoan thuoc Nhan vien (1-1)
        public virtual NhanVien? NhanVien { get; set; }
    }
}
