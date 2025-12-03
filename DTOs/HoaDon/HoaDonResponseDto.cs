namespace ManagementHotel.DTOs.HoaDon
{
    public class HoaDonResponseDto
    {
        public int MaHoaDon { get; set; }
        public string? TrangThaiThanhToan { get; set; } // Chưa thanh toán, Đã thanh toán
        public int TongTien { get; set; }
        public DateTime NgayLap { get; set; }
    }
}
