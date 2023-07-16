using System.Diagnostics;

namespace TodoApp.Pages;

public partial class ManageToDoPage : ContentPage
{
    public ManageToDoPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    async void OnSaveButtonClick(object sender, EventArgs e)
    {
        try
        {
            if (true)  // 새 값인지 아닌지
            {
                Debug.WriteLine($"-------<> 새 일정 추가");
                //await _dataService.AddToDoAsync(ToDo);
            }
            else
            {
                Debug.WriteLine($"--------<> 이전일성 수정");
                //await _dataService.UpdateToDoAsync(ToDo);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"저장버튼 예외 {ex.Message}");
        }

        //await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteButtonClick(object sender, EventArgs e)
    {
        Debug.WriteLine($"--------<> 삭제버튼 클릭이벤트");
        //await _dataService.DeleteToDoAsync(ToDo.Id);
        //await Shell.Current.GoToAsync("..");
    }

    async void OnCancelButtonClick(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}