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
                        Supplier = supplier, Category = arts, Description = "The book allows you to relax using color by focusing on art and be inspired by the beautiful product accomplished at the end.", Title = "Color Therapy",
                        Image = "~/images/Books/172556246.jpg", Price = 1.00m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = arts, Description = "The world of art will show how through time influences have shaped what and how we see art.This book will follow and show in detail why this is important in the world we live n today.",
                        Title = "Art that changed the world", Image = "~/images/Books/ArtThatChangedTheWorld.jpg",
                        Price = 20m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = music, Description = "David Bowie was both ahead of he's time and influential musician in the music industry.This book takes a close look at what shaped the artist and how it came to be.",
                        Title = "Biography of Bowie",
                        Image = "~/images/Books/Biography of Bowie.jpg", Price = 34m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = music, Description = "Red Hot Chilli Peppers is still regarded as a band which shaped music and has a large following, whilst having the ability to sell out concerts worldwide. Follow what shaped the lead singer and he's background",
                        Title = "Biography of the Red Hot Chilli Peppers singer",
                        Image = "~/images/Books/Biography of the Red Hot Chilli Peppers singer.jpg", Price = 4m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = business, Description = "You can discover some business ideas and what it takes to test them with this book. Learn what principles and tatics the professionals employ to help business succeed",
                        Title = "Testing business ideas", Image = "~/images/Books/Business ideas.jpg", Price = 44m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = business, Description = "This book follows the story of a kiwi dream and the success of a company which grew from humble beginnings. Look at the ideas and aspirations behind a New Zealand business as told by the founder ",
                        Title = "The barefoot investor", Image = "~/images/Books/Business.jpg", Price = 46m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = sports, Description = "A book which covers some of the highs and lows of a long history of a world cup ruby captain. It gives an in depth insight into what it takes to play and captain a team to win the trophy twice and be known as one of the greatest all blacks.", Title = "Chasing great",
                        Image = "~/images/Books/Rugby New Zealand.jpg", Price = 46m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = sports, Description = "Sport can be a world which requires a mind set and hurdles to overcome, but also how you cope with the highs of winning but the lows of losing. Read up on how the professionals cope with stress and having an ability to face such problems in order to be the best in their field on the big world stage for success",
                        Title = "Sport and it's Challenges", Image = "~/images/Books/Sport and it's Challenges.jpg",
                        Price = 46m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = maori, Description = "New Zealand is unique and celebrates the language of Maori.This book will help new comers understand the unique language of Maori who came to New Zealand and made it home",
                        Title = "Moari proverbs",
                        Image = "~/images/Books/Moari proverbs.jpg", Price = 60m
                    },
                    new Book
                    {
                        Supplier = supplier, Category = maori, Description = "This book is unique for you to read about when it comes to understanding what encapsulates Maori traditions. If think of New Zealand then culturally the haka springs to mind, but that is only part of Maori culture.",
                        Title = "Moari Tradition",
                        Image = "~/images/Books/Moari Tradition.jpg", Price = 56m
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