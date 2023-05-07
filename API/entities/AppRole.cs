using Microsoft.AspNetCore.Identity;

namespace API.entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}