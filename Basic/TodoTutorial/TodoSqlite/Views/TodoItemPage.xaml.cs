using TodoSqlite.Data;
using TodoSqlite.Models;

namespace TodoSqlite.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TodoItemPage : ContentPage
{
	public TodoItemPage()
	{
		InitializeComponent();
	}

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var todoItem = (TodoItem)BindingContext;
        TodoItemDatabase database = await TodoItemDatabase.Instance;
        await database.SaveItemAsync(todoItem);
        await Navigation.PopAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var todoItem = (TodoItem)BindingContext;
        TodoItemDatabase database = await TodoItemDatabase.Instance;
        await database.DeleteItemAsync(todoItem);
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}