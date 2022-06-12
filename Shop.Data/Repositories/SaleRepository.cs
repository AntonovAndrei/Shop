using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Core.Entities;
using Shop.Data.IRepositories;

namespace Shop.Data.Repositories
{
    public class SaleRepository: ISaleRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<SaleRepository> _logger;

        public SaleRepository(ShopContext context, ILogger<SaleRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public int Count()
        {
            return _context.Sales.Count();
        }

        public IList<Sale> GetByProductId(int productId)
        {
            var sales = _context.Products.Where(e => e.Id == productId)
                .Include(e => e.Sales)
                .Select(e => e.Sales.ToList()).FirstOrDefault();

            if (sales == null)
            {
                _logger.LogInformation($"No sales with {productId} productId");
                return null;
            }

            return sales;
        }

        public async Task AddAsync(Sale sale)
        {
            await _context.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            
            if(sale == null)
            {
                _logger.LogInformation($"No sale with {id} id");
                throw new Exception("An sale with this id was not found");
            }

            _context.Remove(sale);
            await _context.SaveChangesAsync();
        }

        public IList<Sale> GetAll()
        {
            return _context.Sales.Include(e => e.Buyer).ToList();
        }

        public Sale GetById(int id)
        {
            var sale = _context.Sales.Include(e => e.Buyer).FirstOrDefault(e => e.Id == id);

            if (sale == null)
            {
                _logger.LogInformation($"No sale with {id} id");
                throw new Exception("An sale with this id was not found");
            }

            return sale; 
        }

        public async Task UpdateAsync(Sale sale)
        {
            try
            {
                _context.Sales.Update(sale);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
