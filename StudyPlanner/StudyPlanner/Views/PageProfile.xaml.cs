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
    public partial class PageProfile : ContentPage
    {
        public PageProfile()
        {
            InitializeComponent();
        }

        private void refresh_Refreshing(object sender, EventArgs e)
        {
            //RefreshData();
            refresh.IsRefreshing = false;
        }

        private async void Header_Clicked(object sender, EventArgs e)
        {
           // await Shell.Current.GoToAsync($"ProfileE?Title=Edit Profile&Type={PageType.Edit}");
        }
    }
}