using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Category
        // Get All
        IEnumerable<T> GetAll();

        // Get One
        T Get(Expression<Func<T, bool>> filter);

        // Add One
        void Add(T entity);

        // Update One <! Removed as it's a generic repository, implemented on specific repository !>
        //void Update(T entity);

        // Delete One
        void Remove(T entity);

        // Delete Multiples
        void RemoveRange(IEnumerable<T> entity);
    }
}
