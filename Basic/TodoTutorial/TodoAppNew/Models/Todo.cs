using System.ComponentModel;

namespace TodoApp.Models
{
    public class Todo : INotifyPropertyChanged
    {
        int _id;
        string _toDoName;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get => _id;
            set
            {
                if (_id == value) return;

                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        public string ToDoName
        {
            get => _toDoName ?? string.Empty;
            set
            {
                if (_toDoName == value) return;

                _toDoName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToDoName)));
            }
        }
    }
}
