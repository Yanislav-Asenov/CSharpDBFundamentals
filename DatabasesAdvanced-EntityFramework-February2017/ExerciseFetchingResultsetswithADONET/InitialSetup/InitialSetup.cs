namespace InitialSetup
{
    using System;
    using System.Data.SqlClient;

    public class InitialSetup
    {
        public static SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=MinionsDB;Integrated Security=True");

        public static void Main()
        {
            RunDbSetup();
        }

        public static void RunDbSetup()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                ["Data Source"] = @"(LocalDb)\MSSQLLocalDB",
                ["Integrated Security"] = true
            };

            SqlConnection connection = new SqlConnection(builder.ToString());

            try
            {
                connection.Open();
                ExecuteSqlCommand("CREATE DATABASE MinionsDB", connection);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }

            builder["Initial Catalog"] = "MinionsDB";
            connection = new SqlConnection(builder.ToString());
            try
            {
                connection.Open();
                string createTownsSQL = "CREATE TABLE Towns (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), Country VARCHAR(50))";
                string createMinionsSQL = "CREATE TABLE Minions (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), Age INT, TownId INT, CONSTRAINT FK_Towns FOREIGN KEY (TownId) REFERENCES Towns(Id))";
                string createVillainsSQL = "CREATE TABLE Villains (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactor VARCHAR(20))";
                string createMinionsVillainsSQL = "CREATE TABLE MinionsVillains(MinionId INT, VillainId INT, CONSTRAINT FK_Minions FOREIGN KEY (MinionId) REFERENCES Minions(Id), CONSTRAINT  FK_Villains FOREIGN KEY (VillainId) REFERENCES Villains(Id))";

                ExecuteSqlCommand(createTownsSQL, connection);
                ExecuteSqlCommand(createMinionsSQL, connection);
                ExecuteSqlCommand(createVillainsSQL, connection);
                ExecuteSqlCommand(createMinionsVillainsSQL, connection);

                string insertTownsSQL = "INSERT INTO Towns (Name, Country) VALUES ('Sofia','Bulgaria'), ('Burgas','Bulgaria'), ('Varna', 'Bulgaria'), ('London','UK'),('Liverpool','UK'),('Ocean City','USA'),('Paris','France')";
                string insertMinionsSQL = "INSERT INTO Minions (Name, Age, TownId) VALUES ('bob',10,1),('kevin',12,2),('steward',9,3), ('rob',22,3), ('michael',5,2),('pep',3,2)";
                string insertVillainsSQL = "INSERT INTO Villains (Name, EvilnessFactor) VALUES ('Gru','super evil'),('Victor','evil'),('Simon Cat','good'),('Pusheen','super good'),('Mammal','evil')";
                string insertMinionsVillainsSQL = "INSERT INTO MinionsVillains VALUES (1,2), (3,1),(1,3),(3,3),(4,1),(2,2),(1,1),(3,4), (1, 4), (1,5), (5, 1), (4,1), (3, 1)";

                ExecuteSqlCommand(insertTownsSQL, connection);
                ExecuteSqlCommand(insertMinionsSQL, connection);
                ExecuteSqlCommand(insertVillainsSQL, connection);
                ExecuteSqlCommand(insertMinionsVillainsSQL, connection);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static void ExecuteSqlCommand(string queryString, SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand(queryString, connection);
            cmd.ExecuteNonQuery();
        }
    }
}
