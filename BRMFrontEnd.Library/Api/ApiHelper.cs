using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BRMFrontEnd.Library.Models;
using BRMFrontEnd.Library.Models.ModelInterfaces;
using BRMWindowsUI.Models;

namespace BRMFrontEnd.Library.Api {
    public class ApiHelper : IApiHelper {
        private ILoggedInUserModel _loggedInUserModel;
        private HttpClient _apiClient;

        public ApiHelper(ILoggedInUserModel loggedInUserModel) {
            _loggedInUserModel = loggedInUserModel;
            InitialiseClient();

        }

        private void InitialiseClient() {
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(apiUrl);
            ClearApiClient();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string userName, string password) {
            var data = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password)
            });
            using (HttpResponseMessage responseMessage = await _apiClient.PostAsync("/token", data)) {
                Trace.WriteLine($"{_apiClient.BaseAddress}/token");
                if (responseMessage.IsSuccessStatusCode) {
                    AuthenticatedUser user = await responseMessage.Content.ReadAsAsync<AuthenticatedUser>();
                    
                    return user;
                }
                else {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }           
        }
        public async Task GetAndSetLoggedInUserInfo(string token) {
            ClearApiClient();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            using (HttpResponseMessage responseMessage = await _apiClient.GetAsync("/api/user")) {
                if (responseMessage.IsSuccessStatusCode) {
                    LoggedInUserModel userResult = await responseMessage.Content.ReadAsAsync<LoggedInUserModel>();
                    MapToLoggedInUser(userResult);
                } else {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }
        
        private void MapToLoggedInUser(ILoggedInUserModel userToMapFrom) {
            _loggedInUserModel.Id = userToMapFrom.Id;
            _loggedInUserModel.Token = userToMapFrom.Token;
            _loggedInUserModel.FirstName = userToMapFrom.FirstName;
            _loggedInUserModel.LastName = userToMapFrom.LastName;
            _loggedInUserModel.EmailAddress = userToMapFrom.EmailAddress;
            _loggedInUserModel.CreatedDate = userToMapFrom.CreatedDate;
        }
        private void ClearApiClient() {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
        }
    }
}
