using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.RequestFeatures;
using Entities.DTO;

namespace Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetAllProductsAsyncPerCategory(int categoryId);
        Task<Product> GetProductAsync(int id);
        void DeleteProduct(Product product);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);

        Task<bool> UploadImage(int id, ProductFileUploadDto productFile);

    }
}
