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
            if (true)  // �� ������ �ƴ���
            {
                Debug.WriteLine($"-------<> �� ���� �߰�");
                //await _dataService.AddToDoAsync(ToDo);
            }
            else
            {
                Debug.WriteLine($"--------<> �����ϼ� ����");
                //await _dataService.UpdateToDoAsync(ToDo);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"�����ư ���� {ex.Message}");
        }

        //await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteButtonClick(object sender, EventArgs e)
    {
        Debug.WriteLine($"--------<> ������ư Ŭ���̺�Ʈ");
        //await _dataService.DeleteToDoAsync(ToDo.Id);
        //await Shell.Current.GoToAsync("..");
    }

    async void OnCancelButtonClick(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}