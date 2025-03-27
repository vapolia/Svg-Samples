using Android.App;
using Android.OS;

namespace SvgAndroidNativeDemo;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        //Select where to search for embedded resources
        Vapolia.Svgs.Config.AddResourceAssembly(typeof(MainActivity));

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);
    }
}