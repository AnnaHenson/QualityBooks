using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QualityBooks.Data;
using QualityBooks.Models;

namespace QualityBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
           ViewBag.Categories= _dbContext.Category.GroupJoin(_dbContext.Books, category => category.Id, book => book.CategoryId,
                (category, books) => new CategoryWithBookCount(category.Id, category.Name, books.Count())).ToList();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
