using ManagementHotel.Configs;
using ManagementHotel.Data;
using ManagementHotel.Helpers;
using ManagementHotel.Repositories;
using ManagementHotel.Repositories.IRepositories;
using ManagementHotel.Services;
using ManagementHotel.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Bind settings
var jwtConfig = new JwtConfig();
builder.Configuration.GetSection("JwtConfig").Bind(jwtConfig);
builder.Services.AddSingleton(jwtConfig);

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Thêm Event để lấy token từ cookie
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        },

        // Không gửi token
        OnChallenge = context =>
        {
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = System.Text.Json.JsonSerializer.Serialize(new
            {
                message = "Unauthorized"
            });

            return context.Response.WriteAsync(response);
        },

        // Token sai / hết hạn
        OnAuthenticationFailed = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = System.Text.Json.JsonSerializer.Serialize(new
            {
                message = "Token không hợp lệ hoặc đã hết hạn"
            });

            return context.Response.WriteAsync(response);
        },

        // Role/Claim không đủ quyền
        OnForbidden = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var response = System.Text.Json.JsonSerializer.Serialize(new
            {
                message = "Bạn không có quyền truy cập tài nguyên này"
            });

            return context.Response.WriteAsync(response);
        }
    };



    // Cấu hình xác thực token
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
    };
});

// Authorization với các policy
builder.Services.AddAuthorization(options =>
{
    // Policy cho Admin ACTIVE
    options.AddPolicy("AdminActive", policy =>
    {
        policy.RequireRole("Admin");                     // role = Admin
        policy.RequireClaim("Status", "Hoạt động");      // status = Hoạt động
    });

    // Policy cho Nhân viên ACTIVE
    options.AddPolicy("StaffActive", policy =>
    {
        policy.RequireRole("Nhân viên");
        policy.RequireClaim("Status", "Hoạt động");      // status = Hoạt động
    });

    // Policy chung Admin + Nhân viên ACTIVE
    options.AddPolicy("ActiveUser", policy =>
    {
        policy.RequireRole("Admin", "Nhân viên");
        policy.RequireClaim("Status", "Hoạt động");
    });
});

builder.Services.AddSingleton<JwtTokenService>();

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
builder.Services.AddScoped<IDichVuRepository, DichVuRepository>();
builder.Services.AddScoped<IDatPhongRepository, DatPhongRepository>();
builder.Services.AddScoped<ILoaiPhongService, LoaiPhongService>();
builder.Services.AddScoped<IPhongService, PhongService>();
builder.Services.AddScoped<IKhachHangService, KhachHangService>();
builder.Services.AddScoped<INhanVienService, NhanVienService>();
builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();
builder.Services.AddScoped<IDichVuService, DichVuService>();
builder.Services.AddScoped<IDatPhongService, DatPhongService>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
