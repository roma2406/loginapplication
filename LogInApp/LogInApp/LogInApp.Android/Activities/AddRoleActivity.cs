using System;
using Android.App;
using Android.OS;
using Android.Widget;
using LogInApp.Models;
using LogInApp.ViewModels;

namespace LogInApp.Droid.Activities
{
    [Activity(Label = "AddRoleActivity")]
    class AddRoleActivity : Activity
    {
        private EditText _newRoleName;
        public MainViewModel ViewModel
        {
            get { return MainViewModel.Instance; }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_addnewrole);
            var button = FindViewById<Button>(Resource.Id.addrole);

            button.Click += Button_ClickAsync;
        }

        private void Button_ClickAsync(object sender, EventArgs e)
        {
            //var correct = null;
            _newRoleName = FindViewById<EditText>(Resource.Id.newrolename);


            var NewRole = new Role
            {
                id = Guid.NewGuid(),
                name = _newRoleName.Text,

            };

            if (ViewModel.CheckRole())
            {
                Android.Widget.Toast.MakeText(this, "Role added successfully", ToastLength.Short).Show();
                //AppDatabase.Instance.Login.database.Insert(NewRole);
                ViewModel.AddNewrole(NewRole);
                Finish();
            }
            else
            {
                Android.Widget.Toast.MakeText(this, "There is entered role in database", ToastLength.Short).Show();
            }

        }
    }
}