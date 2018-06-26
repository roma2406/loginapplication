using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using LogInApp.Models;


namespace LogInApp.Droid
{
    public class UserListAdapterActivity : BaseAdapter<User>
    {
        List<User> items;
        Activity context;
        public UserListAdapterActivity(Activity context, List<User> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override User this[int position] => items[position];

        public override int Count => items.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.activity_userlistitem, null);
            view.FindViewById<ImageView>(Resource.Id.avatar).SetImageResource(Resource.Mipmap.drawable_usericon);
            view.FindViewById<TextView>(Resource.Id.name).Text = item.name;
            view.FindViewById<TextView>(Resource.Id.surname).Text = item.surname;
            return view;
        }
    }
}
