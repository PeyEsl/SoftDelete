using Microsoft.EntityFrameworkCore;
using SoftDelete.Data;
using SoftDelete.Models.Entities;

namespace SoftDelete.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return _context.Customers.Find(id)!;
        }

        public void Add(Customer customer)
        {
            customer.RegistrationDate = DateTime.Now;
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            var customerFind = _context.Customers.Find(customer.Id);
            customerFind!.FirstName = customer.FirstName;
            customerFind.LastName = customer.LastName;
            customerFind.NationalCode = customer.NationalCode;
            customerFind.PhoneNumber = customer.PhoneNumber;
            customerFind.EditedDate = DateTime.Now;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }

        public bool Exists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        public void Restore(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                customer.IsDeleted = false;
                customer.DeletedDate = null;
                _context.SaveChanges();
            }
        }
    }
}
