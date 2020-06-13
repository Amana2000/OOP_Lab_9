using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections;
using Java.Nio.Channels;
using System.Xml.Serialization;
using Android.Icu.Text;
using Android.Views.InputMethods;
using Android.Content;
using Android.Content.PM;

namespace App1

{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
       
        TextView txt;
        Spinner spinner;
        ArrayList operations = new ArrayList();
        Time time = new Time();
        Time time1 = new Time();
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            types_operations();
            init_spinner();
            txt = FindViewById<TextView>(Resource.Id.textView5);
            // обработчики кнопок
            FindViewById<Button>(Resource.Id.button).Click += (o, e) => print();
            FindViewById<Button>(Resource.Id.button1).Click += (o, e) => do_operations();
            FindViewById<Button>(Resource.Id.button3).Click += (o, e) => clear();
            FindViewById<Button>(Resource.Id.button2).Click += (o, e) => second_activity(); 
            


        }
        //инициализация спиннера
        private void init_spinner() 
        {
            spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, operations);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }
        //типы операций в спиннере
        private void types_operations() 
        {
            operations.Add(" ");
            operations.Add("+");
            operations.Add("-");
        }

        //печать времени в Label
        private void print() 
        {           
            if (FindViewById<EditText>(Resource.Id.editText1).Text=="" || FindViewById<EditText>(Resource.Id.editText).Text=="" || FindViewById<EditText>(Resource.Id.editText3).Text=="")
            {
                 Toast.MakeText(this, "Заполните все поля", ToastLength.Short).Show(); 
            }
            else
            {
                time.Set_time(int.Parse(FindViewById<EditText>(Resource.Id.editText1).Text), int.Parse(FindViewById<EditText>(Resource.Id.editText).Text), int.Parse(FindViewById<EditText>(Resource.Id.editText3).Text));
                txt.Text = time.To_Str();
                EditText keyboard = FindViewById<EditText>(Resource.Id.editText3);
                InputMethodManager imm = (InputMethodManager)GetSystemService(Android.Content.Context.InputMethodService);
                imm.HideSoftInputFromWindow(keyboard.WindowToken, 0);
            }
        }

        //Выполнение операций со временем
        private void do_operations() 
        {
         
            if (FindViewById<EditText>(Resource.Id.editText1).Text == "" || FindViewById<EditText>(Resource.Id.editText).Text == "" || FindViewById<EditText>(Resource.Id.editText3).Text == ""|| FindViewById<EditText>(Resource.Id.editText2).Text == "" || FindViewById<EditText>(Resource.Id.editText4).Text == "" || FindViewById<EditText>(Resource.Id.editText5).Text == "")
                {
                Toast.MakeText(this, "Заполните все поля", ToastLength.Short).Show();

                }
            else
            {
                time.Set_time(int.Parse(FindViewById<EditText>(Resource.Id.editText1).Text), int.Parse(FindViewById<EditText>(Resource.Id.editText).Text), int.Parse(FindViewById<EditText>(Resource.Id.editText3).Text));
                time1.Set_time(int.Parse(FindViewById<EditText>(Resource.Id.editText2).Text), int.Parse(FindViewById<EditText>(Resource.Id.editText4).Text), int.Parse(FindViewById<EditText>(Resource.Id.editText5).Text));
                switch (spinner.SelectedItem.ToString()) 
                {
                    case " ":
                        Toast.MakeText(this, "Выберите операцию", ToastLength.Short).Show();
                        break;
                    case "+":
                        time = time + time1;
                        txt.Text = time.To_Str();
                        EditText keyboard = FindViewById<EditText>(Resource.Id.editText5);
                        InputMethodManager imm = (InputMethodManager)GetSystemService(Android.Content.Context.InputMethodService);
                        imm.HideSoftInputFromWindow(keyboard.WindowToken, 0);
                        break;
                    case "-":
                        Time t1 = new Time();
                        time = time - time1;
                        txt.Text = time.To_Str();
                        EditText keyboard1 = FindViewById<EditText>(Resource.Id.editText5);
                        InputMethodManager imm1 = (InputMethodManager)GetSystemService(Android.Content.Context.InputMethodService);
                        imm1.HideSoftInputFromWindow(keyboard1.WindowToken, 0);
                        break;
                  

                };
                
            }

        }

        //очистка всех полей и Label
        private void clear() 
        {
            FindViewById<EditText>(Resource.Id.editText).Text = "";
            FindViewById<EditText>(Resource.Id.editText1).Text = "";
            FindViewById<EditText>(Resource.Id.editText2).Text = "";
            FindViewById<EditText>(Resource.Id.editText3).Text = "";
            FindViewById<EditText>(Resource.Id.editText4).Text = "";
            FindViewById<EditText>(Resource.Id.editText5).Text = "";
            spinner.Adapter=null;
            txt.Text = "0:0:0";
        }

        //вызов второго activity и передача ему параметров
        private void second_activity() 
        {
            Intent intent = new Intent(this, typeof(Second_activity));
            intent.PutExtra("hours",time.hours);
            intent.PutExtra("minutes", time.minutes);
            intent.PutExtra("seconds", time.seconds);
            StartActivity(intent);
        }
        //всплывающая подсказка для спиннера
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            Toast.MakeText(this, "Вы выбрали операцию : " + spinner.GetItemAtPosition(e.Position), ToastLength.Short).Show();
        }











        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
       
    }
}