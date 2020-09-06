using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace HardcoreMotivator.BL
{
    internal class DataBaseManager
    {
        internal static bool IsAnyUsersExists()
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

        internal static object[] GetTopOneRow(string tableName, string whereCondition = "")
        {
            try
            {
                using var connection = new SQLiteConnection(GetConnectionString());
                connection.Open();
                var command = new SQLiteCommand($"SELECT * FROM {tableName} {whereCondition} ORDER BY Id LIMIT 1", connection);
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

                return new object[0];
            }
            catch (Exception e)
            {
                throw new Exception($"Can not get data from database: {e.Message}");
            }
        }

        internal static List<object[]> GetAllRows(string tableName)
        {
            try
            {
                using var connection = new SQLiteConnection(GetConnectionString());
                connection.Open();
                var selectCommand = new SQLiteCommand($"SELECT * FROM {tableName}", connection);
                var reader = selectCommand.ExecuteReader();
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

        internal static bool CreateUser(string name, string password)
        {
            try
            {
                using var connection = new SQLiteConnection(GetConnectionString());
                connection.Open();

                if (DoesUserAlreadyExist(name, connection))
                    return false;
                
                var insertCommand = new SQLiteCommand("INSERT INTO User (Name, PassForOff) VALUES (@param1, @param2)", connection);
                insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.Add("@param1", DbType.String).Value = name;
                insertCommand.Parameters.Add("@param2", DbType.String).Value = password;
                insertCommand.ExecuteNonQuery();
                return true;

            }
            catch (Exception e)
            {
                throw new SQLiteException($"Can not create user: {e.Message}");
            }
        }

        private static bool DoesUserAlreadyExist(string name, SQLiteConnection connection)
        {
            var selectCommand = new SQLiteCommand("SELECT COUNT(*) FROM User WHERE Name = @param1", connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.Add("@param1", DbType.String).Value = name;
            return (long)selectCommand.ExecuteScalar() == 1;
        }

        public static void CreateUserActionTable(string userActionTableName)
        {
            try
            {
                using var connection = new SQLiteConnection(GetConnectionString());
                connection.Open();
                var createTableCommand = new SQLiteCommand($"CREATE TABLE {userActionTableName} (\"Id\" INTEGER NOT NULL UNIQUE, \"Date\" TEXT NOT NULL, \"Value\" REAL NOT NULL, PRIMARY KEY(\"Id\" AUTOINCREMENT));", connection);
                createTableCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new SQLiteException($"Can not create user action table <{userActionTableName}>: {e.Message}");
            }
        }

        private static string GetConnectionString(string id="Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static void AddValueToActionsDictionary(string userActionActionName, string userActionTableName, int measurementId, int userId)
        {
            try
            {
                using var connection = new SQLiteConnection(GetConnectionString());
                connection.Open();
                
                var insertCommand = new SQLiteCommand("INSERT INTO ActionsDictionary (UserActionName, DbTableActionName, MeasurementId, UserId) VALUES ('?','?', @param1, @param2)", connection);
                insertCommand.CommandType = CommandType.Text;               
                insertCommand.Parameters.Add(new SQLiteParameter(userActionActionName, DbType.String));
                insertCommand.Parameters.Add(new SQLiteParameter(userActionTableName, DbType.String));
                insertCommand.Parameters.Add("@param1", DbType.Int32).Value = measurementId;
                insertCommand.Parameters.Add("@param2", DbType.Int32).Value = userId;
                insertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new SQLiteException($"Can not insert action to action dictionary table: {e.Message}");
            }
        }
    }
}
