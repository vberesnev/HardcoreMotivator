using System;
using System.Collections.Generic;
using System.Text;

namespace HardcoreMotivator.BL.Models
{
    public class ActionResult
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public Measurement Measurement { get; set; }
    }
}
