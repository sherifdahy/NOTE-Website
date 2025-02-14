using DAL.Data;
using DAL.Repository;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Entities.Models.Receipt;
using ETA.eReceipt.IntegrationToolkit;
using ETA.eReceipt.IntegrationToolkit.Application.Dtos;
using ETA.eReceipt.IntegrationToolkit.Application.Interfaces;
using ETA.eReceipt.IntegrationToolkit.Application.Services;
using ETA.eReceipt.IntegrationToolkit.Infrastructure.Services;
using Interface.Services.ApiCall;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            builder.Services.AddIdentity<applicationUser, IdentityRole<int>>(option =>
                option.Password.RequireDigit = true
                )
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IGenericRepo<Product>, GenericRepository<Product>>();
            builder.Services.AddScoped<IGenericRepo<receipt>, GenericRepository<receipt>>();

            builder.Services.AddTransient<IApiCall, ApiCall>();


            builder.Services
            .AddToolkit(builder.Configuration, ServiceLifetime.Transient);

            builder.Services.AddScoped<IToolkitHandler, ToolkitHandler>();

            builder.Services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new ItemVMModelBinderProvider());
            });

            builder.Services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new TaxTotalVMModelBinderProvider());
            });


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
