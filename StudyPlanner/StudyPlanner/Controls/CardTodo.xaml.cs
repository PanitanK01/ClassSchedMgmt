using StudyPlanner.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyPlanner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardTodo : ContentView
    {
        public static readonly BindableProperty TaskProperty = BindableProperty.Create(nameof(Task), typeof(Todo), typeof(CardTodo), null, BindingMode.TwoWay);
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(Input), string.Empty);
        public static readonly BindableProperty StartDateProperty = BindableProperty.Create(nameof(StartDate), typeof(DateTime), typeof(CardTimeActive), null);

        public event EventHandler TodoChanged;
        public event EventHandler Deleted;
        public event EventHandler EmptyTask;

        public string oldText;

        public Todo Task
        {
            get => (Todo)GetValue(TaskProperty);
            set => SetValue(TaskProperty, value);
        }
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        public DateTime StartDate
        {
            get => (DateTime)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }

        public CardTodo()
        {
            InitializeComponent();
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            oldText = Task.Task;
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Contains(Environment.NewLine))
            {
                var editor = (Editor)sender;
                Task.Task = e.OldTextValue;
                OnPropertyChanged(nameof(Task));
                editor.Unfocus();
                if (oldText != Task.Task)
                    TodoChanged?.Invoke(Task, EventArgs.Empty);
                if (string.IsNullOrWhiteSpace(Task.Task))
                    EmptyTask?.Invoke(Task, EventArgs.Empty);
            }
        }
        private void OnCompleted(object sender, EventArgs e)
        {
            if (oldText != Task.Task)
                TodoChanged?.Invoke(Task, EventArgs.Empty);
            if (string.IsNullOrWhiteSpace(Task.Task))
                EmptyTask?.Invoke(Task, EventArgs.Empty);
        }
        private void OnCompleteToggledChanged(object sender, EventArgs e)
        {
            TodoChanged?.Invoke(Task, EventArgs.Empty);
        }
        private void OnImportantToggledChanged(object sender, EventArgs e)
        {
            TodoChanged?.Invoke(Task, EventArgs.Empty);

        }
        private void OnDeleteClicked(object sender, EventArgs e)
        {
            Deleted?.Invoke(Task, EventArgs.Empty);
        }
    }
}