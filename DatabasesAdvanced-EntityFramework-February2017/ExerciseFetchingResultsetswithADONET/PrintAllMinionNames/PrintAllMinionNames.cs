namespace PrintAllMinionNames
{
    using InitialSetup;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    class PrintAllMinionNames
    {
        static void Main()
        {
            try
            {
                InitialSetup.Connection.Open();

                var allMinionNames = GetAllMinionNames();

                bool isFirst = true;
                int first = 0;
                int last = allMinionNames.Count - 1;
                for (int i = 0; i < allMinionNames.Count; i++)
                {
                    if (isFirst)
                    {
                        Console.WriteLine(allMinionNames[first++]);
                    }
                    else
                    {
                        Console.WriteLine(allMinionNames[last--]);
                    }

                    isFirst = !isFirst;
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

        public static List<string> GetAllMinionNames()
        {
            var resultList = new List<string>();
            string getAllMinionNames = "SELECT Name FROM Minions";
            SqlCommand getAllMinionNamesCmd = new SqlCommand(getAllMinionNames, InitialSetup.Connection);
            SqlDataReader reader = getAllMinionNamesCmd.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    resultList.Add(reader["Name"].ToString());
                }
            }

            return resultList;
        }
    }
}
