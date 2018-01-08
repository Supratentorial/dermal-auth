using dermal.auth.Interfaces;
using System;
using dermal.auth.Data;
using dermal.auth.Models;

namespace dermal.auth.Services
{
    public class UserMapper : IUserMapper
    {
        public ApplicationUser WriteUser(UserDto userDto)
        {
            var user = new ApplicationUser();
            user.Id = userDto.Id;
            user.GivenName = userDto.GivenName;
            user.UserName = userDto.Email;
            user.Email = userDto.Email;
            return user;
        }

        public UserDto WriteUserDto(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
