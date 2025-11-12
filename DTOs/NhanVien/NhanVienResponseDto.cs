using System;

namespace ManagementHotel.DTOs.NhanVien
{
    public class NhanVienResponseDto
    {
        public int MaNhanVien { get; set; }
        public string? HoTen { get; set; }
        public string? CCCD { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? ChucVu { get; set; }
        public int? Luong { get; set; }
    }
}