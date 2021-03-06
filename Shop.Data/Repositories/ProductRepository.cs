using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Core.Entities;
using Shop.Data.IRepositories;

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

        public int Count()
        {
            return _context.Products.Count();
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

            _context.Products.Remove(product);
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
                return null;
            }

            return product;
        }

        public IList<Product> GetProductBySaleId(int saleId)
        {
            var sales = _context.Sales.Where(e => e.Id == saleId)
                .Include(e => e.Products)
                .Select(e => e.Products.ToList()).FirstOrDefault();

            if (sales == null)
            {
                _logger.LogInformation($"No products with {saleId} productId");
                throw new Exception("An products with this productId was not found");
            }

            return sales;
        }

        public void Update(Product product)
        {
            try
            {
                _context.Products.Update(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
