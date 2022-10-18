using StudyPlanner.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyPlanner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Input : ContentView
    {
        public static BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(InputType), typeof(Input), InputType.Entry);
        public static BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(string), typeof(Input), string.Empty);
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(Input), string.Empty);
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(Input), string.Empty, BindingMode.TwoWay);
        public static BindableProperty KeyboardTypeProperty = BindableProperty.Create(nameof(KeyboardType), typeof(Keyboard), typeof(Input), Keyboard.Default);

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(Input), null, BindingMode.TwoWay);
        //ต้องแก้ ItemsSource
        // public static BindableProperty ItemDisplayBindingProperty = BindableProperty.Create(nameof(ItemDisplayBinding), typeof(BindingBase), typeof(Input));
        public static BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(Input), null, BindingMode.TwoWay);

        public static BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(Input), DateTime.Now, BindingMode.TwoWay);
        public static BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(Input), null);

        public static BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan), typeof(Input), TimeSpan.Zero, BindingMode.TwoWay);

        public static BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(string), typeof(Input), string.Empty, BindingMode.TwoWay);

        public static BindableProperty ImagesProperty = BindableProperty.Create(nameof(Images), typeof(string), typeof(Input), string.Empty, BindingMode.TwoWay);
        public static BindableProperty RemovedImagesProperty = BindableProperty.Create(nameof(RemovedImages), typeof(IList), typeof(Input), null, BindingMode.TwoWay);

        public event EventHandler DateSelected;
        public event EventHandler Completed;
        public event EventHandler ImageClicked;
        public event EventHandler ImageAdded;
        public event EventHandler ErrorHandler;

        public InputType Type
        {
            get => (InputType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }
        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public Keyboard KeyboardType
        {
            get => (Keyboard)GetValue(KeyboardTypeProperty);
            set => SetValue(KeyboardTypeProperty, value);
        }
        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
       
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }
        public DateTime MinimumDate
        {
            get => (DateTime)GetValue(MinimumDateProperty);
            set => SetValue(MinimumDateProperty, value);
        }

        public TimeSpan Time
        {
            get => (TimeSpan)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        public string SelectedColor
        {
            get => (string)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public string Images
        {
            get => (string)GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }
        public IList RemovedImages
        {
            get => (IList)GetValue(RemovedImagesProperty);
            set => SetValue(RemovedImagesProperty, value);
        }

        public List<string> tagColor = new List<string> { "#FFF14235", "#FFE91E63", "#FF9C27B0", "#FF673AB7", "#FF3F51B5", "#FF2196F3", "#FF03A9F4", "#FF00BCD4", "#FF009688", "#FF4CAF50", "#FF8BC34A", "#FFCDDC39", "#FFFFEB3B", "#FFFFC107", "#FFFF9800", "#FFFF5722", "#FF795548", "#FF9E9E9E", "#FF607D8B", "#FFEAEAEA", "#FF000000" };
        public List<string> TagColor { get => tagColor; }

        public ICommand DeleteCommand { get => new Command<ImageData>((image) => DeleteImages(image)); }
        public ICommand PreviewImageCommand { get => new Command<ImageData>((image) => PreviewImage(image));  }

        public enum InputType
        {
            
            Entry,
            Picker,
            DatePicker, 
            TimePicker,
            Editor,
            Task,
            ImagePicker,
            ColorPicker,
            ColorPickerV2
        }


        public Input()
        {
            InitializeComponent();
        }


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (IsSet(HeaderProperty))
                header.IsVisible = Header != string.Empty && Header != null;
            if (ItemsSource == null)
                ItemsSource = new ObservableCollection<ImageData>();
            if (RemovedImages == null)
                RemovedImages = new ObservableCollection<ImageData>();
            if (IsSet(SelectedColorProperty) && !string.IsNullOrWhiteSpace(SelectedColor))
            {
                foreach (Frame c in ((StackLayout)((ScrollView)inputFrame.Children.First()).Children.First()).Children)
                {
                    c.CornerRadius = 4;
                    c.HasShadow = false;
                    c.FadeTo(0.5, 150);

                    if (c.BackgroundColor.ToHex() == SelectedColor)
                    {
                        c.CornerRadius = 16;
                        c.HasShadow = true;
                        c.FadeTo(1, 150);

                        ScrollView scrollView = (ScrollView)inputFrame.Children.First();
                        scrollView.ScrollToAsync(c, ScrollToPosition.Center, true);
                    }
                }
            }
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            DateSelected?.Invoke(Date, EventArgs.Empty);
        }

        private void OnEditorCompleted(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Contains(Environment.NewLine))
            {
                var editor = (Editor)sender;
                string text = e.OldTextValue;
                editor.Text = string.Empty;
                editor.Unfocus();
                if (!string.IsNullOrWhiteSpace(text))
                    Completed?.Invoke(text, EventArgs.Empty);
            }
        }

        private async void OnTakePhotoClicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    ImageData img = new ImageData { Bytes = StreamToByteArray(stream) };
                    ItemsSource.Add(img);
                    AddImages(img.Guid);
                    ImageAdded?.Invoke(img, EventArgs.Empty);
                }

            }
            catch (Exception ex) { ErrorHandler?.Invoke(ex, EventArgs.Empty); }
        }

        private async void OnAddPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                var imageResult = await FilePicker.PickMultipleAsync(new PickOptions { FileTypes = FilePickerFileType.Images, PickerTitle = "Add Images" });

                if (imageResult != null)
                {
                    foreach (var image in imageResult)
                    {
                        var stream = await image.OpenReadAsync();
                        ImageData img = new ImageData { Bytes = StreamToByteArray(stream) };
                        ItemsSource.Add(img);
                        AddImages(img.Guid);
                        ImageAdded?.Invoke(img, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex) { ErrorHandler?.Invoke(ex, EventArgs.Empty); }
        }

        private void AddImages(Guid guid)
        {
            try
            {
                List<string> lstr = Images == null ? new List<string>() : Images.Split('|').ToList();
                lstr.Add(guid.ToString());
                Images = string.Join("|", lstr);
            }
            catch (Exception ex) { ErrorHandler?.Invoke(ex, EventArgs.Empty); }
        }

        private void DeleteImages(ImageData image)
        {
            try
            {
                List<string> lstr = Images.Split('|').ToList();
                lstr.Remove(image.Guid.ToString());
                Images = string.Join("|", lstr);
                ItemsSource.Remove(image);
                RemovedImages.Add(image);
            } catch (Exception ex) { ErrorHandler?.Invoke(ex, EventArgs.Empty); }
        }

        private void PreviewImage(ImageData image)
        {
            ImageClicked?.Invoke(image.Guid, EventArgs.Empty);
        }

        private byte[] StreamToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            byte[] bytes = ms.ToArray();
            return bytes;
        }

        private void OnColorClicked(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            StackLayout parent = (StackLayout)frame.Parent;

            SelectedColor = frame.BackgroundColor.ToHex();

            foreach (Frame c in parent.Children)
            {
                c.CornerRadius = 4;
                c.HasShadow = false;
                c.FadeTo(0.5, 150);
            }

            frame.CornerRadius = 16;
            frame.HasShadow = true;
            frame.FadeTo(1, 150);
        }
    }
}