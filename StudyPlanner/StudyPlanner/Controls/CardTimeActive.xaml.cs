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
    public partial class CardTimeActive : ContentView
    {
        public static readonly BindableProperty IDProperty = BindableProperty.Create(nameof(ID), typeof(int), typeof(CardTimeActive), null);
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CardTimeActive), string.Empty);
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(CardTimeActive), string.Empty);
        public static readonly BindableProperty DayProperty = BindableProperty.Create(nameof(Day), typeof(DayOfWeek), typeof(CardTimeActive), DayOfWeek.Sunday);
        public static readonly BindableProperty StartDateProperty = BindableProperty.Create(nameof(StartDate), typeof(DateTime), typeof(CardTimeActive), null);
        public static readonly BindableProperty EndDateProperty = BindableProperty.Create(nameof(EndDate), typeof(DateTime), typeof(CardTimeActive), null);
        public static readonly BindableProperty StartTimeProperty = BindableProperty.Create(nameof(StartTime), typeof(TimeSpan), typeof(CardTimeActive), null);
        public static readonly BindableProperty EndTimeProperty = BindableProperty.Create(nameof(EndTime), typeof(TimeSpan), typeof(CardTimeActive), null);

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
        public DayOfWeek Day
        {
            get => (DayOfWeek)GetValue(DayProperty);
            set => SetValue(DayProperty, value);
        }
        public DateTime StartDate
        {
            get => (DateTime)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }
        public DateTime EndDate
        {
            get => (DateTime)GetValue(EndDateProperty);
            set => SetValue(EndDateProperty, value);
        }
        public TimeSpan StartTime
        {
            get => (TimeSpan)GetValue(StartTimeProperty);
            set => SetValue(StartTimeProperty, value);
        }
        public TimeSpan EndTime
        {
            get => (TimeSpan)GetValue(EndTimeProperty);
            set => SetValue(EndTimeProperty, value);
        }

        public CardTimeActive()
        {
            InitializeComponent();
        }

        public void IsTimeActive()
        {
            DayOfWeek dow = DateTime.Now.DayOfWeek;
            DateTime dateNow = DateTime.Now;
            TimeSpan timeNow = DateTime.Now.TimeOfDay;

            if ((StartTime != null && EndTime != null && Day == dow && StartTime <= timeNow && timeNow <= EndTime) || (StartDate != null && EndDate != null && StartTime != null && EndTime != null && StartDate <= dateNow && dateNow <= EndDate && StartTime <= timeNow && timeNow <= EndTime))
            {
                Resources["CardStyle"] = Application.Current.Resources["CardActiveStyle"];
                Resources["TitleCardStyle"] = Application.Current.Resources["TitleCardActiveStyle"];
                Resources["DescriptionStyle"] = Application.Current.Resources["DescriptionActiveStyle"];
            }
            else
            {
                Resources["CardStyle"] = Application.Current.Resources["CardInactiveStyle"];
                Resources["TitleCardStyle"] = Application.Current.Resources["TitleCardInactiveStyle"];
                Resources["DescriptionStyle"] = Application.Current.Resources["DescriptionInactiveStyle"];
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            IsTimeActive();
        }

        private void OnCardClicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(ID, EventArgs.Empty);
        }

    }
}