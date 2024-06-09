using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftDelete.Data;
using SoftDelete.Models.DataTransferObjects;
using SoftDelete.Models.Entities;
using SoftDelete.Repositories;

namespace SoftDelete.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: Customer
        public IActionResult Index()
        {
            var customer = _customerRepository.GetAll();
            var customerList = new List<CustomerDto>();
            foreach (var item in customer)
            {
                var customerDto = new CustomerDto
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    NationalCode = item.NationalCode,
                    PhoneNumber = item.PhoneNumber,
                };
                customerList.Add(customerDto);
            }
            return View(customerList);
        }

        // GET: Customer/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customerRepository.GetById((int)id);
            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                NationalCode = customer.NationalCode,
                PhoneNumber = customer.PhoneNumber,
            };

            return View(customerDto);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,NationalCode,PhoneNumber")] CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Id = customerDto.Id,
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    NationalCode = customerDto.NationalCode,
                    PhoneNumber = customerDto.PhoneNumber,
                };
                _customerRepository.Add(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customerDto);
        }

        // GET: Customer/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customerRepository.GetById((int)id);
            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                NationalCode = customer.NationalCode,
                PhoneNumber = customer.PhoneNumber,
            };

            return View(customerDto);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,NationalCode,PhoneNumber")] CustomerDto customerDto)
        {
            if (id != customerDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = new Customer
                    {
                        Id = customerDto.Id,
                        FirstName = customerDto.FirstName,
                        LastName = customerDto.LastName,
                        NationalCode = customerDto.NationalCode,
                        PhoneNumber = customerDto.PhoneNumber,
                    };
                    _customerRepository.Update(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customerDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customerDto);
        }

        // GET: Customer/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customerRepository.GetById((int)id);
            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                NationalCode = customer.NationalCode,
                PhoneNumber = customer.PhoneNumber,
            };

            return View(customerDto);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id != null)
            {
                _customerRepository.Delete((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _customerRepository.Exists(id);
        }
    }
}
