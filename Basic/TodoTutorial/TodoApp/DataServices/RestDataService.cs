using System.Diagnostics;
using System.Text;
using System.Text.Json;
using TodoApp.Models;

namespace TodoApp.DataServices
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // 아이피 변경할 것
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.45.190:5224" : "http://localhost:5224";
            _url = $"{_baseAddress}/api";
            _jsonSerializerOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AddToDoAsync(Todo data)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("----<> No Internet Access");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<Todo>(data, _jsonSerializerOptions);
                StringContent context = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/todo", context);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("----<> Successfully created ToDo");
                }
                else
                {
                    Debug.WriteLine("----<> Failed to created ToDo");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("------<>  Sorry error message - " + ex.Message);
            }

            return;
        }

        public async Task DeleteToDoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("------<> No Internet Connection Detected");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todo/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("------<> Successfully deleted ToDo object");
                }
                else
                {
                    Debug.WriteLine("-------<> Failed to delete ToDo object");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured on main DeleteToDoAsync operation - " + ex.Message);
            }

            return;
        }

        public async Task<List<Todo>> GetAllToDosAsync()
        {
            List<Todo> todos = new List<Todo>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----<> No Internet Access");
                return todos;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo");

                if (response.IsSuccessStatusCode)
                {
                    string context = await response.Content.ReadAsStringAsync();
                    todos = JsonSerializer.Deserialize<List<Todo>>(context, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("-----<> Non http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exec " + ex.Message);
            }

            return todos;
        }

        public async Task UpdateToDoAsync(Todo data)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("------<> No Internet Connection Detected");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<Todo>(data, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{data.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"------<> Successfully updated ToDo Object");
                }
                else
                {
                    Debug.WriteLine($"------<> Failed to update ToDo Object \n{jsonToDo}");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occur in main UpdateToDoAsync operation - {ex.Message}");
            }

            return;
        }
    }
}
