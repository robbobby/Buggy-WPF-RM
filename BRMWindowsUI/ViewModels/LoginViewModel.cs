using System;
using System.Windows.Controls;
using Caliburn.Micro;
namespace BRMWindowsUI.ViewModels {
    public class LoginViewModel : Screen {
        private string _userName;
        private string _password;

        public string UserName {
            get => _userName;
            set {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogin);
            }
    }


        public string Password {
            get => _password;
            set {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public bool CanLogin {
            get {
            bool output = false;
            if (UserName?.Length > 0 && Password?.Length > 0)
                output = true;
            ;
            return output;
            }
        }

        public void Login(string userName, string password) {
            Console.WriteLine("Hello");
        }
    }
}