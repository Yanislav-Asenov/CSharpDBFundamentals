namespace GetVillainsNames
{
    using InitialSetup;
    using System;
    using System.Data.SqlClient;

    public class GetVillainsNames
    {
        public static void Main()
        {
            try
            {
                string getVillansNamesSQL = $"SELECT v.Name, (SELECT COUNT(*) FROM Minions INNER JOIN MinionsVillains AS mv ON mv.VillainId = v.Id) AS NumberOfMinions FROM Villains AS v";
                InitialSetup.Connection.Open();
                SqlCommand cmd = new SqlCommand(getVillansNamesSQL, InitialSetup.Connection);
                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} {reader["NumberOfMinions"]}");
                    }
                }
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
    }
}
