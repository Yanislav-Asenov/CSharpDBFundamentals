namespace BookshopSystem
{
    using System;
    using BookShopSystem.Data;
    using System.Linq;
    using System.Collections.Generic;
    using BookShopSystem.Models;
    using System.Globalization;

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
            //BookTitlesByCategory(context);
            #endregion
            #region 05. Books Released Before Date
            //BooksReleasedBeforeDate(context);
            #endregion
            #region 06. Authors Search
            //AuthorsSearch(context);
            #endregion
            #region 07. Books Search
            //BooksSearch(context);
            #endregion
            #region 08. Book Titles Search
            //BookTitlesSearch(context);
            #endregion
            #region 09. Count Books
            //CountBooks(context);
            #endregion
            #region 10. Total Book Copies
            //TotalBookCopies(context);
            #endregion
        }

        private static void TotalBookCopies(BookSystemDbContext context)
        {
            var authorsWithTotalNumberOfBookCopies = context.Authors
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    TotalBookCopies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.TotalBookCopies)
                .ToList();

            foreach (var a in authorsWithTotalNumberOfBookCopies)
            {
                Console.WriteLine($"{a.FirstName} {a.LastName} - {a.TotalBookCopies}");
            }
        }

        private static void CountBooks(BookSystemDbContext context)
        {
            var bookTitleTargetLength = int.Parse(Console.ReadLine());
            var booksCount = context.Books.Count(b => b.Title.Length > bookTitleTargetLength);
            Console.WriteLine(booksCount);
        }

        private static void BookTitlesSearch(BookSystemDbContext context)
        {
            var inputString = Console.ReadLine();
            var resultBooks = context.Books
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    AuthorFirstName = b.Author.FirstName,
                    AuthorLastName = b.Author.LastName
                })
                .Where(b => b.AuthorLastName.StartsWith(inputString))
                .ToList();

            foreach (var book in resultBooks.OrderBy(b => b.Id))
            {
                Console.WriteLine(book.Title);
            }
        }

        private static void BooksSearch(BookSystemDbContext context)
        {
            var inputString = Console.ReadLine();
            var resultBookTitles = context.Books
                .Select(b => b.Title)
                .Where(title => title.Contains(inputString))
                .ToList();

            foreach (var title in resultBookTitles)
            {
                Console.WriteLine(title);
            }
        }

        private static void AuthorsSearch(BookSystemDbContext context)
        {
            var inputString = Console.ReadLine();
            var resultAuthors = context.Authors
                .Select(a => new { a.FirstName, a.LastName })
                .Where(a => a.FirstName.EndsWith(inputString))
                .ToList();

            foreach (var a in resultAuthors)
            {
                Console.WriteLine($"{a.FirstName} {a.LastName}");
            }
        }

        private static void BooksReleasedBeforeDate(BookSystemDbContext context)
        {
            var inputDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            var resultBooks = context.Books.Where(b => b.ReleaseDate < inputDate)
                .Select(b => new
                {
                    b.Title,
                    b.Edition,
                    b.Price
                })
                .ToList();

            foreach (var b in resultBooks)
            {
                Console.WriteLine($"{b.Title} - {b.Edition} - {b.Price}");
            }
        }

        private static void BookTitlesByCategory(BookSystemDbContext context)
        {
            var categoryNames = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var resultCategories = new List<Category>();

            var resultCategoriesWithBooks = context.Categories
                .Where(c => categoryNames.Contains(c.Name))
                .SelectMany(c => c.Books.Select(b => new { Id = b.Id, Title = b.Title }))
                .ToList();
                
            foreach (var book in resultCategoriesWithBooks)
            {
                Console.WriteLine(book.Title);
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
