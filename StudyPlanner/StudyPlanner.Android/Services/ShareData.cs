using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StudyPlanner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(StudyPlanner.Droid.Services.ShareData))]
namespace StudyPlanner.Droid.Services
{
    public class ShareData : IShareData
    {
        // Reference
        // - https://developer.android.com/training/secure-file-sharing/setup-sharing
        // - https://github.com/xamarin/Essentials/issues/130

        public Task<bool> Share(List<string> filePaths, string subject, string text)
        {
            // Problem Found
            // - Facebook : only images can be share with current method (Might work with Xamarin.Facebook https://www.nuget.org/packages/Xamarin.Facebook/)
            // - Line : only images can be share with current method (Have to use Line URL scheme to send text but can't share images within the url https://developers.line.biz/en/docs/line-login/using-line-url-scheme/#sending-text-messages)

            var context = Android.App.Application.Context;

            // setup intent for sharing data
            Intent baseIntent = new Intent(Intent.ActionSendMultiple);
            baseIntent.SetFlags(ActivityFlags.GrantReadUriPermission);
            baseIntent.SetType("image/*");

            // put subject into intent (Support only some app like Gmail, Google Keep)
            baseIntent.PutExtra(Intent.ExtraSubject, subject);

            // make parcel for all file and put into intent
            var FilesParcel = new List<IParcelable>();
            filePaths.ForEach(path => { FilesParcel.Add(AndroidX.Core.Content.FileProvider.GetUriForFile(context, $"{AppInfo.PackageName}.fileprovider", new Java.IO.File(path))); });
            baseIntent.PutParcelableArrayListExtra(Intent.ExtraStream, FilesParcel);

            // put text data (note) into intent
            baseIntent.PutExtra(Intent.ExtraText, text);

            // set new intent to share with NewTask Flag
            Intent shareIntent = Intent.CreateChooser(baseIntent, "Share Note");
            shareIntent.SetFlags(ActivityFlags.NewTask);
            context.StartActivity(shareIntent);

            return Task.FromResult(true);
        }
    }
}