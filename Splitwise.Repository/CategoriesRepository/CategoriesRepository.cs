using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;

namespace Splitwise.Repository.CategoriesRepository
{
    public class CategoriesRepository : ICategoriesRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IMapper _mapper;

        public CategoriesRepository(SplitwiseContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }
        public bool CategoryExists(int id)
        {
            return context.Categories.Any(e => e.Id == id);
        }

        public void CreateCategory(Categories Category)
        {
            context.Categories.Add(Category);
        }

        public async Task DeleteCategory(CategoriesAC Category)
        {
            var x = await context.Categories.FindAsync(Category.Id);
            context.Categories.Remove(x);
        }

        public void Dispose()
        {
            
        }

        public IEnumerable<CategoriesAC> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoriesAC>>(context.Categories);
        }

        public async Task<CategoriesAC> GetCategory(int id)
        {
            return _mapper.Map<CategoriesAC>(await context.Categories.FindAsync(id));
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void UpdateCategory(Categories Category)
        {
            context.Entry(Category).State = EntityState.Modified;
        }
    }
}
