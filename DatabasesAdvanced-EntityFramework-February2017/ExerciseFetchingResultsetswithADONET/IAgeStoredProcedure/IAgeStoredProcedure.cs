namespace IAgeStoredProcedure
{
    using InitialSetup;
    using System;
    using System.Data.SqlClient;

    class IAgeStoredProcedure
    {
        static void Main()
        {
            try
            {
                string minionId = Console.ReadLine();

                InitialSetup.Connection.Open();
                string increaseAgeStoredProcedure = "EXEC dbo.usp_GetOlder @minionId";
                SqlCommand increaseAgeStoredProcedureCmd =
                    new SqlCommand(increaseAgeStoredProcedure, InitialSetup.Connection);
                increaseAgeStoredProcedureCmd.Parameters.AddWithValue("@minionId", minionId);
                SqlDataReader reader = increaseAgeStoredProcedureCmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            finally
            {
                InitialSetup.Connection.Close();
            }
        }
    }
}
