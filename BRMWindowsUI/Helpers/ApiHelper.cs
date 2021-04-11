using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace BRMWindowsUI.Helpers {
    public class ApiHelper {
        public HttpClient ApiClient { get; set; }

        public ApiHelper() {
            InitialiseClient();
        }

        private void InitialiseClient() {
            string apiUrl = ConfigurationManager.AppSettings["api"];
            ApiClient = new HttpClient {
                BaseAddress = new Uri(apiUrl)
            };
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Authenticate(string userName, string password) {
            var data = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "username"),
                new KeyValuePair<string, string>("password", "password")
            });
            using (HttpResponseMessage responseMessage = await ApiClient.PostAsync("/token", data)) {
                if (responseMessage.IsSuccessStatusCode) {
                    var result = await responseMessage.Content.ReadAsAsync<string>();
                }
                
            }           
        }
    }
}
