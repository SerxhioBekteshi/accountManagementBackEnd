using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategories();
        public Task<Category> GetCategory(int id);
        public  void CreateCategory(Category category);
        public  void UpdateCategory(Category category);
        public  void DeleteCategory(Category category);

    }
}
