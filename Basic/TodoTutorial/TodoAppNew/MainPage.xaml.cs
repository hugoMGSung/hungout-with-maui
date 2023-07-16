using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using TodoApp.Models;
using TodoApp.Pages;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TodoApp
{
    public partial class MainPage : ContentPage
    {
        HttpClient client = new HttpClient(); //restAPI를 위함
        String serviceAddress = "192.168.45.190";
        String servicePort = "5224";
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public MainPage()
        {
            InitializeComponent();
            InitApiService();
        }

        private void InitApiService()
        {
            client.BaseAddress = new Uri($"http://{serviceAddress}:{servicePort}"); // 기본 URL
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // 헤더
        }

        async void OnAddToDoClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("--------<> 일정추가 버튼 클릭");

            var navigationParameter = new Dictionary<string, object>
            {
                { nameof(Todo), new Todo()}
            };

            await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationParameter);
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("---------<> 선택 클릭");
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            var todos = LoadApi();
            collectionView.ItemsSource = todos.Result;
        }

        private async Task<List<Todo>> LoadApi()
        {
            List<Todo> todos = new List<Todo>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----<> 인터넷 접속 오류");
                return todos;
            }

            try
            {
                HttpResponseMessage? response = await client.GetAsync("api/todo");
                response.EnsureSuccessStatusCode();

                var context = await response.Content.ReadAsAsync<Object>();
                var result = JsonConvert.SerializeObject(context);
                var jArray = JArray.Parse(result);
                
                foreach (var todo in jArray)
                {
                    todos.Add(new Todo
                    {
                        Id = Convert.ToInt32(todo["id"]), // Id 로 하면 안됨
                        ToDoName = todo["todoName"].ToString(), // TodoName으로 하면 안됨
                    });                  
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Service Loaded Error : {ex.Message}");
            }
            
            return todos;
        }
    }
}