using StudyPlanner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTodo : ContentPage
    {
        public PageTodo()
        {
            InitializeComponent();
        }
        private async void RefreshData()
        {
            List<Todo> incomplete = await App.Database.GetTodos(false);
            BindableLayout.SetItemsSource(incompleteView, incomplete);

            List<Todo> complete = await App.Database.GetTodos(true);
            BindableLayout.SetItemsSource(completeView, complete);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshData();
        }

        private void OnRefreshing(object sender, EventArgs e)
        {
            RefreshData();
            refresh.IsRefreshing = false;
        }

        private async void OnTodoAdded(object sender, EventArgs e)
        {
            await App.Database.Save(new Todo { Task = (string) sender });
            RefreshData();
        }

        private async void OnTodoChanged(object sender, EventArgs e)
        {
            await App.Database.UpdateTodo((Todo)sender); 
            RefreshData();
        }

        private async void OnTodoDeleted(object sender, EventArgs e)
        {
            bool ans = sender == null || await DisplayAlert("Delete Task ?", "Delete a task will permanently remove it from your device.", "Yes", "No");
            if (ans)
                await App.Database.DeleteTodo((Todo)sender);
            RefreshData();
        }

        private async void OnTodoForceDeleted(object sender, EventArgs e)
        {
            await App.Database.DeleteTodo((Todo)sender);
            RefreshData();
        }

        private async void Header_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"AssignmentsAE");
        }
    }
}