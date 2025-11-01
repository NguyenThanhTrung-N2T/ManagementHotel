using Microsoft.EntityFrameworkCore;
namespace ManagementHotel.Data
{
    public class ManagementHotelContext : DbContext
    {
        public ManagementHotelContext(DbContextOptions<ManagementHotelContext> options)
            : base(options)
        {
        }
    }
}
