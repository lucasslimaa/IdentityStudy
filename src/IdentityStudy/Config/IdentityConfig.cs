using IdentityStudy.Areas.Identity.Data;
using IdentityStudy.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityStudy.Config
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(name: "CanDelete", configurePolicy: policy => policy.RequireClaim("CanDelete"));

                options.AddPolicy(name: "CanRead", configurePolicy: policy => policy.Requirements.Add(new RequiredPermissions(permission: "CanRead")));
                options.AddPolicy(name: "CanWrite", configurePolicy: policy => policy.Requirements.Add(new RequiredPermissions(permission: "CanWrite")));
            });

            return services;
        }

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<IdentityStudyContext>(options =>
                  options.UseSqlServer(
                      configuration.GetConnectionString("IdentityStudyContextConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<IdentityStudyContext>();

            return services;
        }
    }
}
