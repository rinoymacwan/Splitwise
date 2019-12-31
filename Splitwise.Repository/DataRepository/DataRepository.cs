using Microsoft.EntityFrameworkCore;
using Splitwise.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Splitwise.DomainModel.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Splitwise.Repository.DataRepository
{
    public class DataRepository: IDataRepository
    {
        SplitwiseContext context;
        //DbSet<T> dbSet;
        //Implementations Left
        //Any
        public DataRepository(SplitwiseContext _context)
        {
            context = _context;
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : class
        {
            return await CreateDbSet<T>().AddAsync(entity);
        }

        public EntityEntry<T> Add<T>(T entity) where T : class
        {
            return CreateDbSet<T>().Add(entity);
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await CreateDbSet<T>().AddRangeAsync(entities);
        }

        public EntityState Entry<T>(T entity) where T : class
        {
            return context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> FindAsync<T>(int id) where T : class
        {
            return await CreateDbSet<T>().FindAsync(id);
        }

        public async Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await CreateDbSet<T>().Where(predicate).FirstAsync();
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return CreateDbSet<T>().Where(predicate).FirstOrDefault();
        }

        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await CreateDbSet<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return  CreateDbSet<T>().AsQueryable();
        }

        public void Remove<T>(T entity) where T : class
        {
            CreateDbSet<T>().Remove(entity);
        }

        public void RemoveRange<T>(IEnumerable<T> entity) where T : class
        {
            CreateDbSet<T>().RemoveRange(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return CreateDbSet<T>().Where(predicate);
        }


        //var dbSet = CreateDbSet<T>();
        //return dbSet.FindAsync(id);



        public DbSet<T> CreateDbSet<T>() where T : class
        {
            return context.Set<T>();
        }
    }
}
