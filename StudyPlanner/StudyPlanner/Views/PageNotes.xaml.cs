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
    public partial class PageNotes : ContentPage
    {
        public PageNotes()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void RefreshData()
        {
            List<Note> notes = await App.Database.GetNotes();
            BindableLayout.SetItemsSource(dataView, notes);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshData();

        }
        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"NotesAE?Title=Add Note&Type={PageType.Add}");
        }
        private async void OnCardClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"NotesAE?Title=Edit Note&Type={PageType.Edit}&ID={(int)sender}");
        }

        private void OnRefreshing(object sender, EventArgs e)
        {
            RefreshData();
            refresh.IsRefreshing = false;
        }

        private async void OnImageClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"PreviewImage?ID={sender}");
        }
    }
}