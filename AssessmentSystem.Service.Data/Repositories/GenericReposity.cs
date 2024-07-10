using AssessmentSystem.Service.Data.Contexts;
using AssessmentSystem.Service.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Data.Repositories
{
    public class GenericReposity<T> : IGenericRepository<T> where T : class, IEntity
    {
        private DataContext _dataContext;
        public GenericReposity(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Add(T entity)
        {
            _dataContext.Set<T>().Add(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            _dataContext.Set<T>().AddRange(entities);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dataContext.Set<T>().Any(predicate);
        }

        public T Get(int id)
        {
            return _dataContext.Set<T>().Find(id);
        }

        public T Get(long id)
        {
            return _dataContext.Set<T>().Find(id);
        }

        public IQueryable<T> Get()
        {
            return _dataContext.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dataContext.Set<T>().Where(predicate);
        }

        public void Remove(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
        }

        public void Remove(IEnumerable<T> entities)
        {
            _dataContext.Set<T>().RemoveRange(entities);
        }

        public void Remove(IQueryable<T> entities)
        {
            _dataContext.Set<T>().RemoveRange(entities);
        }
    }
}
