namespace IncreaseMinionsAge
{
    using InitialSetup;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Text;

    class IncreaseMinionsAge
    {
        static void Main()
        {
            InitialSetup.Connection.Open();

            string[] minionIds = Console.ReadLine().Split(' ');

            IncreaseAge(minionIds);

            var minionNames = GetMinionNames(minionIds);

            StringBuilder updateMinionNames = new StringBuilder();
            foreach (var kvp in minionNames)
            {
                updateMinionNames.Append($"UPDATE Minions SET Name = '{kvp.Value}' WHERE Id = {kvp.Key}; ");
            }

            SqlCommand updateMinionNamesCmd = new SqlCommand(updateMinionNames.ToString(), InitialSetup.Connection);
            updateMinionNamesCmd.ExecuteNonQuery();

            Console.WriteLine(string.Join("\n", GetAllMinionsInfo()));

            InitialSetup.Connection.Close();
        }

        public static List<string> GetAllMinionsInfo()
        {
            string getAllMinionsInfo = $"SELECT Name, Age FROM Minions";
            SqlCommand getAllMinionsInfoCmd = new SqlCommand(getAllMinionsInfo, InitialSetup.Connection);
            SqlDataReader reader = getAllMinionsInfoCmd.ExecuteReader();

            var result = new List<string>();
            using (reader)
            {
                while (reader.Read())
                {
                    result.Add($"{reader["Name"]} {reader["Age"]}");
                }
            }

            return result;
        }

        private static Dictionary<string, string> GetMinionNames(string[] minionIds)
        {
            string getMinionNames = $"SELECT Id, Name FROM Minions WHERE Id IN ({string.Join(", ", minionIds)})";
            SqlCommand getMinionNamesCmd = new SqlCommand(getMinionNames, InitialSetup.Connection);
            SqlDataReader reader = getMinionNamesCmd.ExecuteReader();

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var result = new Dictionary<string, string>();
            using (reader)
            {
                while (reader.Read())
                {
                    var titleCasedName = textInfo.ToTitleCase(reader["Name"].ToString());
                    result.Add(reader["Id"].ToString(), titleCasedName);
                }
            }

            return result;
        }

        private static int IncreaseAge(string[] minionIds)
        {
            string increaseMinionsAge = $"UPDATE Minions SET Age = Age + 1 WHERE Id IN ({string.Join(", ", minionIds)})";
            SqlCommand increaseMinionsAgeCmd = new SqlCommand(increaseMinionsAge, InitialSetup.Connection);

            return increaseMinionsAgeCmd.ExecuteNonQuery();
        }
    }
}
