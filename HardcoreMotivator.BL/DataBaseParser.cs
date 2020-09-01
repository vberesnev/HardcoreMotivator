using System;
using System.Collections.Generic;
using System.Text;
using HardcoreMotivator.BL.Models;

namespace HardcoreMotivator.BL
{
    class DataBaseParser
    {
        public User GetUser()
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

        public bool CreateUser(User user)
        {
            try
            {
                DataBaseManager.
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Can not create user: {e.Message}");
            }
        }
    }
}
