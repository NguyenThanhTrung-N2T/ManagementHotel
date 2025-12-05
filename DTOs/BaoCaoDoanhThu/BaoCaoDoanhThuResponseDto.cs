namespace ManagementHotel.DTOs.BaoCaoDoanhThu
{
    public class BaoCaoDoanhThuResponseDto
    {
        public int MaBaoCao { get; set; }
        public DateTime NgayLap { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public int TongDoanhThu { get; set; }
        public string? GhiChu { get; set; }
    }
}
