using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PWMS.Web.Blazor.Services.HttpService
{
    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private IConfiguration _configuration;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            IConfiguration configuration
        )
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _configuration = configuration;
        }

        public async Task<T> Delete<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }

        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequest<T>(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }

        // helper methods

        private async Task<T> sendRequest<T>(HttpRequestMessage request)
        {
            // add jwt auth header if user is logged in and request is to the api url
            var token = await _localStorageService.GetItemAsStringAsync("authToken");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token?.Replace("\"", ""));

            using var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var returnUrl = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);
                _navigationManager.NavigateTo($"/logout?returnUrl={Uri.EscapeDataString(returnUrl)}");
                return default!;
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                //var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                //throw new Exception(error!["message"]);
            }

            var result = await response.Content.ReadFromJsonAsync<T>();

            return result!;
        }
    }
}
