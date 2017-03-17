namespace BookshopSystem
{
    using System;
    using BookShopSystem.Data;
    using System.Linq;
    using System.Collections.Generic;
    using BookShopSystem.Models;
    using System.Globalization;
    using EntityFramework.Extensions;
    using System.Data.SqlClient;

    public class Startup
    {
        static void Main()
        {
            var context = new BookSystemDbContext();

            #region 01. Books Titles By Age Restriction
            //BookTitlesByAgeRestriction(context);
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
            #region 11. Find Profit
            //FindProfit(context);
            #endregion
            #region 12. Most Recent Books
            //MostRecentBooks(context);
            #endregion
            #region 13. Increase Book Copies
            //IncreaseBookCopies(context);
            #endregion
            #region 14. Remove Books
            //RemoveBooks(context);
            #endregion
            #region 15. Stored Procedure
            //StoredProcedure(context);
            #endregion
        }

        private static void CallAStoredProcedure(BookSystemDbContext context)
        {
            var nameArgs = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameArgs[0];
            var lastName = nameArgs[1];

            SqlParameter firstNameParameter = new SqlParameter("@FirstName", firstName);
            SqlParameter lastNameParameter = new SqlParameter("@LastName", lastName);
            var totalBooks = context.Database
                .SqlQuery<int>("exec usp_GetToalNumberOfBooksForAuthor @FirstName, @LastName",
                    firstNameParameter,
                    lastNameParameter);

            Console.WriteLine($"{firstName} {lastName} has written {totalBooks} books");
        }

        private static void StoredProcedure(BookSystemDbContext context)
        {
            var nameArgs = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameArgs[0];
            var lastName = nameArgs[1];

            SqlParameter firstNameParameter = new SqlParameter("@FirstName", firstName);
            SqlParameter lastNameParameter = new SqlParameter("@LastName", lastName);
            var totalBooks = context.Database
                .SqlQuery<int>("exec usp_GetToalNumberOfBooksForAuthor @FirstName, @LastName",
                    firstNameParameter,
                    lastNameParameter)
                .FirstOrDefault();

            Console.WriteLine($"{firstName} {lastName} has written {totalBooks} books");
        }

        private static void RemoveBooks(BookSystemDbContext context)
        {
            var booksForDelete = context.Books.Where(b => b.Copies < 4200).ToList();

            int totalNumberOfBooksDeleted = context.Books.Where(b => b.Copies < 4200).Delete();
            context.SaveChanges();

            Console.WriteLine($"{totalNumberOfBooksDeleted} books were deleted");
        }

        private static void IncreaseBookCopies(BookSystemDbContext context)
        {
            int totalBooksUpdated = context.Books
                .Where(b => b.ReleaseDate > new DateTime(2013, 06, 06))
                .Update(b => new Book { Copies = b.Copies + 44 });
            context.SaveChanges();

            int totalCopiesAdded = 44 * totalBooksUpdated;
            Console.WriteLine(totalCopiesAdded);
        }

        private static void MostRecentBooks(BookSystemDbContext context)
        {
            var result = context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    TotalBooks = c.Books.Count,
                    Books = c.Books.OrderByDescending(b => b.ReleaseDate)
                        .ThenBy(b => b.Title)
                        .Take(3)
                        .Select(b => new
                        {
                            Title = b.Title,
                            ReleaseDate = b.ReleaseDate
                        })
                })
                .Where(c => c.TotalBooks > 35)
                .OrderByDescending(c => c.TotalBooks)
                .ToList();

            foreach (var category in result)
            {
                Console.WriteLine($"--{category.Name}: {category.TotalBooks} books");
                foreach (var book in category.Books)
                {
                    Console.WriteLine($"{book.Title} ({book.ReleaseDate.Year})");
                }
            }
        }

        private static void FindProfit(BookSystemDbContext context)
        {
            var result = context.Categories.Select(c => new
            {
                Name = c.Name,
                TotalProfit = c.Books.Sum(b => b.Price * b.Copies)
            })
            .OrderByDescending(b => b.TotalProfit)
            .ThenBy(c => c.Name)
            .ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Name} - ${item.TotalProfit:F2}");
            }
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

        private static void BookTitlesByAgeRestriction(BookSystemDbContext context)
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
