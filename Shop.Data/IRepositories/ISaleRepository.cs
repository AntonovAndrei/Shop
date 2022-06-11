using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.IRepositories
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task DeleteAsync(int id);
        IList<Sale> GetAll();
        Sale GetById(int id);
        IList<Sale> GetByProductId(int productId);
    }
}
