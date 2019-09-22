using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QualityBooks.Areas.ShoppingCart.Models.ShoppingCartViewModels;
using QualityBooks.Data;

namespace QualityBooks.Areas.ShoppingCart.ViewComponents
{
    public class ShoppingCartViewModelComponent:ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartViewModelComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(ReturnCurrentCartViewModel());
        }

        public ShoppingCartViewModel ReturnCurrentCartViewModel()
        {
            var cart = Models.ShoppingCart.GetCart(this.HttpContext);
            //Set up view model
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(_context),
                CartTotal = cart.GetTotal(_context)
            };
            return viewModel;
        }
    }
}