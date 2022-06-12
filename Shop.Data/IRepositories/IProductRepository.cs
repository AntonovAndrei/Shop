using Shop.Core.Entities;

namespace Shop.Data.IRepositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        void Update(Product product);
        Task DeleteAsync(int id);
        IList<Product> GetAll();
        Product GetById(int id);
        IList<Product> GetProductBySaleId(int saleId);
        int Count();
    }
}
