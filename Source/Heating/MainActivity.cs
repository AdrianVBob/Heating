using Android.App;
using Android.Widget;
using Android.OS;

namespace Heating
{
    [Activity(Label = "Heating", Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var state = FindViewById<EditText>(Resource.Id.userName);



            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
    }
}

