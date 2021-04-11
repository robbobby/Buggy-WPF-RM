using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BRMWindowsUI.Models;
namespace BRMWindowsUI.Helpers {
    public class ApiHelper : IApiHelper {
        private HttpClient _apiClient;

        public ApiHelper() {
            InitialiseClient();
        }

        private void InitialiseClient() {
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(apiUrl);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
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
                    var result = await responseMessage.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }           
        }
    }
}
