using StudyPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEvents : ContentPage
    {
        public PageEvents()
        {
            InitializeComponent();
        }
        private async void RefreshData()
        {
            List<Event> events = await App.Database.GetEvents();
            BindableLayout.SetItemsSource(dataView, events);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshData();
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"EventsAE?Title=Add Activity&Type={PageType.Add}");
        }

        private async void OnCardClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"EventsAE?Title=Edit Activity&Type={PageType.Edit}&ID={(int)sender}");
        }

        private void OnRefreshing(object sender, EventArgs e)
        {
            RefreshData();
            refresh.IsRefreshing = false;
        }
    }
}