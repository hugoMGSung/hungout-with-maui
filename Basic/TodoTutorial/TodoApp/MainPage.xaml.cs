using System.Diagnostics;
using System.Text.Json;
using TodoApp.DataServices;
using TodoApp.Models;
using TodoApp.Pages;

namespace TodoApp
{
    public partial class MainPage : ContentPage
    {
        private readonly IRestDataService _dataService;

        public MainPage(IRestDataService dataServices)
        {
            InitializeComponent();
            _dataService = dataServices;
        }

        /// <summary>
        /// Override the method: When the page is loading do the following 
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await _dataService.GetAllToDosAsync();
        }

        /// <summary>
        /// Event-Hanlders: 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnAddToDoClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("--------<> Clicked the Add Button");

            var navigationParameter = new Dictionary<string, object>
            {
                { nameof(Todo), new Todo()}
            };

            await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationParameter);
        }

        /// <summary>
        /// Event-Handler: 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string jsonToDo = JsonSerializer.Serialize<Todo>(e.CurrentSelection.FirstOrDefault() as Todo);

            Debug.WriteLine("---------<> Clicked on Selection \n" + jsonToDo ?? string.Empty);

            var navigationParameter = new Dictionary<string, object>
            {
                { nameof(Todo), e.CurrentSelection.FirstOrDefault() as Todo}
            };

            await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationParameter);

        }
    }
}