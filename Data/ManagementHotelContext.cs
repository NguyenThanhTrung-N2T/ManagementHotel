using ManagementHotel.Models;
using Microsoft.EntityFrameworkCore;
namespace ManagementHotel.Data
{
    public class ManagementHotelContext : DbContext
    {
        public ManagementHotelContext(DbContextOptions<ManagementHotelContext> options)
            : base(options)
        {
        }

        // Them cac DBSet cho cac table trong database
        public DbSet<Phong> phongs { get; set; }
        public DbSet<KhachHang> khachHangs { get; set; }
        public DbSet<DatPhong> datPhongs { get; set; }
        public DbSet<DichVu> dichVus { get; set; }
        public DbSet<BaoCaoDoanhThu> baoCaoDoanhThus { get; set; }
        public DbSet<NhanVien> nhanViens { get; set; }
        public DbSet<LoaiPhong> loaiPhongs { get; set; }
        public DbSet<HoaDon> hoaDons { get; set; }
        public DbSet<ChiTietHoaDon> chiTietHoaDons { get; set; }
        public DbSet<TaiKhoan> taiKhoans { get; set; }
    }
}
