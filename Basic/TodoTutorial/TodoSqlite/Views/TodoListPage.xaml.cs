using TodoSqlite.Data;
using TodoSqlite.Models;

namespace TodoSqlite.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TodoListPage : ContentPage
{
	public TodoListPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        TodoItemDatabase database = await TodoItemDatabase.Instance;
        listView.ItemsSource = await database.GetTodoItemsAsync();

    }

    private async void OnItemAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TodoItemPage
        {
            BindingContext = new TodoItem()
        });
    }

    private async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new TodoItemPage 
            {
                BindingContext = e.SelectedItem as TodoItem
            });
        }
    }
}