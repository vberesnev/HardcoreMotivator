using System;
using System.Collections.Generic;
using System.Text;

namespace HardcoreMotivator.BL.Models
{
    public class UserAction: IUserAction
    {
        private string _actionName;
        public string ActionName
        {
            get => _actionName;
            set 
            { 
                _actionName = value;
                TableName = Helper.GenerateTableName(value);
            }
        }
        public string TableName { get; private set; }
        public User User { get; set; }
        public Measurement Measurement { get; set; }
        public IEnumerable<ActionResult> Results { get; set; }

        public UserAction(){}

        public UserAction(User user)
        {
            User = user;
        }

        public UserAction(string actionName, User user, Measurement measurement)
        {
            ActionName = actionName;
            User = user;
            Measurement = measurement;
        }
    }
}
