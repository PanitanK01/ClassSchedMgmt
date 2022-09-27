using StudyPlanner.Database;
using StudyPlanner.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyPlanner
{
    public partial class App : Application
    {
        private static SPDB database;

        public static SPDB Database
        {
            get
            {
                if (database == null)
                    database = new SPDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SPDB.db3"));
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("ClassroomAE", typeof(PageClassroomAE));
            Routing.RegisterRoute("EventsAE", typeof(PageEventsAE));
            Routing.RegisterRoute("NotesAE", typeof(PageNotesAE));
            Routing.RegisterRoute("PreviewImage", typeof(PagePreviewImage));
            Routing.RegisterRoute("AssignmentsAE", typeof(PageAssignmentsAE));

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
