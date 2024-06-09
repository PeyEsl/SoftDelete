using SoftDelete.Models.Entities;

namespace SoftDelete.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        bool Exists(int id);
        void Restore(int id);
    }
}
