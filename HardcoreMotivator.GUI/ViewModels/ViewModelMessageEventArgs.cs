using System;
using System.Collections.Generic;
using System.Text;

namespace HardcoreMotivator.GUI.ViewModels
{
    class ViewModelMessageEventArgs: EventArgs
    {
        public EventType Type { get; private set; }
        public string Message { get; private set; }
        
        public ViewModelMessageEventArgs(EventType type, string message)
        {
            Type = type;
            Message = message;
        }
    }

    enum EventType
    {
        Message,
        Success,
        Error,
    }
}
