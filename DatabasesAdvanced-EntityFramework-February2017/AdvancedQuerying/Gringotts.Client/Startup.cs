namespace Gringotts.Client
{
    using Gringotts.Data;
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            var context = new GringottsDbContext();

            #region 18. Deposits Sum For Ollivander Family
            //DepositSumForOllivanderFamily(context);
            #endregion
            #region 19. Deposists Filter
            //DepositsFilter(context);
            #endregion
        }

        private static void DepositsFilter(GringottsDbContext context)
        {
            var result = context.WizzardDeposits
                .Where(wd => wd.MagicWandCreator == "Ollivander family")
                .GroupBy(wd => wd.DepositGroup, wd => wd.DepositAmount)
                .Where(wd => wd.Sum() < 150000)
                .OrderByDescending(wd => wd.Sum())
                .ToList();

            foreach (var depositGroup in result)
            {
                Console.WriteLine($"{depositGroup.Key} - {depositGroup.Sum()}");
            }
        }

        private static void DepositSumForOllivanderFamily(GringottsDbContext context)
        {
            var result = context.WizzardDeposits
                .Where(wd => wd.MagicWandCreator == "Ollivander family")
                .GroupBy(wd => wd.DepositGroup, wd => wd.DepositAmount)
                .ToList();

            foreach (var depositGroup in result)
            {
                Console.WriteLine($"{depositGroup.Key} - {depositGroup.Sum()}");
            }
        }
    }
}
