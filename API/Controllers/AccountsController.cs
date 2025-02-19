using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController(DataContext context, ITokenService tokenService) : ControllerBase
	{
		[Route("register")]
		[HttpPost]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			if (await UserExists(registerDto.Username)) return BadRequest("username is taken");

			using var hmac = new HMACSHA512();

			var user = new AppUser
			{
				UserName = registerDto.Username,
				PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
				PasswordSalt = hmac.Key
			};

			context.Users.Add(user);
			await context.SaveChangesAsync();

			var userDto = new UserDto
			{
				Username = registerDto.Username,
				Token = tokenService.CreateToken(user),
			};

			return userDto;
		}

		[Route("login")]
		[HttpPost]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);

			if (user == null) return Unauthorized("Invalid username");

			using var hmac = new HMACSHA512(user.PasswordSalt);

			var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

			for (int i = 0; i < computeHash.Length; i++)
			{
				if (computeHash[i] != user.PasswordHash[i])
					return Unauthorized("Invalid password");
			}

			var userDto = new UserDto
			{
				Username = loginDto.Username,
				Token = tokenService.CreateToken(user),
			};

			return userDto;
		}

		private async Task<bool> UserExists(string username)
		{
			return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
		}
	}
}
