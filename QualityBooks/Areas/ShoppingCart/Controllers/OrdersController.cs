using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QualityBooks.Areas.ShoppingCart.Models;
using QualityBooks.Data;
using QualityBooks.Models;

namespace QualityBooks.Areas.ShoppingCart.Controllers
{
    [Authorize(Roles = "Admin, Member")]
    [Area("ShoppingCart")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ShoppingCart/Orders
        [Authorize(Roles = "Admin, Member")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

 
        // GET: ShoppingCart/Orders/Create
        [Authorize(Roles = "Member")]
        public IActionResult Create()
        {
            return View();
        }


        // POST: ShoppingCart/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,City,PostalCode,Country,Phone")] Order order)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                Models.ShoppingCart cart = Models.ShoppingCart.GetCart(this.HttpContext);
                List<CartItem> items = cart.GetCartItems(_context);
                List<OrderDetail> details = new List<OrderDetail>();
                foreach(CartItem item in items)
                {
                    OrderDetail detail = CreateOrderDetailForThisItem(item);
                    detail.Order = order;
                    details.Add(detail);
                    _context.Add(detail);
                }
                order.User = user;
                order.OrderDate = DateTime.Today;
                order.Total = Models.ShoppingCart.GetCart(this.HttpContext).GetTotal(_context);
                order.GST = order.Total * (decimal) 0.15;
                order.GrandTotal = order.Total + order.GST;
                order.OrderDetails = details;
                
                _context.SaveChanges();

                return RedirectToAction("Purchased", new RouteValueDictionary(new {action ="Purchased", id = order.OrderID}));
                
            }
            return View(order);
        }

        private OrderDetail CreateOrderDetailForThisItem(CartItem item)
        {
            OrderDetail detail = new OrderDetail();
            detail.Quantity = item.Count;
            detail.Book = item.Book;
            detail.UnitPrice = item.Book.Price;
            return detail;
        }
        
       
        public async  Task<ActionResult> Purchased(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(i => i.User).AsNoTracking()
                .SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            var details = _context.OrderDetail.Where(detail => detail.Order.OrderID == order.OrderID)
                .Include(detail => detail.Book).ToList();
            order.OrderDetails = details;
            Models.ShoppingCart.GetCart(this.HttpContext).EmptyCart(_context);
            return View(order);
        }

        // GET: ShoppingCart/Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(i => i.User).AsNoTracking()
                .SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            var details = _context.OrderDetail.Where(detail => detail.Order.OrderID == order.OrderID)
                .Include(detail => detail.Book).ToList();

            order.OrderDetails = details;

            return View(order);
        }

        // POST: ShoppingCart/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderID == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
