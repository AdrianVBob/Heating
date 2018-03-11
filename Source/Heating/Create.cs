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
using RestSharp;
using Android.Views.Animations;

namespace Heating
{
    [Activity(Label = "Create")]
    public class Create : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateAccount);

            Animation anim = AnimationUtils.LoadAnimation(ApplicationContext, Resource.Animation.fadeIn);
            Animation anim2 = AnimationUtils.LoadAnimation(ApplicationContext, Resource.Animation.abc_fade_out);

            var username = FindViewById<EditText>(Resource.Id.userNamecrt);
            var password = FindViewById<EditText>(Resource.Id.passwordcrt);
            var brdId = FindViewById<EditText>(Resource.Id.brdId);
            var crtBtn = FindViewById<Button>(Resource.Id.crtacc);

            var client = new RestClient("http://");


            crtBtn.Click += delegate
            {
               
                crtBtn.StartAnimation(anim);
                crtBtn.StartAnimation(anim2);
                

                var request = new RestRequest("api/Create//");
                var result = client.Execute(request);
                string res = result.Content;


                if (res.Equals("1"))
                {
                    Toast.MakeText(this, "Account succesfuly created.", ToastLength.Long);
                    Intent intent = new Intent(this, typeof(Login));

                }

                else
                {
                    Toast.MakeText(this, "Error", ToastLength.Long);
                }


            };


        }
    }
}