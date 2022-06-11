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
        public Task AddAsync(Buyer buyer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Buyer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Buyer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Buyer buyer)
        {
            throw new NotImplementedException();
        }
    }
}
