using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.ChainOfResponsibility.Models;


namespace WebApp.ChainOfResponsibility;


public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
        => Configuration = configuration;



    public void ConfigureServices(IServiceCollection services)
    {

        services.AddDbContext<AppIdentityDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
        });

        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<AppIdentityDbContext>();




        services.AddControllersWithViews();
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
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}