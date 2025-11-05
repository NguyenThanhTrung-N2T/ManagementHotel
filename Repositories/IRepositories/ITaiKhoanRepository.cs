using Microsoft.AspNetCore.Identity.Data;
using System.Diagnostics.Eventing.Reader;

namespace ManagementHotel.Repositories.IRepositories
{
    public interface ITaiKhoanRepository
    {
        bool ValidateAccount(LoginRequest user);
    }
}
