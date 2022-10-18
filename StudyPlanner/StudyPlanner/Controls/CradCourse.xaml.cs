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
    public partial class CradCourse : ContentView
    {
        public static readonly BindableProperty IDProperty = BindableProperty.Create(nameof(ID), typeof(int), typeof(CardTimeActive), null);
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CradCourse), string.Empty);
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(CardTimeActive), string.Empty);
       
        public event EventHandler Clicked;

        public int ID
        {
            get => (int)GetValue(IDProperty);
            set => SetValue(IDProperty, value);
        }
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }
       
        public CradCourse()
        {
            InitializeComponent();
        }
        private void OnCardClicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(ID, EventArgs.Empty);
        }
    }
}