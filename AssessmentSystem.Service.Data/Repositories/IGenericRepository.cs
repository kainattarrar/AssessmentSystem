using AssessmentSystem.Service.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Data.Repositories
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        void Add(T entity);
        void Add(IEnumerable<T> entities);
        bool Any(Expression<Func<T, bool>> predicate);
        T Get(int id);
        T Get(long id);
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        void Remove(T entity);
        void Remove(IEnumerable<T> entities);
        void Remove(IQueryable<T> entities);
    }
}
