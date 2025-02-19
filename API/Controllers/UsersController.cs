using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	public class UsersController(DataContext context) : BaseApiController
	{
		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
		{
			return await context.Users.ToListAsync();
		}

		[Authorize]
		[HttpGet("{id:int}")]
		public async Task<ActionResult<AppUser>> GetUser(int id)
		{
			var user = await context.Users.FindAsync(id);

			return user == null ? NotFound() : user;
		}
	}
}
