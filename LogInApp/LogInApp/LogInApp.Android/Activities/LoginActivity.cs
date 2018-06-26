using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using LogInApp.ViewModels;

namespace LogInApp.Droid
{
    [Activity(Label = "LogInApp", MainLauncher = true, Icon = "@mipmap/icon")]
    public class LogIn : Activity
    {
        //public AppServices AppServices => AppServices.Instance;
        public MainViewModel ViewModel
        {
            get { return MainViewModel.Instance; }
        }

        private EditText _loginEditText;
        private EditText _passwordEditText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login);

            _loginEditText = FindViewById<EditText>(Resource.Id.log_in_name);
            _passwordEditText = FindViewById<EditText>(Resource.Id.log_in_password);

            // Get our button from the layout resource,
            // and attach an event to it
            var button = FindViewById<Button>(Resource.Id.log_in);

            //AppServices.SQLite = new SQLite();
            button.Click += Button_ClickAsync;
        }

        protected override void OnResume()
        {
            base.OnResume();

            _passwordEditText.TextChanged += _password_TextChanged;
            _loginEditText.TextChanged += _logIn_TextChanged;
        }
        protected override void OnPause()
        {
            base.OnPause();

            _passwordEditText.TextChanged += _password_TextChanged;
            _loginEditText.TextChanged -= _logIn_TextChanged;
        }

        private void _logIn_TextChanged(object sender, global::Android.Text.TextChangedEventArgs e)
        {

        }

        private void _password_TextChanged(object sender, global::Android.Text.TextChangedEventArgs e)
        {

        }

        private void Button_ClickAsync(object sender, EventArgs e)
        {
            var login = _loginEditText.Text;
            var password = _passwordEditText.Text;

            var choose = ViewModel.CheckUser(login, password);
            if (choose == Guid.Empty)
            {
                Android.Widget.Toast.MakeText(this, "Login or password incorrect", ToastLength.Short).Show();
                _loginEditText.Text = "";
                _passwordEditText.Text = "";
                return;
            }
            var nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
    }
}

