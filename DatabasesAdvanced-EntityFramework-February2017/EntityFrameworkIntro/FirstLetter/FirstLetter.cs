namespace FirstLetter
{
    using Gringotts.Data;
    using System;
    using System.Linq;
    using System.Text;

    class FirstLetter
    {
        static void Main()
        {
            var dbContext = new GringottsContext();
            var wizzardsFirstLetter = dbContext.WizzardDeposits
                .Where(wd => wd.DepositGroup == "Troll Chest")
                .Select(wd => wd.FirstName.Substring(0, 1))
                .OrderBy(wd => wd)
                .Distinct()
                .ToList();

            StringBuilder content = new StringBuilder();
            wizzardsFirstLetter.ForEach(wd =>
            {
                content.AppendLine(wd);
            });

            Console.WriteLine(content);
        }
    }
}
