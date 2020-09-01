using System;
using System.Collections.Generic;
using System.Text;

namespace HardcoreMotivator.BL.Models
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordForOff { get; set; }

        public User(string name, string password)
        {
            Name = name;
            PasswordForOff = password;
        }

        public User(int id, string name, string password)
            : this(name, password)
        {
            Id = id;
        }


    }
}
