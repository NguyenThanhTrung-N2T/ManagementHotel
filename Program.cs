using ManagementHotel.Data;
using ManagementHotel.Repositories;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services;
using ManagementHotel.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình routing để sử dụng URL chữ thường
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Cấu hình dịch vụ và connect cơ sở dữ liệu
builder.Services.AddControllers();
builder.Services.AddDbContext<ManagementHotelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Đăng ký Services và Repositories 
builder.Services.AddScoped<ILoaiPhongRepository, LoaiPhongRepository>();
builder.Services.AddScoped<IPhongRepository, PhongRepository>();
builder.Services.AddScoped<IKhachHangRepository, KhachHangRepository>();
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();
builder.Services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
builder.Services.AddScoped<ILoaiPhongService, LoaiPhongService>();
builder.Services.AddScoped<IPhongService, PhongService>();
builder.Services.AddScoped<IKhachHangService, KhachHangService>();
builder.Services.AddScoped<INhanVienService, NhanVienService>();
builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Kiểm tra kết nối cơ sở dữ liệu 
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var logger = services.GetRequiredService<ILogger<Program>>();
var context = services.GetRequiredService<ManagementHotelContext>();
try
{
    if (await context.Database.CanConnectAsync())
    {
        logger.LogInformation("Ket noi thanh cong!");
    }
    else
    {
        logger.LogWarning("Ket noi that bai!");
    }
}
catch (Exception ex)
{
    logger.LogError(ex, "Khong the ket noi database!");
}

// Cấu hình pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
