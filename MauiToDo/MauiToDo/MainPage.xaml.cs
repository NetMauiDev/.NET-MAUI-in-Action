using MauiToDo.Data;
using MauiToDo.Models;

namespace MauiToDo
{
    public partial class MainPage : ContentPage
    {
        string _todoListData = string.Empty;

        readonly Database _database;

        public MainPage()
        {
            InitializeComponent();
            _database = new Database();

            _ = Initialize();
        }

        private async Task Initialize()
        {
            var todos = await _database.GetTodos();

            foreach (var todo in todos)
            {
                _todoListData += $"{todo.Title} - {todo.Due:f}{Environment.NewLine}";
            }

            TodosLabel.Text = _todoListData;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var todo = new TodoItem
            {
                Due = DueDatepicker.Date,
                Title = TodoTitleEntry.Text
            };

            var inserted = await _database.AddTodo(todo);

            if (inserted != 0) 
            {
                _todoListData += $"{todo.Title} - {todo.Due:f}{Environment.NewLine}";

                TodosLabel.Text = _todoListData;

                TodoTitleEntry.Text = string.Empty;
                DueDatepicker.Date = DateTime.Now;
            }
        }
    }

}
