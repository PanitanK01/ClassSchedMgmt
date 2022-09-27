using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyPlanner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoData : ContentView
    {

        public static BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(NoData), string.Empty);
        public static BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(NoData), string.Empty);

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public NoData()
        {
            InitializeComponent();
        }
    }
}