using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualityBooks.Data;
using QualityBooks.Models;
using Remotion.Linq.Clauses;

namespace QualityBooks.Areas.Catalogue.Controllers
{
    [Area("Catalogue")]
    [Authorize(Roles= "Admin")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public BooksController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [AllowAnonymous]
        // GET: Books
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;

            }

            ViewData["CurrentFilter"] = searchString;

            var books = from b in _context.Books select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                if (Decimal.TryParse(searchString, out var price))
                {
                    books = books.Where(b => b.Price == price);
                }
                else
                {
                    books = books.Where(b => b.Title.Contains(searchString));
                }
            }

            int pageSize = 3;
            return View(await PaginatedList<Book>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        
        [AllowAnonymous]
        // GET: Books
        public async Task<IActionResult> IndexByCategory(int categoryId)
        {
            var books = from b in _context.Books where b.CategoryId == categoryId select b;
            int pageSize = 3;
            return View("Index", await PaginatedList<Book>.CreateAsync(books.AsNoTracking(),1, pageSize));
        }

        [AllowAnonymous]
        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.Include(b => b.Supplier)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        
        public IActionResult Create()
        {
            ViewBag.Suppliers = PopulateSuppliers();
            ViewBag.Categories = PopulateCategories();
            return View();
        }

        private SelectList PopulateCategories(int categoryId=0)
        {
            var categoryQuery = _context.Category.OrderBy(x => x.Name);
            return new SelectList(categoryQuery.AsNoTracking(),"Id", "Name", categoryId);
        }

        private SelectList PopulateSuppliers(int bookSupplierId=0)
        {
            var supplierQuery = _context.Suppliers.OrderBy(x => x.SupplierName);
            return new SelectList(supplierQuery.AsNoTracking(),"Id","SupplierName",bookSupplierId);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Price,Image,SupplierId,CategoryId,BookImage")] Book book)
        {
            if (ModelState.IsValid)
            {
                await SaveImage(book);

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        private async Task SaveImage(Book book)
        {
            if (book.BookImage != null && book.BookImage.Length > 0)
            {
                var imagePath = _env.WebRootPath + "\\images\\Books\\";
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                var fileName = imagePath + book.BookImage.FileName;
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    await book.BookImage.CopyToAsync(stream);
                    book.Image = "~/images/Books/" + book.BookImage.FileName;
                }
            }
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            ViewBag.Suppliers = PopulateSuppliers(book.SupplierId);
            ViewBag.Categories = PopulateCategories(book.CategoryId);

            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,BookImage,SupplierId,CategoryId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await SaveImage(book);
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            _context.Books.Remove(book);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                TempData["BookUsed"] =
                    "The book being deleted has been used in previous orders. Delete those orders before trying again.";
                return RedirectToAction("Delete");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
