using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.DataAccess.Context;

namespace Security.Api.Configuration
{
    public static class IdentityConfiguration
    {

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Security.Api")));

            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AplicationDbContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}
