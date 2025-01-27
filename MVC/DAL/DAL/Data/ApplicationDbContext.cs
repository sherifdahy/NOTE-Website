using Entities.ConfigurationSettings;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext
        <ApplicationUser,IdentityRole<int>,int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Modified ||
                           e.State == EntityState.Added
                           select e.Entity;

            foreach(var entity in entities)
            {
                ValidationContext validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext,true);

            }
            return base.SaveChanges();
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
