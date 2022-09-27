using StudyPlanner.Models;
using StudyPlanner.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ID), "ID")]
    [QueryProperty(nameof(Title), "Title")]
    [QueryProperty(nameof(Type), "Type")]
    public partial class PageNotesAE : ContentPage
    {
        public int ID { get; set; }
        public new string Title { get; set; }
        public string Type { get; set; }
        public Note Note { get; set; }
        public ObservableCollection<ImageData> Images { get; set; }
        public ObservableCollection<ImageData> RemovedImages { get; set; }
        public Note BackupNote { get; set; } // Backup unsaved note to bring data back after go to PreviewImage page
        public List<string> Class { get; set; } = new List<string>();

        private bool QueryPropertyComplete = false;
        private bool ReloadToolbar = false;
        public PageNotesAE()
        {
            InitializeComponent();
            BindingContext = this;
        }
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await App.Database.Save(Note);
            foreach (ImageData image in RemovedImages)
                await App.Database.DeleteImage(image);
            Shell.Current.SendBackButtonPressed();
        }

        private async void OnUpdateClicked(object sender, EventArgs e)
        {
            await App.Database.UpdateNote(Note);
            foreach (ImageData image in RemovedImages)
                await App.Database.DeleteImage(image);
            Shell.Current.SendBackButtonPressed();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool ans = await DisplayAlert("Delete Note ?", "Delete a note will permanently remove it from your device.", "Yes", "No");
            if (ans)
            {
                await App.Database.DeleteNote(Note);
                foreach (string guid in Note.ImageList.Split('|'))
                    await App.Database.DeleteImage(Guid.Parse(guid));
                foreach (ImageData image in RemovedImages)
                    await App.Database.DeleteImage(image);
                Shell.Current.SendBackButtonPressed();
            }
        }

        private async void OnShareClicked(object sender, EventArgs e)
        {
            string noteText = $"--- Note from Study Planner ---\n\nTitle : {Note.Title}\nClass : {Note.Class}\n\n{Note.Text}";

            if (Device.RuntimePlatform == Device.Android)
            {
                List<string> shareFiles = new List<string>();
                if (Note.ImageList != null)
                {
                    foreach (string guid in Note.ImageList.Split('|'))
                    {
                        ImageData image = await App.Database.GetImage(Guid.Parse(guid));
                        File.WriteAllBytes(Path.Combine(FileSystem.CacheDirectory, guid + ".png"), image.Bytes);
                        shareFiles.Add(Path.Combine(FileSystem.CacheDirectory, guid + ".png"));
                    }
                }

                await Clipboard.SetTextAsync(noteText);
                await DependencyService.Get<IShareData>().Share(shareFiles, Note.Title, noteText);
            }
            else
            {
                string action = await DisplayActionSheet("Content to share ?", "Cancel", null, "Images", "Note");
                if (action == "Images")
                {
                    List<ShareFile> shareFiles = new List<ShareFile>();
                    if (Note.ImageList != null)
                    {
                        foreach (string guid in Note.ImageList.Split('|'))
                        {
                            ImageData image = await App.Database.GetImage(Guid.Parse(guid));
                            File.WriteAllBytes(Path.Combine(FileSystem.CacheDirectory, guid + ".png"), image.Bytes);
                            shareFiles.Add(new ShareFile(Path.Combine(FileSystem.CacheDirectory, guid + ".png")));
                        }
                    }
                    await Share.RequestAsync(new ShareMultipleFilesRequest { Title = "Share Images", Files = shareFiles });
                } else if (action == "Note")
                    await Share.RequestAsync(new ShareTextRequest { Title = "Share Note", Text = noteText });
            }

            
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateToolbar();

            if (BackupNote != null)
            {
                Note = BackupNote;
                // Update UI 
                tagColorPicker.SelectedColor = Note.TagColor;
              //  classPicker.SelectedItem = Note.Class; //
                titleEntry.Text = Note.Title;
                textEntry.Text = Note.Text;
                imagePicker.Images = Note.ImageList;
            }

        }

        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (QueryPropertyComplete)
            {
                QueryPropertyComplete = false;
                
                if (Type == PageType.Add)
                {
                    Note = new Note();
                }
                else if (Type == PageType.Edit)
                {
                    Note = await App.Database.GetNote(ID);

                    if (Images.Count == 0)
                    {
                        List<string> lstr = Note.ImageList?.Split('|').ToList();
                        if (lstr != null)
                            foreach (var guid in lstr)
                                Images.Add(await App.Database.GetImage(Guid.Parse(guid)));
                    }
                }

            //    List<Classroom> classrooms = await App.Database.GetClassrooms();
            //    foreach (Classroom c in classrooms)
            //        Class.Add(c.Name);
            //    if (!Class.Contains(Note.Class))
            //        Class.Insert(0, Note.Class);
            //    classPicker.ItemsSource = Class;

                OnPropertyChanged(nameof(Note));
            }

            if (ReloadToolbar)
            {
                ReloadToolbar = false;
                UpdateToolbar();
            }

            if (propertyName == "QueryAttributes")
            {
                QueryPropertyComplete = true;
                if (Note != null)
                {
                    ReloadToolbar = true;
                    OnPropertyChanged(nameof(Note));
                }
            }
        }

        private void UpdateToolbar()
        {
            ToolbarItems.Clear();
            if (Type == PageType.Add)
            {
                ToolbarItem itemSave = new ToolbarItem { IconImageSource = new FontImageSource { FontFamily = "Icons", Glyph = "done", Size = 24 } };
                itemSave.Clicked += OnSaveClicked;
                ToolbarItems.Add(itemSave);
            }
            else if (Type == PageType.Edit)
            {
                ToolbarItem itemUpdate = new ToolbarItem { IconImageSource = new FontImageSource { FontFamily = "Icons", Glyph = "done", Size = 24 } };
                itemUpdate.Clicked += OnUpdateClicked;
                ToolbarItems.Add(itemUpdate);
                ToolbarItem itemShare = new ToolbarItem { IconImageSource = new FontImageSource { FontFamily = "Icons", Glyph = "share", Size = 24 } };
                itemShare.Clicked += OnShareClicked;
                ToolbarItems.Add(itemShare);
                ToolbarItem itemDelete = new ToolbarItem { IconImageSource = new FontImageSource { FontFamily = "Icons", Glyph = "delete", Size = 24 } };
                itemDelete.Clicked += OnDeleteClicked;
                ToolbarItems.Add(itemDelete);
            }
        }

        private async void OnImageClicked(object sender, EventArgs e)
        {
            BackupNote = Note;
            await Shell.Current.GoToAsync($"PreviewImage?ID={sender}");
        }

        private async void OnImageAdded(object sender, EventArgs e)
        {
            await App.Database.Save((ImageData)sender);
        }

        private async void OnImagePickerErrorHandler(object sender, EventArgs e)
        {
            await DisplayAlert("Image Picker Error", "Please try again", "OK");
        }
    }
}