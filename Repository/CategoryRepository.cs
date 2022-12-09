using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategories() => FindAll().OrderBy(c => c.Code).ToList();

        public async Task<Category> GetCategory(int id) 
        => await FindByCondition(c => c.Id.Equals(id)).FirstOrDefaultAsync();
       
        public void CreateCategory(Category category) => Create(category);

        public void DeleteCategory(Category category) => Delete(category);

        public void UpdateCategory(Category category) => Update(category);

    }
}
