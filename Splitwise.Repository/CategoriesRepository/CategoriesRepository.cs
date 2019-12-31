using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using Splitwise.Repository.DataRepository;

namespace Splitwise.Repository.CategoriesRepository
{
    public class CategoriesRepository : ICategoriesRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IMapper _mapper;
        private readonly IDataRepository dataRepository;

        public CategoriesRepository(SplitwiseContext context, IMapper mapper, IDataRepository _dataRepository)
        {
            this.context = context;
            _mapper = mapper;
            dataRepository = _dataRepository;

        }
        public bool CategoryExists(int id)
        {
            return context.Categories.Any(e => e.Id == id);
        }

        public void CreateCategory(Categories Category)
        {
            dataRepository.Add(Category);
        }

        public async Task DeleteCategory(CategoriesAC Category)
        {
            var x = await dataRepository.FindAsync<Categories>(Category.Id);
            dataRepository.Remove(x);
        }

        public void Dispose()
        {
            
        }

        public IEnumerable<CategoriesAC> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoriesAC>>(dataRepository.GetAll<Categories>());
        }

        public async Task<CategoriesAC> GetCategory(int id)
        {
            return _mapper.Map<CategoriesAC>(await dataRepository.FindAsync<Categories>(id));
        }

        public async Task Save()
        {
            await dataRepository.SaveChangesAsync();
        }

        public void UpdateCategory(Categories Category)
        {
            dataRepository.Entry(Category);
        }
    }
}
