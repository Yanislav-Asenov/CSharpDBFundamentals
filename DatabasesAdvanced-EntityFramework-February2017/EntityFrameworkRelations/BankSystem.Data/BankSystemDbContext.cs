namespace BankSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BankSystemDbContext : DbContext
    {
        public BankSystemDbContext()
            : base("name=BankSystemDbContext")
        {
        }
    }
}