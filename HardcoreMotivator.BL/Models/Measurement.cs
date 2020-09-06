using System;
using System.Collections.Generic;
using System.Text;

namespace HardcoreMotivator.BL.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public Measurement(int id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
