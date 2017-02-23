namespace RemoveVillain
{
    using InitialSetup;
    using System;
    using System.Data.SqlClient;

    class RemoveVillain
    {
        static void Main()
        {
            try
            {
                InitialSetup.Connection.Open();

                int villainId = int.Parse(Console.ReadLine());
                string villainName = GetVillainNameById(villainId);

                if (villainName == null)
                {
                    Console.WriteLine("No such villain was found");
                    return;
                }

                int numberOfReleasedMinions = ReleaseVillainMinions(villainId);

                Console.WriteLine($"{villainName} was deleted");
                Console.WriteLine($"{numberOfReleasedMinions} minions released");
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

        public static int DeleteVillainById(int villainId)
        {
            string deleteVillainFromVillains = "DELETE FROM Villains WHERE Id = @villainId";
            SqlCommand deleteVillainFromVillainsCmd =
                new SqlCommand(deleteVillainFromVillains, InitialSetup.Connection);
            deleteVillainFromVillainsCmd.Parameters.AddWithValue("@villainId", villainId);
            return deleteVillainFromVillainsCmd.ExecuteNonQuery();
        }

        public static int ReleaseVillainMinions(int villainId)
        {
            string deleteVillainFromVillainMinions = "DELETE FROM MinionsVillains WHERE VillainId = @villainId";
            SqlCommand deleteVillanFromVillainMinionsCmd =
                new SqlCommand(deleteVillainFromVillainMinions, InitialSetup.Connection);
            deleteVillanFromVillainMinionsCmd.Parameters.AddWithValue("@villainId", villainId);

            return deleteVillanFromVillainMinionsCmd.ExecuteNonQuery();
        }

        public static string GetVillainNameById(int villainId)
        {
            string getVillainNameById = "SELECT TOP 1 Name FROM Villains WHERE Id = @villainId";
            SqlCommand getVillainNameByIdCmd =
                new SqlCommand(getVillainNameById, InitialSetup.Connection);
            getVillainNameByIdCmd.Parameters.AddWithValue("@villainId", villainId);
            SqlDataReader reader = getVillainNameByIdCmd.ExecuteReader();

            using (reader)
            {
                if (reader.Read())
                {
                    return reader["Name"].ToString();
                }

                return null;
            }
        }
    }
}
