using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.IRepositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        IList<Product> GetAll();
        Product GetById(int id);
        IList<Product> GetProductBySaleId(int saleId);
        int Count();
    }
}
