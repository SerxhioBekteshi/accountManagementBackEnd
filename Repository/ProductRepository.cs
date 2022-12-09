using Contracts;
using Entities;
using Entities.DTO;
using Entities.Models;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public void CreateProduct(Product product)
        {
            Create(product);
        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
         FindAll().OrderBy(e => e.Name).ToList();

        public async Task<IEnumerable<Product>> GetAllProductsAsyncPerCategory(int categoryId) =>
                FindByCondition(e => e.CategoryId == categoryId).OrderBy(e => e.Name).ToList();


        public async Task<Product> GetProductAsync(int id) =>
            FindByCondition(e => e.Id == id).SingleOrDefault();

       public void UpdateProduct(Product product)
       {
            Update(product);
       }

        public async Task<bool> UploadImage(int id, ProductFileUploadDto productFileDto)
        {

            var existingProduct = await GetProductAsync(id);

            try
            {
                if (productFileDto.image is not null)
                {
                    using (var ms = new MemoryStream())
                    {
                        productFileDto.image.CopyTo(ms);
                        existingProduct.image = Convert.ToBase64String(ms.ToArray());
                    }
                }
                else
                    existingProduct.image = null;

                existingProduct.DateModified = DateTime.Now;
                Update(existingProduct);

                return true;
                
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }


    }
}
