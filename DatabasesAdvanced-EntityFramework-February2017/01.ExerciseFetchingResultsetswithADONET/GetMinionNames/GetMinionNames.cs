namespace GetMinionNames
{
    using InitialSetup;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    class GetMinionNames
    {
        static void Main()
        {
            try
            {
                InitialSetup.Connection.Open();
                Console.WriteLine("Enter villain ID: ");
                int targetVillainId = int.Parse(Console.ReadLine());

                string villainName = GetVillainNameById(targetVillainId);
                Console.WriteLine(villainName);
                if (villainName == $"No villain with ID {targetVillainId} exists in the database.")
                    return;

                string minionsInfoForVillain = GetMinionNamesForVillain(targetVillainId);
                Console.WriteLine(minionsInfoForVillain);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                InitialSetup.Connection.Close();
            }
        }

        public static string GetVillainNameById(int villainId)
        {
            string response = string.Empty;

            string getVillainNameById = "SELECT Name FROM Villains WHERE Id = @targetVillianId";
            SqlCommand getVillainNameCmd = new SqlCommand(getVillainNameById, InitialSetup.Connection);
            getVillainNameCmd.Parameters.AddWithValue("@targetVillianId", villainId);

            var targetVillianName = getVillainNameCmd.ExecuteScalar();

            if (targetVillianName == null)
            {
                response = $"No villain with ID {villainId} exists in the database.";
            }
            else
            {
                response = $"Villain: {targetVillianName}";
            }

            return response;
        }

        public static string GetMinionNamesForVillain(int villainId)
        {
            string getMinionsForVillain = "SELECT Name, Age FROM Minions INNER JOIN MinionsVillains AS mv ON mv.VillainId = @targetVillainId";
            SqlCommand getMinionsForVillainCmd = new SqlCommand(getMinionsForVillain, InitialSetup.Connection);
            getMinionsForVillainCmd.Parameters.AddWithValue("@targetVillainId", villainId);
            SqlDataReader reader = getMinionsForVillainCmd.ExecuteReader();

            using (reader)
            {
                StringBuilder minionsInfo = new StringBuilder();
                int rowCounter = 0;
                while (reader.Read())
                {
                    minionsInfo.Append($"{++rowCounter}. {reader["Name"]} {reader["Age"]}\n");
                }

                if (minionsInfo.Length == 0)
                {
                    return "(no minions)";
                }

                return minionsInfo.ToString();
            }
        }
    }
}
