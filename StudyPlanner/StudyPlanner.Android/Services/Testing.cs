using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StudyPlanner.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(StudyPlanner.Droid.Services.Testing))]
namespace StudyPlanner.Droid.Services
{
    public class Testing : ITesting
    {
        public async Task Share(string text, List<string> filesPath)
        {
            var context = Android.App.Application.Context;

            Intent baseIntent = new Intent(Intent.ActionSendMultiple);
            baseIntent.PutExtra(Intent.ExtraText, "This is a text");
            baseIntent.SetFlags(ActivityFlags.GrantReadUriPermission);
            baseIntent.SetType("image/*");


            var URIs = new List<IParcelable>();

            filesPath.ForEach(path => { URIs.Add(AndroidX.Core.Content.FileProvider.GetUriForFile(context, "com.companyname.studyplanner.fileprovider", new Java.IO.File(path))); });

            baseIntent.PutParcelableArrayListExtra(Intent.ExtraStream, URIs);
            //baseIntent.PutExtra(Intent.ExtraStream, AndroidX.Core.Content.FileProvider.GetUriForFile(context, "com.companyname.studyplanner.fileprovider", new Java.IO.File(filesPath[0])));

            

            Intent shareIntent = Intent.CreateChooser(baseIntent, "Share Note");
            shareIntent.SetFlags(ActivityFlags.NewTask);
            shareIntent.AddFlags(ActivityFlags.GrantReadUriPermission);
            context.StartActivity(shareIntent);
        }
    }
}