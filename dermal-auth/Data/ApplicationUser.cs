using Microsoft.AspNetCore.Identity;

namespace dermal.auth.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string TenantId { get; set; }
    }
}