using System;
using System.Collections.Generic;
using System.Text;
using HardcoreMotivator.BL;
using HardcoreMotivator.BL.Models;

namespace HardcoreMotivator.GUI.ViewModels
{
    class InitialViewModel: BaseViewModel
    {
        public IEnumerable<Measurement> MeasurementSource { get; }

        private User _user;
        public User User
        {
            get => _user;
            set { _user = value; OnPropertyChanged(nameof(User)); OnPropertyChanged(nameof(UserAction));}
        }

        private IUserAction _userAction;
        public IUserAction UserAction
        {
            get => _userAction;
            set { _userAction = value; OnPropertyChanged(nameof(UserAction)); }
        }

        public RelayCommand AddUserCommand { get; private set; }

        public InitialViewModel()
        {
            MeasurementSource = DataManager.GetMeasurements();
            User = new User();
            UserAction = new UserAction(User);
            AddUserCommand = new RelayCommand(AddUser);
        }

        private void AddUser(object obj)
        {
            if (!CheckForm())
            {
                var eventArgs = new ViewModelMessageEventArgs(EventType.Error,  "Fill all textboxes");
                SendMessage(eventArgs);
            }

            try
            {
                CreateUser();
                CreateUserAction();
            }
            catch (Exception e)
            {
                var eventArgs = new ViewModelMessageEventArgs(EventType.Error, e.Message);
                SendMessage(eventArgs);
            }
        }

        private bool CheckForm()
        {
            return (!string.IsNullOrEmpty(User.Name) && 
                    !string.IsNullOrEmpty(User.PasswordForOff) &&
                    !string.IsNullOrEmpty(UserAction.ActionName) &&
                    UserAction.Measurement != null);
        }

        private void CreateUser()
        {
            if (DataManager.CreateUser(User))
            {
                User =  DataManager.GetUserByName(User.Name);
                return;
            }
            throw new Exception("Can not create new user, because user with this name already exists");
        }

        private bool CreateUserAction()
        {
            return DataManager.CreateUserAction(UserAction);
        }
    }
}
