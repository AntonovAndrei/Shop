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
    public class BuyerRepository : IBuyerRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<BuyerRepository> _logger;

        public BuyerRepository(ShopContext context, ILogger<BuyerRepository> logger)
        {
            _logger = logger;
            _context = context;
        }


        public async Task AddAsync(Buyer buyer)
        {
            await _context.AddAsync(buyer);
            await _context.SaveChangesAsync();
        }

        public int Count()
        {
            return _context.Buyers.Count();
        }

        public async Task DeleteAsync(int id)
        {
            var buyer = await _context.Buyers.FindAsync(id);

            if (buyer == null)
            {
                _logger.LogInformation($"No buyer with {id} id");
                throw new Exception("An buyer with this id was not found");
            }

            _context.Remove(buyer);
            await _context.SaveChangesAsync();
        }

        public IList<Buyer> GetAll()
        {
            return _context.Buyers.ToList();
        }

        public Buyer GetById(int id)
        {
            var buyer = _context.Buyers.FirstOrDefault(e => e.Id == id);

            if (buyer == null)
            {
                _logger.LogInformation($"No buyer with {id} id");
                throw new Exception("An buyer with this id was not found");
            }

            return buyer;
        }

        public async Task UpdateAsync(Buyer buyer)
        {
            try
            {
                _context.Buyers.Update(buyer);
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
