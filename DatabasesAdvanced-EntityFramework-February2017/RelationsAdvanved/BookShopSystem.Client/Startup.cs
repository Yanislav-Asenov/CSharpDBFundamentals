namespace BookShopSystem.Client
{
    using BookShopSystem.Data;
    using System.Data.Entity;
    using System.Linq;

    public class Startup
    {
        static void Main()
        {
            Database.SetInitializer<BookSystemDbContext>(new CreateDatabaseIfNotExists<BookSystemDbContext>());

            var context = new BookSystemDbContext();

            SetupBooksRelatedBooks(context);

            PrintBooksWithRelatedBooks(context);
        }

        private static void SetupBooksRelatedBooks(BookSystemDbContext context)
        {
            var books = context.Books.Take(3).ToList();

            books[0].RelatedBooks.Add(books[1]);
            books[1].RelatedBooks.Add(books[1]);
            books[0].RelatedBooks.Add(books[1]);
            books[2].RelatedBooks.Add(books[1]);

            context.SaveChanges();
        }

        private static void PrintBooksWithRelatedBooks(BookSystemDbContext context)
        {
            var books = context.Books.Include(b => b.RelatedBooks).Take(3).ToList();

            foreach (var book in books)
            {
                System.Console.WriteLine($"--{book.Title}");
                foreach (var relatedBook in book.RelatedBooks)
                {
                    System.Console.WriteLine(relatedBook.Title);
                }
            }
        }
    }
}
