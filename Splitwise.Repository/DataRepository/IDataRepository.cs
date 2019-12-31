using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.DataRepository
{
    public interface IDataRepository
    {
        //IEnumerable<T> GetAll();
        //Task<T> GetById(object id);
        //void Add(T obj);
        //void Update(T obj);
        //void Delete(object id);
        //void Save();
        //Implementations Left
        //Any
        //
        void Save();
        IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;
        Task<EntityEntry<T>> AddAsync<T>(T entity) where T : class;
        EntityEntry<T> Add<T>(T entity) where T : class;
        Task SaveChangesAsync();
        EntityState Entry<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void RemoveRange<T>(IEnumerable<T> entity) where T : class;
        Task<T> FindAsync<T>(int id) where T : class;
    }
}
