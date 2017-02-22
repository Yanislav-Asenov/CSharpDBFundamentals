namespace ChangeTownNamesCasing
{
    using InitialSetup;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    class ChangeTownNamesCasing
    {
        static void Main()
        {
            InitialSetup.Connection.Open();

            string countryName = Console.ReadLine();
            string getTownsForCountry = "SELECT Name FROM Towns WHERE Country = @countryName";
            SqlCommand getTownsForCountryCmd = new SqlCommand(getTownsForCountry, InitialSetup.Connection);
            getTownsForCountryCmd.Parameters.AddWithValue("@countryName", countryName);
            SqlDataReader reader = getTownsForCountryCmd.ExecuteReader();

            List<string> townNames = new List<string>();
            using (reader)
            {
                while (reader.Read())
                {
                    townNames.Add(reader.GetString(0));
                }

                if (townNames.Count == 0)
                {
                    Console.WriteLine("No town names were affected.");
                    InitialSetup.Connection.Close();
                    return;
                }
            }

            string updateTownNames = $"UPDATE Towns SET Name = UPPER(Name) WHERE Country = @countryName";
            SqlCommand updateTownNamesCmd = new SqlCommand(updateTownNames, InitialSetup.Connection);
            updateTownNamesCmd.Parameters.AddWithValue("@countryName", countryName);
            updateTownNamesCmd.ExecuteNonQuery();

            Console.WriteLine($"{townNames.Count} town names were affected.");
            Console.WriteLine($"[{string.Join(", ", townNames.Select(x => x.ToUpper()).ToList())}]");

            InitialSetup.Connection.Close();
        }
    }
}
