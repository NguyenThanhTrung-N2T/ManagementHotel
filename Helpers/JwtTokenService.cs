using ManagementHotel.Configs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagementHotel.Helpers
{
    public class JwtTokenService
    {
        private readonly JwtConfig _jwtConfig;

        public JwtTokenService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public string GenerateToken(string maTaiKhoan, string tenDangNhap, string vaiTro, string trangThai)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, maTaiKhoan),
                new Claim(JwtRegisteredClaimNames.UniqueName, tenDangNhap),
                new Claim(ClaimTypes.Role, vaiTro),   // role
                new Claim("Status", trangThai),        // status
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtConfig.ExpirationInHours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
