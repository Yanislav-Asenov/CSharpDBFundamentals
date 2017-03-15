namespace BookshopSystem
{
    using System;
    using BookShopSystem.Data;
    using System.Linq;
    using System.Collections.Generic;
    using BookShopSystem.Models;

    public class Startup
    {
        static void Main()
        {
            var context = new BookSystemDbContext();

            #region 01. Books Titles By Age Restriction
            //BooksTitlesByAgeRestriction(context);
            #endregion
            #region 01. Golden Books
            //PrintGoldenBooks(context);
            #endregion
            #region 02. Books By Price
            //BooksByPrice(context);
            #endregion
            #region 03. Not Released Books
            //PrintNotReleasedBooks(context);
            #endregion
            #region 04. Book Titles By Category
            BookTitlesByCategory(context);
            #endregion
        }

        private static void BookTitlesByCategory(BookSystemDbContext context)
        {
            var categories = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var resultCategories = context.Categories
                .Where(c => categories.Contains(c.Name))
                .Select(c => new
                {
                    Name = c.Name,
                    Books = c.Books
                })
                .ToList();

            foreach (var category in resultCategories)
            {
                foreach (var book in category.Books.OrderBy(b => b.Id))
                {
                    Console.WriteLine(book.Title);
                }
            }
        }

        private static void PrintNotReleasedBooks(BookSystemDbContext context)
        {
            int inputYear = int.Parse(Console.ReadLine());
            var notReleasedBooks = context.Books
                .Select(b => new
                {
                    b.Title,
                    b.ReleaseDate
                })
                .Where(b => b.ReleaseDate.Year != inputYear)
                .ToList();

            notReleasedBooks.ForEach(b => Console.WriteLine(b.Title));
        }

        private static void BooksByPrice(BookSystemDbContext context)
        {
            var booksByPrice = context.Books
                .Where(b => b.Price < 5 || b.Price > 40)
                .Select(b => new
                {
                    BookId = b.Id,
                    BookTitle = b.Title,
                    BookPrice = b.Price
                })
                .OrderBy(b => b.BookId)
                .ToList();

            booksByPrice.ForEach(b => Console.WriteLine($"{b.BookTitle} - ${b.BookPrice:F2}"));
        }

        private static void PrintGoldenBooks(BookSystemDbContext context)
        {
            var goldenBooks = context.Books
                .Where(b => b.Edition == "Gold" && b.Copies < 5000)
                .Select(b => new
                {
                    b.Title
                })
                .ToList();

            goldenBooks.ForEach(b => Console.WriteLine(b.Title));
        }

        private static void BooksTitlesByAgeRestriction(BookSystemDbContext context)
        {
            string targetRestriction = Console.ReadLine();
            var bookTitles = context.Books
                .Where(b => 
                    b.AgeRestriction
                    .Equals(targetRestriction, StringComparison.InvariantCultureIgnoreCase)
                )
                .Select(b => new
                {
                    b.Title,
                    b.AgeRestriction
                })
                .ToList();

            bookTitles.ForEach(b => Console.WriteLine(b.Title));
        }
    }
}
