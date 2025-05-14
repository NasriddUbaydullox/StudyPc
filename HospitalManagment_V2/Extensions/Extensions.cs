using System.Reflection;
using System.Text;
using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.JwtSetting;
using HospitalManagment_V2.Middleware;
using HospitalManagment_V2.Repository;
using HospitalManagment_V2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace HospitalManagment_V2.Extansions
{

    public static class Extensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {



            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes()
                .Where(r => r.IsClass && r.Name.EndsWith("Repository"))
                .ToList();

            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces()
                    .FirstOrDefault(r => r.Name.EndsWith("Repository"));

                services.AddScoped(interfaceType, type);
            }

            return services;
        }

        

        public static IServiceCollection AddMonitoring(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<Context>(options =>
            {
                options
                    .UseNpgsql("Server=localhost;Port=5432;Database=HospitalManagmentV2;User Id=postgres;Password=postgres;")
                    .LogTo(Console.WriteLine, LogLevel.Information);
            });
            return services;
        }

		public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Description = "JWT Authorization header using the Bearer scheme. \n\r Enter 'Bearer' [space] and then your token in the text input below.\n\r Example: \"Bearer 12345abcdef\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement()
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					}, []
				}
			});
			});

			return services;
		}

		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.Audience = "cafe.uz";

					var signInKey = configuration["Jwt:Key"];

					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidateIssuerSigningKey = true,
						ValidAudiences = ["cafe.uz", "mobile.cafe.uz"],
						ValidIssuers = ["cafe.uz"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signInKey))
					};
				});

			return services;
		}

		public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddOptions<JwtSettings>()
				.BindConfiguration("Jwt");

			return services;
		}
	}
}
