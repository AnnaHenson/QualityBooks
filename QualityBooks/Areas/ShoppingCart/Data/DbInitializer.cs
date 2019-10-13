using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualityBooks.Data;
using QualityBooks.Models;

namespace QualityBooks.Areas.ShoppingCart.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Category.Any())
            {
                return;
            }

            var category = new Category{Name = "Maori Culture"};
            context.Category.Add(category);
            context.SaveChanges();
        }
    }
}
