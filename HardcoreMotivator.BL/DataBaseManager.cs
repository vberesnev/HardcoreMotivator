using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace HardcoreMotivator.BL
{
    public class DataBaseManager
    {
        public static bool IsUserExists()
        {
            try
            {
                using var connection = new SQLiteConnection(GetConnectionString());
                connection.Open();
                var command = new SQLiteCommand("SELECT COUNT(*) FROM User", connection);
                return Convert.ToInt32(command.ExecuteScalar()) == 1;
            }
            catch (Exception e)
            {
                throw new Exception($"Can not get data from database: {e.Message}");
            }
            
        }

        private static string GetConnectionString(string id="Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
