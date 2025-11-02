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

        // Cau hinh quan he 1-1 giua DatPhong va HoaDon
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatPhong>()
                .HasOne(dp => dp.HoaDon)
                .WithOne(hd => hd.DatPhong)
                .HasForeignKey<HoaDon>(hd => hd.MaDatPhong); // HoaDon là bên dependent

            base.OnModelCreating(modelBuilder);

            // Quan hệ 1-1 giữa NhanVien và TaiKhoan
            modelBuilder.Entity<NhanVien>()
                .HasOne(nv => nv.TaiKhoan)
                .WithOne(tk => tk.NhanVien)
                .HasForeignKey<TaiKhoan>(tk => tk.MaNhanVien); // TaiKhoan giữ khóa ngoại

            base.OnModelCreating(modelBuilder);
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
