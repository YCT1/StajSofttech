using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace RepositoryDesignPattern
{
    // Generic Reposity Interface
    public interface IRepository<TEntity> where TEntity : class
    {
        // For finding the objects
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        // For the adding the objects
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entites);

        // For the removing the objects
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entites);

    }
}
