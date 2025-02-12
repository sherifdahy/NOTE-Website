using Entities.ConfigurationSettings;
using Entities.Models;
using Entities.Models.Receipt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext
        <applicationUser,IdentityRole<int>,int>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        //public override int SaveChanges()
        //{
        //    var entities = from e in ChangeTracker.Entries()
        //                   where e.State == EntityState.Modified ||
        //                   e.State == EntityState.Added
        //                   select e.Entity;

        //    foreach(var entity in entities)
        //    {
        //        ValidationContext validationContext = new ValidationContext(entity);
        //        Validator.ValidateObject(entity, validationContext,true);

        //    }
        //    return base.SaveChanges();
        //}

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<receipt> Receipts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int> { Id =1, Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole<int> { Id =2, Name = "User", NormalizedName = "USER" }
            );
            base.OnModelCreating(builder);
        }
    }
}
