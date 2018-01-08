using Microsoft.AspNetCore.Identity;

namespace dermal.auth.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string TenantId { get; set; }
    }
}