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
    public partial class PageCourses : ContentPage
    {
        public PageCourses()
        {
            InitializeComponent();
        }
        private async void RefreshData()
        {
            List<Course> course = await App.Database.GetCourses();
            Console.WriteLine(course);
            BindableLayout.SetItemsSource(dataView, course);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshData();
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"CoursesAE?Title=Add Course&Type={PageType.Add}");
        }

        private void refresh_Refreshing(object sender, EventArgs e)
        {
            RefreshData();
            refresh.IsRefreshing = false;
        }

        private async void CardCourse_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"CoursesAE?Title=Edit Course&Type={PageType.Edit}&ID={(int)sender}");
        }

       
    }
}