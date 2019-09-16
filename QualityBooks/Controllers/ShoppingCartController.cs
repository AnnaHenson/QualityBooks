using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Operators;
using QualityBooks.Areas.ShoppingCart.Models;
using QualityBooks.Data;
using SQLitePCL;

namespace QualityBooks.Controllers
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
            var cart = ShoppingCart.GetCart(this.HttpContext);
            return View(cart);

        }

        public ActionResult AddToCart(int id)
        {
            // retrieve album from the database.
            var addedBook = _context.Books.Single(book => book.Id == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedBook, _context);
            // Go back to the main store page for more books
            return RedirectToAction("Index", "Books");
        }

        public ActionResult RemoveFromCart(int id);
    

        
    }
}



    

    








       

    
        

        
        

        
   



        

        








 

        

        
    

    








    



    

