using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Core.Entities;
using Shop.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ShopContext context, ILogger<ProductRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                _logger.LogInformation($"No product with {id} id");
                throw new Exception("An product with this id was not found");
            }

            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public IList<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            var product = _context.Products.FirstOrDefault(e => e.Id == id);

            if (product == null)
            {
                _logger.LogInformation($"No product with {id} id");
                throw new Exception("An product with this id was not found");
            }

            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
