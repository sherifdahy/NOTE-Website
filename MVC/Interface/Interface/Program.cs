using DAL.Data;
using DAL.Repository;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Entities.Models.Document.BaseComponent;
using Entities.Models.Document.Receipt;
using ETA.eReceipt.IntegrationToolkit;
using ETA.eReceipt.IntegrationToolkit.Application.Dtos;
using ETA.eReceipt.IntegrationToolkit.Application.Interfaces;
using ETA.eReceipt.IntegrationToolkit.Application.Services;
using ETA.eReceipt.IntegrationToolkit.Infrastructure.Services;
using Interface.ApplicationConfiguration;
using Interface.Services.ApiCall;
using Interface.Services.Report;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Interface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(typeof(Program));



            //  built in service need to regiser 
            builder.Services.AddDbContext<ApplicationDbContext>
                (option => option.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("online")));


            // custom service need to register
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(option =>
                option.Password.RequireDigit = true
                )
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IGenericRepo<Product>, GenericRepository<Product>>();
            builder.Services.AddScoped<IGenericRepo<Receipt>, GenericRepository<Receipt>>();

            builder.Services.AddTransient<IApiCall, ApiCall>();


            builder.Services.AddScoped<IServiceReport, ServiceReport>();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddScoped<TokenAttribute>();

            builder.Services.AddScoped<IAppConfig, AppConfigDevelopment>();


            builder.Services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new CommercialDiscountDataVMBinderProvider());
            });

            builder.Services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new TaxableItemVMModelBinderProvider());
            });

            builder.Services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new TaxTotalsVMModelBinderProvider());
            });

            var app = builder.Build();




            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
