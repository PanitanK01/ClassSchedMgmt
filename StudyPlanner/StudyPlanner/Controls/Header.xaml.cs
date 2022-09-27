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
    public partial class Header : ContentView
    {
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(Header), string.Empty);
        public static BindableProperty EnableClickedProperty = BindableProperty.Create(nameof(EnableClicked), typeof(bool), typeof(Header), false);

        public event EventHandler Clicked;

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public bool EnableClicked
        {
            get => (bool)GetValue(EnableClickedProperty);
            set => SetValue(EnableClickedProperty, value);
        }

        public Header()
        {
            InitializeComponent();
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}