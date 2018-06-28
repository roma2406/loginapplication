using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using LogInApp.Models;


namespace LogInApp.Droid.Activities
{
    public class RoleListAdapterActivity : BaseAdapter<Role>
    {
        List<Role> itemes;
        Activity context;
        public RoleListAdapterActivity(Activity context, List<Role> itemes)
            : base()
        {
            this.context = context;
            this.itemes = itemes;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Role this[int position] => itemes[position];

        public override int Count => itemes.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = itemes[position];
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.activity_rolelistitem, null);
            view.FindViewById<TextView>(Resource.Id.name).Text = item.name;
            view.FindViewById<ImageView>(Resource.Id.trashcan).SetImageResource(Resource.Mipmap.drawable_trashcan);

            return view;
        }
    }
}
