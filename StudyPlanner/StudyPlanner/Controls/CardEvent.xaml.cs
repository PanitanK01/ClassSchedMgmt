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
    public partial class CardEvent : ContentView
    {
        public static readonly BindableProperty IDProperty = BindableProperty.Create(nameof(ID), typeof(int), typeof(CardTimeActive), null);
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CardEvent), string.Empty);
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(CardTimeActive), string.Empty);
        public static readonly BindableProperty LocationProperty = BindableProperty.Create(nameof(Location), typeof(string), typeof(CardTimeActive), string.Empty);
        public static readonly BindableProperty OrganizerProperty = BindableProperty.Create(nameof(Organizer), typeof(string), typeof(CardTimeActive), string.Empty);
        public static readonly BindableProperty StartDateProperty = BindableProperty.Create(nameof(StartDate), typeof(DateTime), typeof(CardTimeActive), null);

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
        public string Location
        {
            get => (string)GetValue(LocationProperty);
            set => SetValue(LocationProperty, value);
        }
        public string Organizer
        {
            get => (string)GetValue(OrganizerProperty);
            set => SetValue(OrganizerProperty, value);
        }
        public DateTime StartDate
        {
            get => (DateTime)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }

        public CardEvent()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (IsSet(StartDateProperty))
            {
                Color[] dayColor = Application.Current.Resources["DayColor"] as Color[];
                lb_title.TextColor = dayColor[(int)StartDate.DayOfWeek];
            }
        }

        private void OnCardClicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(ID, EventArgs.Empty);
        }
    }
}