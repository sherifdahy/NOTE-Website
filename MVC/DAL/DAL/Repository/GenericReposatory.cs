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
        


        public IQueryable<T> FindAll(Expression<Func<T, bool>> criteria,string[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            if(includes != null)
            {
                foreach(string include in includes)
                {
                    query.Include(include);
                }
            }
            return query.Where(criteria);
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

        public void DeleteAll(Expression<Func<T,bool>> expression)
        {
            IQueryable<T> query = context.Set<T>().Where(expression);
            context.Set<T>().RemoveRange(query.ToList());
            
        }

        public int Count()
        {
            return context.Set<T>().Count();
        }




    }
}
