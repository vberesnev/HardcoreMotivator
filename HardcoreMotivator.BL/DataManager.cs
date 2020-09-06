using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using HardcoreMotivator.BL.Models;

namespace HardcoreMotivator.BL
{
    public static class DataManager
    {
        public static bool IsAnyUsersExists()
        {
            try
            {
                return DataBaseManager.IsAnyUsersExists();
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Can not get user from database: {e.Message}");
            }
        }
        
        public static User GetFirstUser()
        {
            try
            {
                var sqlResult = DataBaseManager.GetTopOneRow("User");
                var user = new User(Convert.ToInt32(sqlResult[0]),
                                    sqlResult[1].ToString(),
                                    sqlResult[2].ToString());
                return user;
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Can not get user from database: {e.Message}");
            }
        }

        public static User GetUserByName(string userName)
        {
            try
            {
                var sqlResult = DataBaseManager.GetTopOneRow("User", $"WHERE Name = '{userName}'");
                var user = new User(Convert.ToInt32(sqlResult[0]),
                    sqlResult[1].ToString(),
                    sqlResult[2].ToString());
                return user;
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Can not get user from database: {e.Message}");
            }
        }

        public static bool CreateUser(User user)
        {
            try
            {
                return DataBaseManager.CreateUser(user.Name, user.PasswordForOff);
            }
            catch (Exception e)
            {
                throw new SQLiteException($"Can not create user: {e.Message}");
            }
        }

        public static List<Measurement> GetMeasurements()
        {
            try
            {
                var measurements = new List<Measurement>();
                var sqlResult = DataBaseManager.GetAllRows("MeasurementsDictionary");
                foreach (var objectArr in sqlResult)
                {
                    var measurement = new Measurement(Convert.ToInt32(objectArr[0]), objectArr[1].ToString());
                    measurements.Add(measurement);
                }

                return measurements;
            }
            catch (Exception e)
            {
                throw new SQLiteException($"Can not get measurements list: {e.Message}");
            }
        }

        public static bool CreateUserAction(IUserAction userAction)
        {
            try
            {
                DataBaseManager.CreateUserActionTable(userAction.TableName);
                DataBaseManager.AddValueToActionsDictionary(userAction.ActionName, userAction.TableName, userAction.Measurement.Id, userAction.User.Id);
                return true;
            }
            catch (Exception e)
            {
                throw new SQLiteException($"Can not create user action: {e.Message}");
            }
        }
    }
}
