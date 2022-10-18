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
    public partial class PageHome : ContentPage
    {
        public PageHome()
        {
            InitializeComponent();
        }
        private async void RefreshData()
        {
            List<Classroom> classrooms = await App.Database.GetClassrooms(DateTime.Now.DayOfWeek);
            BindableLayout.SetItemsSource(classView, classrooms);
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

      

        private async void ProfileClicked (object sender, EventArgs e) 
        {
            await Shell.Current.GoToAsync($"Profile");
        }
    }
}