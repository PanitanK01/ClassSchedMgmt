using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyPlanner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Picker : ContentView
    {
        public static BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(string), typeof(Input), string.Empty);

        public event EventHandler SelectedIndexChanged;

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public IList ItemsSource
        {
            get => inputPicker?.ItemsSource;
            set => inputPicker.ItemsSource = value;
        }

        public object SelectedItem
        {
            get => inputPicker?.SelectedItem;
            set => inputPicker.SelectedItem = value;
        }

        public BindingBase ItemDisplayBinding
        {
            get => inputPicker?.ItemDisplayBinding;
            set => inputPicker.ItemDisplayBinding = value;
        }

        public string Placeholder
        {
            get => inputPicker?.Title;
            set => inputPicker.Title = value;
        }

        

        public Picker()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (IsSet(HeaderProperty))
                header.IsVisible = Header != string.Empty && Header != null;
        }

        private void inputPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged.Invoke(sender, e);
        }
    }
}