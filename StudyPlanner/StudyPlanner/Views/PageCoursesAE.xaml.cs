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
    public partial class PageCoursesAE : ContentPage
    {
        public int ID { get; set; }
        public new string Title { get; set; }
        public string Type { get; set; }

        public Course Course { get; set; }

        private bool QueryPropertyComplete = false;

        public PageCoursesAE()
        {
            InitializeComponent();
            BindingContext = this;

        }
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            Console.WriteLine(Course);
            await App.Database.Save(Course);
            Shell.Current.SendBackButtonPressed();
        }

        private async void OnUpdateClicked(object sender, EventArgs e)
        {
            await App.Database.UpdateCourse(Course);
            Shell.Current.SendBackButtonPressed();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool ans = await DisplayAlert("Delete Courses ?", "Delete a class will permanently remove it from your device.", "Yes", "No");
            if (ans)
            {
                await App.Database.DeleteCourse(Course);
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

                    Course = new Course();
                }
                else if (Type == PageType.Edit)
                {
                    ToolbarItem itemUpdate = new ToolbarItem { IconImageSource = new FontImageSource { FontFamily = "Icons", Glyph = "done", Size = 24 } };
                    itemUpdate.Clicked += OnUpdateClicked;
                    ToolbarItems.Add(itemUpdate);
                    ToolbarItem itemDelete = new ToolbarItem { IconImageSource = new FontImageSource { FontFamily = "Icons", Glyph = "delete", Size = 24 } };
                    itemDelete.Clicked += OnDeleteClicked;
                    ToolbarItems.Add(itemDelete);

                  Course = await App.Database.GetCourse(ID);
                }
                OnPropertyChanged(nameof(Course));
            }

            if (propertyName == "QueryAttributes")
            {
                QueryPropertyComplete = true;
                ToolbarItems.Clear();
            }



        }
    }
}