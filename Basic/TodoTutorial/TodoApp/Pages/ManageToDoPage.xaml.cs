using System.Diagnostics;
using System.Text.Json;
using TodoApp.DataServices;
using TodoApp.Models;

namespace TodoApp.Pages;

[QueryProperty(nameof(Todo), "Todo")]
public partial class ManageToDoPage : ContentPage
{
    private readonly IRestDataService _dataService;

    Todo _toDo;
    bool _isNew;
    string _jsonToDo;

    public Todo ToDo
    {
        get => _toDo;
        set
        {
            _isNew = IsNew(value);
            _toDo = value;
            _jsonToDo = JsonSerializer.Serialize<Todo>(_toDo);
            OnPropertyChanged();
        }
    }

    public ManageToDoPage(IRestDataService dataService)
    {
        InitializeComponent();
        _dataService = dataService;
        BindingContext = this; //<----- this is important above all else 
    }

    public bool IsNew(Todo data)
    {
        return data.Id == 0;
    }

    async void OnSaveButtonClick(object sender, EventArgs e)
    {
        try
        {
            if (_isNew)
            {
                Debug.WriteLine($"-------<> Adding a new ToDo object \n {_jsonToDo}");
                await _dataService.AddToDoAsync(ToDo);
            }
            else
            {
                Debug.WriteLine($"--------<> Updating an ToDo Object \n {_jsonToDo}");
                await _dataService.UpdateToDoAsync(ToDo);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error occur on Save Button Click - " + ex.Message);
        }

        await Shell.Current.GoToAsync("..");
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void OnDeleteButtonClick(object sender, EventArgs e)
    {
        Debug.WriteLine($"--------<> Delete an ToDo Object \n{_jsonToDo}");
        await _dataService.DeleteToDoAsync(ToDo.Id);
        await Shell.Current.GoToAsync("..");
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void OnCancelButtonClick(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

}