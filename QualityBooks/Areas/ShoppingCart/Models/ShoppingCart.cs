using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using QualityBooks.Data;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace QualityBooks.Areas.ShoppingCart.Models
{
    public class ShoppingCart
    {
        public String ShoppingCartID { get; set; }
        public const string CartSessionKey = "cartID";

        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartID = cart.GetCartId(context);
            return cart;
        }

        public void AddToCart(Books books, ApplicationDbContext db)
        {
            var cartItem = db.CartItems.SingleOrDefault(c => c.CartID == ShoppingCartID && c.Books.ID == books.ID);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Books = books,
                    CartID = ShoppingCartID,
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
            var cartItem = db.CartItems.SingleOrDefault(cart => cart.CartID == ShoppingCartID && cart.Books.ID == id);
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

        public void EmptyCart(ApplicationDbContext db)
        {
            var cartItems = db.CartItems.Where(cart => cart.CartID == ShoppingCartID);
            foreach (var cartItem in cartItems)
            {
                db.CartItems.Remove(cartItem);
            }

            db.SaveChanges();
        }

        public List<CartItem> GetCartItems(ApplicationDbContext db)
        {
            List<CartItem> cartItems = db.CartItems.Include(c => c.Books)
                .Where(CartItem => CartItem.CartID == ShoppingCartID).ToList();
            return cartItems;
        }

        public int GetCount(ApplicationDbContext db)
        {
            int? count = (from cartItems in db.CartItems where cartItems.CartID == ShoppingCartID select (int?) cartItems.Count).Sum();
            return count ?? 0;
        }

        public decimal GetTotal(ApplicationDbContext db)
        {
            decimal?
                total = (from cartItems in db.CartItems where cartItems.CartID == ShoppingCartID 
                    select(int ?)cartItems.Count * cartItems.Books.Price).Sum();
            return total ?? decimal.Zero;

        }

        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString(CartSessionKey) == null)
            {
                Guid tempCartID = Guid.NewGuid();
                context.Session.SetString(CartSessionKey, tempCartID.ToString());
            }

            return context.Session.GetString(CartSessionKey);
        }
    }
}

        


 







            
       
    

