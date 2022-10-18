using StudyPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageClassroom : ContentPage
    {
        List<DayOfWeek> dayOfWeeks = new List<DayOfWeek> { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };
        List<int> Semester = new List<int> { 1, 2, 3 };
        public PageClassroom()
        {
            InitializeComponent();
        }

        private async void RefreshData()
        {
            List<GroupClassroom> group = new List<GroupClassroom>();
            foreach (DayOfWeek day in dayOfWeeks)
            {
                GroupClassroom classrooms = new GroupClassroom(day, await App.Database.GetClassrooms(day));
                if (classrooms.Any())
                    group.Add(classrooms);
            }
            BindableLayout.SetItemsSource(dataView, group);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshData();
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"ClassroomAE?Title=Add Class&Type={PageType.Add}");
        }

        private async void OnCardClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"ClassroomAE?Title=Edit Class&Type={PageType.Edit}&ID={(int)sender}");
        }

        private void OnRefreshing(object sender, EventArgs e)
        {
            RefreshData();
            refresh.IsRefreshing = false;
        }

        private async void OnCourses_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"Courses");
        }
    }
}