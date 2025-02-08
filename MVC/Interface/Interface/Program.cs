using DAL.Data;
using DAL.Repository;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Interface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            //  built in service need to regiser 
            builder.Services.AddDbContext<ApplicationDbContext>
                (option => option.UseSqlServer(builder.Configuration.GetConnectionString("online")));


            // custom service need to register
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(option =>
                option.Password.RequireDigit = true
                )
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IGenericRepo<Product>, GenericRepository<Product>>();




            var app = builder.Build();




            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");



            app.Run();
        }
    }
}
