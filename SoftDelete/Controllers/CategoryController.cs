using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftDelete.Models.DataTransferObjects;
using SoftDelete.Models.Entities;
using SoftDelete.Repositories;

namespace SoftDelete.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: Category
        public IActionResult Index()
        {
            var category = _categoryRepository.GetAll();
            var categoryList = new List<CategoryDto>();
            foreach (var item in category)
            {
                var categoryDto = new CategoryDto
                {
                    Id = item.Id,
                    Title = item.Title,
                };
                categoryList.Add(categoryDto);
            }
            return View(categoryList);
        }

        // GET: Category/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById((int)id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Title = category.Title,
            };

            return View(categoryDto);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title")] CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Id = categoryDto.Id,
                    Title = categoryDto.Title,
                };
                _categoryRepository.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        // GET: Category/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById((int)id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Title = category.Title,
            };

            return View(categoryDto);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title")] CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = new Category
                    {
                        Id = categoryDto.Id,
                        Title = categoryDto.Title,
                    };
                    _categoryRepository.Update(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(categoryDto.Id))
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
            return View(categoryDto);
        }

        // GET: Category/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById((int)id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Title = category.Title,
            };

            return View(categoryDto);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id != null)
            {
                _categoryRepository.Delete((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _categoryRepository.Exists(id);
        }
    }
}
