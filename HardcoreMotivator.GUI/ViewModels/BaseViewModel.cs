﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using HardcoreMotivator.GUI.Annotations;

namespace HardcoreMotivator.GUI.ViewModels
{
    internal class BaseViewModel: INotifyPropertyChanged
    {
        public event EventHandler<ViewModelMessageEventArgs> MessageEvent;
        protected virtual void SendMessage(ViewModelMessageEventArgs args)
        {
            MessageEvent?.Invoke(this, args);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
