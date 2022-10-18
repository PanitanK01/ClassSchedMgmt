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
    public partial class PageAssignmentsAE : ContentPage
    {
        public int ID { get; set; }
        public new string Title { get; set; }
        public string Type { get; set; }

        public Todo Assignments { get; set; }

        private bool QueryPropertyComplete = false;
        public PageAssignmentsAE()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void ChangeDateTime()
        {
            // Make date start at 00:00 and end at 23:59
            Assignments.StartDate = Assignments.StartDate.Date + new TimeSpan(0, 0, 0);
            Assignments.EndDate = Assignments.EndDate.Date + new TimeSpan(23, 59, 59);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            ChangeDateTime();
            await App.Database.Save(Assignments);
            Shell.Current.SendBackButtonPressed();
        }

        private async void OnUpdateClicked(object sender, EventArgs e)
        {
            ChangeDateTime();
            await App.Database.Save(Assignments);

            Shell.Current.SendBackButtonPressed();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool ans = await DisplayAlert("Delete Event ?", "Delete a event will permanently remove it from your device.", "Yes", "No");
            if (ans)
            {
                await App.Database.Save(Assignments);
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

                    Assignments = new Todo();
                }
                else if (Type == PageType.Edit)
                {
                    ToolbarItem itemUpdate = new ToolbarItem { IconImageSource = new FontImageSource { FontFamily = "Icons", Glyph = "done", Size = 24 } };
                    itemUpdate.Clicked += OnUpdateClicked;
                    ToolbarItems.Add(itemUpdate);
                    ToolbarItem itemDelete = new ToolbarItem { IconImageSource = new FontImageSource { FontFamily = "Icons", Glyph = "delete", Size = 24 } };
                    itemDelete.Clicked += OnDeleteClicked;
                    ToolbarItems.Add(itemDelete);

                   Assignments = await App.Database.GetTodo(ID);
                }
                OnPropertyChanged(nameof(Todo));
            }

            if (propertyName == "QueryAttributes")
            {
                QueryPropertyComplete = true;
                ToolbarItems.Clear();
            }
        }
        private void OnStartDateSelected(object sender, EventArgs e)
        {
            // Set new minimum date for "Input : End Date"
            OnPropertyChanged(nameof(Todo));
        }
    }
}