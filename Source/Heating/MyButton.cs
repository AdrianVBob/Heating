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
using Android.Content.Res;
using Android.Content.PM;
using Android.Util;

namespace Heating
{
    class MyButton : Button
    {
       public double BorderWidth { get; set; }

        public MyButton(Context context, Android.Util.IAttributeSet art) : base(context, art)
        {

            var button = new MyButton(context, art);
            button.BorderWidth = 20;

            

        }

        
    }
}