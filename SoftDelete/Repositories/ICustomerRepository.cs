using SoftDelete.Models.Entities;

namespace SoftDelete.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        void Add(Customer product);
        void Update(Customer product);
        void Delete(int id);
        bool Exists(int id);
        void Restore(int id);
    }
}
