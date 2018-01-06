using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dermal.auth.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace dermal.auth.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {

        private readonly DermalAuthDbContext _dbContext;
        
        public UsersController(DermalAuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersAsync() {
            var users = await this._dbContext.Users.ToListAsync();
            return Ok(users);
        }
    }
}