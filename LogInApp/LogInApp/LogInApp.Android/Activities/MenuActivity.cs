using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Android.Content;
using Android.Views;
using LogInApp.Droid.Activities;
using LogInApp.ViewModels;
using Android;

namespace LogInApp.Droid
{
    [Activity(Label = "MenuActivity")]
    public class MenuActivity : Activity
    {
        public MainViewModel ViewModel
        {
            get { return MainViewModel.Instance; }
        }

        private ListView listView;

        private UserProfileAdapterActivity profileAdapter;

        private UserListAdapterActivity usersAdapter;

        private RoleListAdapterActivity rolesAdapter;
        Guid UserId
        {
            get
            {
                return LoggedUser.Instance.guid;
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (ViewModel.CheckRole())
                MenuInflater.Inflate(Resource.Layout.activity_toolbar, menu);

            //var AddUser = FindViewById<ImageView>(Resource.Id.addUser);
            //AddUser.SetImageResource(Resource.Mipmap.addUser);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
            //    ToastLength.Short).Show();
            var choose = int.Parse(item.TitleFormatted.ToString());
            Intent nextActivity;
            switch (choose)
            {
                case 1:
                    nextActivity = new Intent(this, typeof(AddUserActivity));
                    StartActivity(nextActivity);
                    usersAdapter.NotifyDataSetChanged();
                    break;
                case 2:
                    nextActivity = new Intent(this, typeof(AddRoleActivity));
                    StartActivity(nextActivity);
                    rolesAdapter.NotifyDataSetChanged();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_list);

            profileAdapter = new UserProfileAdapterActivity(this, ViewModel.GetUserById(UserId.ToString()));

            usersAdapter = new UserListAdapterActivity(this, ViewModel.Users);

            rolesAdapter = new RoleListAdapterActivity(this, ViewModel.Roles);

            listView = FindViewById<ListView>(Resource.Id.List);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            var tab = ActionBar.NewTab();
            tab.SetTag(1);
            tab.SetText(Config.PROFILE_STRING);
            tab.TabSelected += TabSelected;
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetTag(2);
            tab.SetText(Config.USERS_STRING);
            tab.TabSelected += TabSelected;
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetTag(3);
            tab.SetText(Config.ROLES_STRING);
            tab.TabSelected += TabSelected;
            ActionBar.AddTab(tab);
            ViewModel.Init();

            if (ViewModel.CheckRole())
            {
                if (listView != null) listView.ItemClick += OnListItemClick;
                rolesAdapter.NotifyDataSetChanged();
            }

        }

        private void TabSelected(object sender, ActionBar.TabEventArgs e)
        {
            var tab = sender as ActionBar.Tab;

            if (tab == null)
            {
                return;
            }

            var id = (int)tab.Tag;
            //ViewModel.Init();
            switch (id)
            {
                case 1:
                    listView.Adapter = profileAdapter;
                    break;
                case 2:
                    usersAdapter.NotifyDataSetChanged();
                    listView.Adapter = usersAdapter;
                    break;
                case 3:
                    rolesAdapter.NotifyDataSetChanged();
                    listView.Adapter = rolesAdapter;
                    break;
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
            rolesAdapter.NotifyDataSetChanged();
            usersAdapter.NotifyDataSetChanged();
        }

        private void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (listView.Adapter == rolesAdapter)
            {
                var role = rolesAdapter[e.Position];
                ViewModel.DeleteRole(role);
                rolesAdapter.NotifyDataSetChanged();
            }
            if (listView.Adapter == usersAdapter)
            {
                var user = usersAdapter[e.Position];
                var nextActivity = new Intent(this, typeof(EditUserActivity));
                nextActivity.PutExtra("user", user.id.ToString());
                StartActivity(nextActivity);
                usersAdapter.NotifyDataSetChanged();
            }
        }
    }
}