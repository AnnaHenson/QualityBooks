using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using QualityBooks.Data;

namespace QualityBooks.Areas.ShoppingCart.Models
{
    public class ShoppingCart
    {
        public String ShoppingcartID { get; set; }
        public const string CartSessionKey = "cartID";

        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingcartID = cart.GetCartID(context);
            return cart;
        }

        public void AddToCart(Books books, ApplicationDbContext db)
        {
            var cartItem = db.CartItems.SingleOrDefault(c => c.CartID == ShoppingcartID && c.Books.ID == books.ID);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Books = books,
                    CartID = ShoppingcartID,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                db.CartItems.Add(cartItem);
            }
            else 
            {
                cartItem.Count++;
            }
            db.SaveChanges();
        }

        public int RemoveFromCart(int id, ApplicationDbContext db)
        {
            var cartItem = db.CartItems.SingleOrDefault(cart => cart.cartID == ShoppingCartID && cart.Books.ID);
            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.CartItems.Remove(cartItem);
                }

                db.SaveChanges();
            }

            return itemCount;
        }

        public void EmptyCart(ApplicationContext db)
        {
            var cartItems = db.CartItems.where(cart => cart.CartID == ShoppingcartID);
            foreach (var cartItem in cartItems)
            {
                db.CartItems.Remove(cartItem);
            }

            db.Savechanges();
        }

        public List<CartItem> GetCartItems(ApplicationDbContext db)
        {
            List<CartItem> cartItems = db.CartItems.Include(c => c.Books)
                .Where(CartItem => CartItem.CartID == ShoppingcartID).ToList();
            return cartItems;
        }

        public int GetCount(ApplicationDbContext db)
        {
            int? count = (from)
        }

    }

}





            
       
    

