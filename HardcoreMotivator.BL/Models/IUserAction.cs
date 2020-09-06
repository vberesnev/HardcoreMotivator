using System.Collections;
using System.Collections.Generic;

namespace HardcoreMotivator.BL.Models
{
    public interface IUserAction
    {
        string ActionName { get; set; }
        string TableName { get; }   
        Measurement Measurement { get; set; }
        User User { get; set; }
        IEnumerable<ActionResult> Results { get; set; }
    }
}