using Management.Core.Models.Product;
using Management.Data;
using Management.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Core.Services
{
    public interface IProductServices
    {
        Task<Product> CreateNewProduct(CreateProductRequest request, CancellationToken cancellationToken);
    }
    public class ProductServices : IProductServices
    {
        private readonly ManagementDbContext _context;

        public ProductServices(ManagementDbContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateNewProduct(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var newProduct = new Product()
            {
                Name = request.Name,
                Active = request.Active,
                Code = request.Code,
                Description = request.Description,
            };
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }
    }
}
