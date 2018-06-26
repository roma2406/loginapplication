using System;
using System.Collections.Generic;
using System.Text;
using Android;
using Android.Content;
using Android.App;
using Android.Views;
using Android.Widget;
using LogInApp.Models;
using LogInApp.ViewModels;


namespace LogInApp.Droid.Activities
{
    public class UserProfileAdapterActivity : BaseAdapter<User>
    {
        public MainViewModel ViewModel
        {
            get { return MainViewModel.Instance; }
        }
        User items;
        Activity context;
        public UserProfileAdapterActivity(Activity context, User items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override User this[int position] => items;

        public override int Count => 1;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //var item = items[position];
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.activity_profileitem, null);
            view.FindViewById<ImageView>(Resource.Id.avatar).SetImageResource(Resource.Mipmap.drawable_usericon);
            view.FindViewById<TextView>(Resource.Id.login).Text = items.login;
            view.FindViewById<TextView>(Resource.Id.namesurname).Text = items.name + " " + items.surname;
            view.FindViewById<TextView>(Resource.Id.userrole).Text = ViewModel.GetRoleName(items.RoleId);

            return view;
        }
    }
}