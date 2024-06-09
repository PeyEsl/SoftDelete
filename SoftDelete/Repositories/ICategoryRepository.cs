using SoftDelete.Models.Entities;

namespace SoftDelete.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Add(Category product);
        void Update(Category product);
        void Delete(int id);
        bool Exists(int id);
        void Restore(int id);
    }
}
