using System;
using Android.App;
using Android.OS;
using Android.Widget;
using LogInApp.Models;
using LogInApp.ViewModels;

namespace LogInApp.Droid
{
    class EditUserActivity : Activity
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
        private User usertochange;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_addnewuser);

            string extras = Intent.GetStringExtra("user");

            usertochange = ViewModel.GetUserById(extras);

            _newUserName = FindViewById<EditText>(Resource.Id.newusername);
            _newUserSurname = FindViewById<EditText>(Resource.Id.newusersurname);
            _newUserRole = FindViewById<EditText>(Resource.Id.newuserrole);
            _newUserLogin = FindViewById<EditText>(Resource.Id.newuserlogin);
            _newUserPassword = FindViewById<EditText>(Resource.Id.newuserpassword);

            var button = FindViewById<Button>(Resource.Id.adduser);
            button.Text = "Edit User";
            var buttonDelete = FindViewById<Button>(Resource.Id.deleteuser);
            buttonDelete.Text = "Delete User";

            _newUserName.Text = usertochange.name;
            _newUserSurname.Text = usertochange.surname;
            _newUserRole.Text = ViewModel.GetRoleName(usertochange.RoleId);
            _newUserLogin.Text = usertochange.login;
            _newUserPassword.Text = usertochange.password;

            buttonDelete.Click += ButtonDelete_ClickAsync;
            button.Click += Button_ClickAsync;

        }

        private void Button_ClickAsync(object sender, EventArgs e)
        {
            //var correct = null;

            usertochange.name = _newUserName.Text;
            usertochange.surname = _newUserSurname.Text;
            usertochange.RoleId = ViewModel.GetRoleGuid(_newUserRole.Text);
            usertochange.login = _newUserLogin.Text;
            usertochange.password = _newUserPassword.Text;


            if (usertochange.RoleId != Guid.Empty)
            {
                Android.Widget.Toast.MakeText(this, "User edited successfully", ToastLength.Short).Show();
                ViewModel.ChangeUser(usertochange);
                Finish();
            }
            else
            {
                Android.Widget.Toast.MakeText(this, "There isn't entered role in database", ToastLength.Short).Show();
            }

        }

        private void ButtonDelete_ClickAsync(object sender, EventArgs e)
        {
            ViewModel.DeleteUser(usertochange);
            Android.Widget.Toast.MakeText(this, "User deleted successfully", ToastLength.Short).Show();
            Finish();
        }
    }
}