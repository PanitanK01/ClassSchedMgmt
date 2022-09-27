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
    public partial class PageAssignmentsAE : ContentPage
    {
        public PageAssignmentsAE()
        {
            InitializeComponent();
        }
         private void OnStartDateSelected(object sender, EventArgs e)
        {
            // Set new minimum date for "Input : End Date"
            OnPropertyChanged(nameof(Event));
        }
    }
}