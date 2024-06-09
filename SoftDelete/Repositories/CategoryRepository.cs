using Microsoft.EntityFrameworkCore;
using SoftDelete.Data;
using SoftDelete.Models.Entities;

namespace SoftDelete.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id)!;
        }

        public void Add(Category category)
        {
            category.RegistrationDate = DateTime.Now;
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            var categoryFind = _context.Categories.Find(category.Id);
            categoryFind!.Title = category.Title;
            categoryFind.EditedDate = DateTime.Now;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Categories
                                    .Include(p => p.Products)
                                    .FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                foreach (var product in category.Products!)
                {
                    product.IsDeleted = true;
                    product.DeletedDate = DateTime.Now;
                }
                category.IsDeleted = true;
                category.DeletedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public bool Exists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        public void Restore(int id)
        {
            var category = _context.Categories
                                    .IgnoreQueryFilters()
                                    .Include(p => p.Products)
                                    .FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                foreach (var product in category.Products!)
                {
                    product.IsDeleted = false;
                    product.DeletedDate = null;
                }
                category.IsDeleted = false;
                category.DeletedDate = null;
                _context.SaveChanges();
            }
        }
    }
}
