using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using LogInApp.Models;
using LogInApp.ViewModels;

namespace LogInApp.Droid.Activities
{
    [Activity(Label = "AddUserActivity")]
    class AddUserActivity : Activity
    {
        public MainViewModel ViewModel
        {
            get { return MainViewModel.Instance; }
        }
        private EditText _newUserName;
        private EditText _newUserSurname;
        private EditText _newUserRole;
        private EditText _newUserLogin;
        private EditText _newUserPassword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_addnewuser);
            var button = FindViewById<Button>(Resource.Id.adduser);
            var buttondelete = FindViewById<Button>(Resource.Id.deleteuser);
            buttondelete.Visibility = ViewStates.Invisible;

            button.Click += Button_ClickAsync;
        }

        private void Button_ClickAsync(object sender, EventArgs e)
        {
            //var correct = null;
            _newUserName = FindViewById<EditText>(Resource.Id.newusername);
            _newUserSurname = FindViewById<EditText>(Resource.Id.newusersurname);
            _newUserRole = FindViewById<EditText>(Resource.Id.newuserrole);
            _newUserLogin = FindViewById<EditText>(Resource.Id.newuserlogin);
            _newUserPassword = FindViewById<EditText>(Resource.Id.newuserpassword);

            var NewUser = new User
            {
                id = Guid.NewGuid(),
                name = _newUserName.Text,
                surname = _newUserSurname.Text,
                RoleId = ViewModel.GetRoleGuid(_newUserRole.Text),
                login = _newUserLogin.Text,
                password = _newUserPassword.Text
            };

            if (ViewModel.GetUserGuid(NewUser.login) == Guid.Empty && NewUser.RoleId != Guid.Empty)
            {
                Android.Widget.Toast.MakeText(this, "User added successfully", ToastLength.Short).Show();
                ViewModel.AddNewuser(NewUser);
                Finish();
            }
            else
            {
                Android.Widget.Toast.MakeText(this, "There isn't entered role in database", ToastLength.Short).Show();
            }

        }
    }
}