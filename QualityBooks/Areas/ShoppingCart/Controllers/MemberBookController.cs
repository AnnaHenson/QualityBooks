using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualityBooks.Data;
using QualityBooks.Models;

namespace QualityBooks.Areas.ShoppingCart.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "Members")]
    [Area("ShoppingCart")]
    public class MemberBookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberBookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingCart/MemberBook
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Books.Include(b => b.Category).Include(b => b.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShoppingCart/MemberBook/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Supplier)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: ShoppingCart/MemberBook/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName");
            return View();
        }

        // POST: ShoppingCart/MemberBook/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,SupplierId,Image,CategoryId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", book.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName", book.SupplierId);
            return View(book);
        }

        // GET: ShoppingCart/MemberBook/Edit/5
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", book.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName", book.SupplierId);
            return View(book);
        }

        // POST: ShoppingCart/MemberBook/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,SupplierId,Image,CategoryId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", book.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName", book.SupplierId);
            return View(book);
        }

        // GET: ShoppingCart/MemberBook/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Supplier)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: ShoppingCart/MemberBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
