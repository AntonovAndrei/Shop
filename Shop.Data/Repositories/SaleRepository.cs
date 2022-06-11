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
    public class SaleRepository: ISaleRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<SaleRepository> _logger;

        public SaleRepository(ShopContext context, ILogger<SaleRepository> logger)
        {
            _logger = logger;
            _context = context;
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
                throw new Exception("An entity with this id was not found");
            }

            _context.Remove(sale);
            await _context.SaveChangesAsync();
        }

        public IList<Sale> GetAll()
        {
            return _context.Sales.ToList();
        }

        public Sale GetById(int id)
        {
            var sale = _context.Sales.FirstOrDefault(e => e.Id == id);

            if (sale == null)
            {
                _logger.LogInformation($"No sale with {id} id");
                throw new Exception("An entity with this id was not found");
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
