using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dermal.auth.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string TenantId { get; set; }
    }
}
