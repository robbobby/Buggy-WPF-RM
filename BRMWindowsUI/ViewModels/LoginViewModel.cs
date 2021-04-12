using System;
using System.Threading.Tasks;
using BRMFrontEnd.Library.Api;
using BRMFrontEnd.Library.Models.ModelInterfaces;
using BRMWindowsUI.Models;
using Caliburn.Micro;

namespace BRMWindowsUI.ViewModels {
    public class LoginViewModel : Screen {
        private string _userName;
        private string _password;
        public string _errorMessage;
        
        private IApiHelper _apiHelper;
        private readonly ILoggedInUserModel _loggedInUser;

        public LoginViewModel(IApiHelper apiHelper, ILoggedInUserModel loggedInUser) {
            _apiHelper = apiHelper;
            _loggedInUser = loggedInUser;
        }

        public string UserName {
            get => _userName;
            set {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogin);
            }
    }
        public bool IsErrorVisible => ErrorMessage.Length > 0;
        
        public string ErrorMessage {
            get => _errorMessage;
            set {
                _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
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
            ErrorMessage = "";
            try {
                AuthenticatedUser result = await _apiHelper.Authenticate(UserName, Password);
                await _apiHelper.GetAndSetLoggedInUserInfo(result.Access_Token);
                ErrorMessage = _loggedInUser.Id;
            } catch(Exception e) {
                ErrorMessage = e.Message;
            }
            
        }
    }
}