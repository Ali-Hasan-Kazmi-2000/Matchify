using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
		{
			services.AddControllers();
			services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlite(config.GetConnectionString("DefaultConnection"));
			});

			services.AddCors();
			services.AddScoped<ITokenService, TokenService>();

			return services;
		}
	}
}
