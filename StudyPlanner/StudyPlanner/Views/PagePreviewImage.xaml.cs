using StudyPlanner.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class PagePreviewImage : ContentPage
    {
        public string ID { get; set; }
        public ImageData Image { get; set; }

        private bool QueryPropertyComplete = false;

        public PagePreviewImage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (QueryPropertyComplete)
            {
                QueryPropertyComplete = false;
                Image = await App.Database.GetImage(Guid.Parse(ID));
                OnPropertyChanged(nameof(Image));
            }
            if (propertyName == "QueryAttributes")
                QueryPropertyComplete = true;
        }
    }
}