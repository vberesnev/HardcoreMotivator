using System;
using System.Collections.Generic;
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

        public static object[] GetTopOneRow(string tableName)
        {
            try
            {
                using var connection = new SQLiteConnection(GetConnectionString());
                connection.Open();
                var command = new SQLiteCommand($"SELECT TOP 1 * FROM {tableName}", connection);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    var result = new object[reader.FieldCount];
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            result[i] = reader.GetValue(i);
                        }
                    }
                    return result;
                }
                else
                {
                    return new object[0];
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Can not get data from database: {e.Message}");
            }
        }

        public static List<object[]> GetAllRows(string tableName)
        {
            try
            {
                using var connection = new SQLiteConnection(GetConnectionString());
                connection.Open();
                var command = new SQLiteCommand($"SELECT * FROM {tableName}", connection);
                var reader = command.ExecuteReader();
                var list = new List<object[]>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var row = new object[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader.GetValue(i);
                        }
                        list.Add(row);
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception($"Can not get data from database: {e.Message}");
            }
        }

        public static bool CreateUser(string name, string password)
        {
            //TODO
        }

        private static string GetConnectionString(string id="Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
