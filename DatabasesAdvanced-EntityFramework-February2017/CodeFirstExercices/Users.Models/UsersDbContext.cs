namespace Users.Models
{
    using System.Data.Entity;

    public class UsersDbContext : DbContext
    {
        public UsersDbContext()
            : base("name=UsersDbContext")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<UsersDbContext>());
        }

        public virtual DbSet<User> Users { get; set; }
    }
}