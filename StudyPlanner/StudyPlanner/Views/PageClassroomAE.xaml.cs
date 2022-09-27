using StudyPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ID), "ID")]
    [QueryProperty(nameof(Title), "Title")]
    [QueryProperty(nameof(Type), "Type")]
    public partial class PageClassroomAE : ContentPage
    {
        public int ID { get; set; }
        public new string Title { get; set; }
        public string Type { get; set; }
        public Classroom Class { get; set; }

        private bool QueryPropertyComplete = false;

        List<DayOfWeek> dayOfWeeks = new List<DayOfWeek> { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };
        // List<NumSemester> semester = new List<Semester> { Semester.1, Semester.2, Semester.3, }; //
        // List<year> semester = new List<Semester> { Semester.1, Semester.2, Semester.3, }; //
        // List<code> semester = new List<Semester> { Semester.1, Semester.2, Semester.3, }; //
        public PageClassroomAE()
        {
            InitializeComponent();
            ip_dayPicker.ItemsSource = dayOfWeeks;

            BindingContext = this;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await App.Database.Save(Class);
            Shell.Current.SendBackButtonPressed();
        }

        private async void OnUpdateClicked(object sender, EventArgs e)
        {
            await App.Database.UpdateClassroom(Class);
            Shell.Current.SendBackButtonPressed();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool ans = await DisplayAlert("Delete Class ?", "Delete a class will permanently remove it from your device.", "Yes", "No");
            if (ans)
            {
                await App.Database.DeleteClassroom(Class);
                Shell.Current.SendBackButtonPressed();
            }
        }

        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (QueryPropertyComplete)
            {
                QueryPropertyComplete = false;

                if (Type == PageType.Add)
                {
                    ToolbarItem itemSave = new ToolbarItem { IconImageSource = new FontImageSource { FontFamily = "Icons", Glyph = "done", Size = 24 } };
                    itemSave.Clicked += OnSaveClicked;
                    ToolbarItems.Add(itemSave);

                    Class = new Classroom();
                }
                else if (Type == PageType.Edit)
                {
                    ToolbarItem itemUpdate = new ToolbarItem { IconImageSource = new FontImageSource { FontFamily = "Icons", Glyph = "done", Size = 24 } };
                    itemUpdate.Clicked += OnUpdateClicked;
                    ToolbarItems.Add(itemUpdate);
                    ToolbarItem itemDelete = new ToolbarItem { IconImageSource = new FontImageSource { FontFamily = "Icons", Glyph = "delete", Size = 24 } };
                    itemDelete.Clicked += OnDeleteClicked;
                    ToolbarItems.Add(itemDelete);

                    Class = await App.Database.GetClassroom(ID);
                }
                OnPropertyChanged(nameof(Class));
            }

            if (propertyName == "QueryAttributes")
            {
                QueryPropertyComplete = true;
                ToolbarItems.Clear();
            }
        }
    }
}