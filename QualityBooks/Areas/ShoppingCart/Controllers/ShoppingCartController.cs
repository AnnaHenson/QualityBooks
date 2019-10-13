using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QualityBooks.Data;

namespace QualityBooks.Areas.ShoppingCart.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = Models.ShoppingCart.GetCart(this.HttpContext);
            return View(cart);

        }

        public ActionResult AddToCart(int id)
        {
            // retrieve album from the database.
            var addedBook = _context.Books.Single(book => book.Id == id);
            var cart = Models.ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedBook, _context);
            // Go back to the main store page for more books
            return RedirectToAction("Index", "Books");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cart = Models.ShoppingCart.GetCart(HttpContext);
            cart.RemoveFromCart(id, _context);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ActionResult ClearCart()
        {
            var cart = Models.ShoppingCart.GetCart(HttpContext);
            cart.EmptyCart(_context);
            return RedirectToAction("Index", "Books");
        }
    

        
    }
}



    

    








       

    
        

        
        

        
   



        

        








 

        

        
    

    








    



    

