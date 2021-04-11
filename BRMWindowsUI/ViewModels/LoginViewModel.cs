using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Controls;
using BRMWindowsUI.Helpers;
using Caliburn.Micro;
namespace BRMWindowsUI.ViewModels {
    public class LoginViewModel : Screen {
        private string _userName;
        private string _password;
        private IApiHelper _apiHelper;
        
        public LoginViewModel(IApiHelper apiHelper) {
            _apiHelper = apiHelper;
        }
        
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
            return output;
            }
        }

        public async Task Login(string userName, string password) {
            try {
                var result = await _apiHelper.Authenticate(UserName, Password);
                Trace.WriteLine(result.Access_Token);
            } catch(Exception e) {
                Trace.WriteLine("Hello there");
                Trace.WriteLine(e.Message);
                Console.WriteLine(e.Message);
            }
            
        }
    }
}