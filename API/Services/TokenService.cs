using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace API.Services
{
	public class TokenService(IConfiguration config) : ITokenService
	{
		public string CreateToken(AppUser user)
		{
			var tokenKey = config["tokenKey"] ?? throw new Exception("Can not access token key from App settings.");
			if (tokenKey.Length < 64) throw new Exception("Your token key needs to be longer.");

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

			var claims = new List<Claim>
			{
				new(ClaimsIdentity.DefaultNameClaimType, user.UserName),
			};

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(3),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
