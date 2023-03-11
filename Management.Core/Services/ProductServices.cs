using Management.Common.Exception;
using Management.Core.Models.Product;
using Management.Data;
using Management.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Core.Services
{
    public interface IProductServices
    {
        Task<List<ProductModels>> GetAllProduct(CancellationToken cancellationToken);
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

        public async Task<List<ProductModels>> GetAllProduct(CancellationToken cancellationToken)
        {
            var queryProduct = await _context.Products
                .Include(x => x.UserProducts)
                .ThenInclude(x => x.User)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = queryProduct.Select(p => new ProductModels
            {
                ProductId = p.Id,
                Name = p.Name,
                Code = p.Code,
                Active = p.Active,
                Description = p.Description,
                CreateBy = p.CreateBy,
                CreateDate = p.CreateDate,
                ModifiedBy = p.ModifiedBy,
                ModifiedDate = p.ModifiedDate,
                UserId = p.UserProducts.FirstOrDefault(up => up.UserId != null)?.UserId ?? Guid.Empty
            }).ToList();

            return result;
        }

    }
}
