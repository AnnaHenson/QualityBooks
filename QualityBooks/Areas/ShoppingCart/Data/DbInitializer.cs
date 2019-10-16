using System.Collections.Generic;
using System.Linq;
using QualityBooks.Data;
using QualityBooks.Models;

namespace QualityBooks.Areas.ShoppingCart.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Category.Any())
            {
                var maori = new Category {Name = "Maori Culture"};
                var sports = new Category {Name = "Sports"};
                var music = new Category {Name = "Music"};
                var business = new Category {Name = "Business"};
                var arts = new Category {Name = "Arts"};
                var categories = new List<Category>
                {
                    maori, sports,
                    music, business, arts
                };
                foreach (var category in categories) context.Category.Add(category);

                var supplier = new Supplier
                {
                    Email = "books@books.com", HomeNumber = "111-111-1111", MobileNumber = "021021021",
                    SupplierName = "Fred's books", WorkNumber = "554554"
                };

                context.Suppliers.Add(supplier);


                const string description =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

                var books = new List<Book>
                {
                    new Book
                    {
                        Supplier = supplier, Category = arts, Description = description, Title = "Color Therapy",
                        Image = "~/images/Books/172556246.jpg", Price = 1.00m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = arts, Description = description,
                        Title = "Art that changed the world", Image = "~/images/Books/ArtThatChangedTheWorld.jpg",
                        Price = 2m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = music, Description = description,
                        Title = "Biography of Bowie",
                        Image = "~/images/Books/Biography of Bowie.jpg", Price = 4m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = music, Description = description,
                        Title = "Biography of the Red Hot Chilli Peppers singer",
                        Image = "~/images/Books/Biography of the Red Hot Chilli Peppers singer.jpg", Price = 4m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = business, Description = description,
                        Title = "Testing business ideas", Image = "~/images/Books/Buisiness ideas.jpg", Price = 44m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = business, Description = description,
                        Title = "The barefoot investor", Image = "~/images/Books/Business.jpg", Price = 46m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = sports, Description = description, Title = "Chasing great",
                        Image = "~/images/Books/Rugby New Zealand.jpg", Price = 46m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = sports, Description = description,
                        Title = "Sport and it's Challenges", Image = "~/images/Books/Sport and it's Challenges.jpg",
                        Price = 46m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = maori, Description = description,
                        Title = "Moari proverbs",
                        Image = "~/images/Books/Moari proverbs.jpg", Price = 46m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = maori, Description = description,
                        Title = "Moari Tradition",
                        Image = "~/images/Books/Moari Tradition.jpg", Price = 46m
                    }
                };
                foreach (var book in books)
                {
                    context.Books.Add(book);
                }

                context.SaveChanges();
            }
        }
    }
}