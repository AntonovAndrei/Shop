using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.IRepositories
{
    public interface IBuyerRepository
    {
        Task AddAsync(Buyer buyer);
        Task UpdateAsync(Buyer buyer);
        Task DeleteAsync(int id);
        IList<Buyer> GetAll();
        Buyer GetById(int id);
        int Count();
    }
}
