using StudyPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// ---------- TODO ----------
// Everything about Images

namespace StudyPlanner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardNote : ContentView
    {
        public static readonly BindableProperty IDProperty = BindableProperty.Create(nameof(ID), typeof(int), typeof(CardNote), null);
        public static BindableProperty ImagesProperty = BindableProperty.Create(nameof(Images), typeof(List<ImageData>), typeof(CardNote), null);
        public static BindableProperty TagColorProperty = BindableProperty.Create(nameof(TagColor), typeof(string), typeof(CardNote), "black");
        public static BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CardNote), string.Empty);
        public static BindableProperty ClassProperty = BindableProperty.Create(nameof(Class), typeof(string), typeof(CardNote), string.Empty);
        public static BindableProperty NoteProperty = BindableProperty.Create(nameof(Note), typeof(string), typeof(CardNote), string.Empty);

        public event EventHandler Clicked;
        public event EventHandler ImageClicked;

        public int ID
        {
            get => (int)GetValue(IDProperty);
            set => SetValue(IDProperty, value);
        }
        public List<ImageData> Images
        {
            get => (List<ImageData>)GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }
        public string TagColor
        {
            get => (string)GetValue(TagColorProperty);
            set => SetValue(TagColorProperty, value);
        }
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public string Class
        {
            get => (string)GetValue(ClassProperty);
            set => SetValue(ClassProperty, value);
        }
        public string Note
        {
            get => (string)GetValue(NoteProperty);
            set => SetValue(NoteProperty, value);
        }

        public ICommand PreviewImageCommand { get => new Command<ImageData>((image) => PreviewImage(image)); }

        public CardNote()
        {
            InitializeComponent();
        }

        private void OnCardClicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(ID, EventArgs.Empty);
        }

        private void PreviewImage(ImageData image)
        {
            ImageClicked?.Invoke(image.Guid, EventArgs.Empty);
        }
    }
}