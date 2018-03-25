
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.Animations;
using RestSharp;
using Newtonsoft.Json;


//[Activity (Label = "YourActivityName",Theme = "@android:style/Theme.NoTitleBar")]
namespace Heating
{
    [Activity(Label = "Login", MainLauncher = true)]

    public class Login : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            

            var user = FindViewById<EditText>(Resource.Id.userName);
            var password = FindViewById<EditText>(Resource.Id.password);
            var log = FindViewById<Button>(Resource.Id.logIn);
            var crt = FindViewById<Button>(Resource.Id.create);
        



            // Create your application here

            log.Click += delegate
             {
                 Animation anim = AnimationUtils.LoadAnimation(ApplicationContext, Resource.Animation.fadeIn);
                 log.StartAnimation(anim);

                 var client = new RestClient("http://localhost:8080");
                 var request = new RestRequest("api/Login/?username=" + user + "&password=" + password);
                 var result = client.Execute(request);
                 string res = result.Content;
                 //  res = JsonConverter.DeserializeObject<System.Collections.Generic.>(result.Content);


                 if (res.Equals("1"))
                 {
                     Intent intent = new Intent(this, typeof(MainActivity));

                     StartActivity(intent);
                     Finish();
                 }
                 return; 

                //   var client = new RestClient("http://chsapi.azurewebsites.net");
               // var restquest = new RestrictionResults
 
             };
            crt.Click += delegate
             {
                 Animation anim = AnimationUtils.LoadAnimation(ApplicationContext, Resource.Animation.fadeIn);
                 crt.StartAnimation(anim);
                 Intent intent = new Intent(this, typeof(Create));


             };
            
            
        }
    }
}