using System;
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
    public partial class ToggleButton : ContentView
    {
        public static BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(ToggleButton), false, BindingMode.TwoWay);
        public static BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(ToggleButton), Color.Black);
        public static BindableProperty NormalIconProperty = BindableProperty.Create(nameof(NormalIcon), typeof(string), typeof(ToggleButton), string.Empty);
        public static BindableProperty ToggledIconProperty = BindableProperty.Create(nameof(ToggledIcon), typeof(string), typeof(ToggleButton), string.Empty);

        public event EventHandler ToggledChanged;

        public bool IsToggled
        {
            get => (bool)GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
        public string NormalIcon
        {
            get => (string)GetValue(NormalIconProperty);
            set => SetValue(NormalIconProperty, value);
        }
        public string ToggledIcon
        {
            get => (string)GetValue(ToggledIconProperty);
            set => SetValue(ToggledIconProperty, value);
        }

        public ToggleButton()
        {
            InitializeComponent();
        }

        private void swapIcon()
        {
            if (IsToggled)
                icon.Glyph = ToggledIcon;
            else
                icon.Glyph = NormalIcon;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (IsSet(IsToggledProperty) && IsSet(NormalIconProperty) && IsSet(ToggledIconProperty))
                swapIcon();
        }

        private void OnToggledChanged(object sender, EventArgs e)
        {
            IsToggled = !IsToggled;
            swapIcon();
            ToggledChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}