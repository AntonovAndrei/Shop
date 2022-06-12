using Shop.Core.Entities;

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
