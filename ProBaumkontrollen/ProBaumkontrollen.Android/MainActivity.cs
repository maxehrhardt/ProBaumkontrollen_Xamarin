using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android;
using Android.Support.Design.Widget;

namespace ProBaumkontrollen.Droid
{
    [Activity(Label = "ProBaumkontrollen", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            RequestStoragePermission();
        }

        void RequestStoragePermission()
        {

            ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 1);
            //if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.Camera))
            //{
            //    // Provide an additional rationale to the user if the permission was not granted
            //    // and the user would benefit from additional context for the use of the permission.
            //    // For example if the user has previously denied the permission.
                
            //    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 1);
  
            //}
            //else
            //{
            //    // Camera permission has not been granted yet. Request it directly.
            //    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.Camera }, 1);
            //}
        }
    }
}

