using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.CategoriesRepository
{
    public interface ICategoriesRepository
    {
        IEnumerable<CategoriesAC> GetCategories();
        Task<CategoriesAC> GetCategory(int id);
        void CreateCategory(Categories Category);
        void UpdateCategory(Categories Category);
        Task DeleteCategory(CategoriesAC Category);
        Task Save();
        bool CategoryExists(int id);
    }
}
