using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entities.InterfacesOfRepo
{
    public interface IGenericRepo <T>  where T : class
    {
        public IEnumerable<T> GetAll();
        public void Delete(T entity);
        T UpdateById(T entity);
        void Insert(T entity);
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes, int skip = 0, int take = 10);
        public int Count();

        public T GetById(int id);

        public void DeleteAll(Expression<Func<T, bool>> expression);



    }
}
