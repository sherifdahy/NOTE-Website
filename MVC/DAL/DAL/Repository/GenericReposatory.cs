using DAL.Data;
using Entities.InterfacesOfRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepo<T> where T : class
    {
        ApplicationDbContext context;
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes)
        {
            IQueryable<T> Query = context.Set<T>();

            if (includes != null)
            {
                foreach (string include in includes)
                {
                    Query.Include(include);
                }
            }

            return Query.Where(criteria);
        }
        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public T UpdateById(T entity)
        {
            context.Set<T>().Update(entity);
            return entity;
        }
    }
}
