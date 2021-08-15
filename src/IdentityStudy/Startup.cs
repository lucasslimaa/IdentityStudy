using IdentityStudy.Areas.Identity.Data;
using IdentityStudy.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityStudy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddMvc().AddMvcOptions(op => op.EnableEndpointRouting=false);

            services.AddDbContext<IdentityStudyContext>(options =>
                   options.UseSqlServer(
                       Configuration.GetConnectionString("IdentityStudyContextConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<IdentityStudyContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(name: "CanDelete", configurePolicy: policy => policy.RequireClaim("CanDelete"));

                options.AddPolicy(name: "CanRead", configurePolicy: policy => policy.Requirements.Add(new RequiredPermissions(permission: "CanRead")));
                options.AddPolicy(name: "CanWrite", configurePolicy: policy => policy.Requirements.Add(new RequiredPermissions(permission: "CanWrite")));
            });

            services.AddSingleton<IAuthorizationHandler,RequiredPermissionsHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
