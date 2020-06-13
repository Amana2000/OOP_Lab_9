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

namespace App1
{
	[Activity(Label = "Second_activity")]
	class Second_activity:Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.second_activity);

			Button button = FindViewById<Button>(Resource.Id.button12);
			to_textview();
			button.Click += delegate { this.Finish(); };// кнопка назад, возвращает к основной activity
		}
		//выводит значения времени на экран
		private void to_textview() 
		{
			TextView textView_time = FindViewById<TextView>(Resource.Id.textView1);
			TextView textView_hours = FindViewById<TextView>(Resource.Id.textView3);
			TextView textView_minutes = FindViewById<TextView>(Resource.Id.textView5);
			TextView textView_seconds = FindViewById<TextView>(Resource.Id.textView7);
			textView_time.Text = set_time().To_Str();
			textView_hours.Text = set_time().To_hours().ToString();
			textView_minutes.Text = set_time().To_minutes().ToString();
			textView_seconds.Text = set_time().To_seconds().ToString();

		}

		// получает значения времени от основного activity и помещает их в объект класса Time
		private Time set_time()
		{
			int hours = Intent.GetIntExtra("hours", 0);
			int minutes = Intent.GetIntExtra("minutes", 0);
			int seconds = Intent.GetIntExtra("seconds", 0);
			Time t = new Time();
			t.Set_time(hours, minutes, seconds);
			return t;
		}
	}
}