using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController(DataContext context) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
		{
			return await context.Users.ToListAsync();
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<AppUser>> GetUser(int id)
		{
			var user = await context.Users.FindAsync(id);
			
			return  user == null ? NotFound(): user;
		}
	}
}
