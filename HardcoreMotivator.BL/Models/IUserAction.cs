using System.Collections;
using System.Collections.Generic;

namespace HardcoreMotivator.BL.Models
{
    public interface IUserAction
    {
        public string ActionName { get; set; }
        public string TableName { get; set; }   
        public IEnumerable<ActionResult> Results { get; set; }
    }
}