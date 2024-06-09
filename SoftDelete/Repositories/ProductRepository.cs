using SoftDelete.Data;
using SoftDelete.Models.Entities;

namespace SoftDelete.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id)!;
        }

        public void Add(Product product)
        {
            product.RegistrationDate = DateTime.Now;
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            var productFind = _context.Products.Find(product.Id);
            productFind!.Name = product.Name;
            productFind.Price = product.Price;
            productFind.EditedDate = DateTime.Now;
            productFind.CategoryId = product.CategoryId;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public bool Exists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public void Restore(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                product.IsDeleted = false;
                product.DeletedDate = null;
                _context.SaveChanges();
            }
        }
    }
}
