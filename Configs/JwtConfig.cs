namespace ManagementHotel.Configs
{
    public class JwtConfig
    {
        public string SecretKey { get; set; } = string.Empty; // Khóa bí mật để ký token
        public string Issuer { get; set; } = string.Empty;    // Ai phát token
        public string Audience { get; set; } = string.Empty;  // Ai dùng token
        public int ExpirationInHours { get; set; } = 8;      // Thời hạn token (tính bằng giờ)
    }
}
