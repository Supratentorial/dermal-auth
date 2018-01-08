using dermal.auth.Data;
using dermal.auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dermal.auth.Interfaces
{
    public interface IUserMapper
    {
        UserDto WriteUserDto(ApplicationUser user);
        ApplicationUser WriteUser(UserDto userDto);
    }
}
