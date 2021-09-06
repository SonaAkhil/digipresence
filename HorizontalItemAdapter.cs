using System;
using System.Collections.Generic;
using System.Net;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Refractored.Controls;
using Square.Picasso;

namespace DigiPresence
{
    class HorizontalItemAdapter : RecyclerView.Adapter
    {
        private Context ctx;
        private static int TYPE_HEADER = 0;
        private static int TYPE_ITEM = 2;
        ViewGroup parent;
        private List<PostDetails> users;
        List<HOMEDATA> hOMEDATAs;
        VideoView videoView1;
        public HorizontalItemAdapter(Context ctx, List<PostDetails> users, List<HOMEDATA> hOMEDATAs)
        {
            this.ctx = ctx;
            this.users = users;
            this.hOMEDATAs = hOMEDATAs;
        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            if (holder.GetType() == typeof(HeaderViewHolder))
            {
                HeaderViewHolder headerHolder = holder as HeaderViewHolder;
                LayoutInflater inflater = LayoutInflater.From(ctx);
                if (headerHolder.horizontalview.ChildCount == 0)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        View itemViews = inflater.Inflate(Resource.Layout.horizontaladapterLayout, parent, false);
                        var imageBitmap = GetImageBitmapFromUrl(users[i].icon);
                        ImageView roundedImageView = (ImageView)itemViews.FindViewById(Resource.Id.roundedImageView);
                        CardView cardview = (CardView)itemViews.FindViewById(Resource.Id.cardview);
                        TextView itemName = (TextView)itemViews.FindViewById(Resource.Id.itemName);
                        roundedImageView.SetImageBitmap(imageBitmap);
                        itemName.Text = users[i].title;
                        int[] colors = { Color.ParseColor(users[i].color1), Color.ParseColor(users[i].color1 + "00") };

                        GradientDrawable gd = new GradientDrawable(GradientDrawable.Orientation.TlBr, colors);
                        gd.SetCornerRadius(20f);
                        cardview.SetBackgroundDrawable(gd);

                        headerHolder.horizontalview.AddView(itemViews);
                    }
                }
            }
            else if (holder.GetType() == typeof(RecyclerViewHolder))
            {
                try
                {
                    RecyclerViewHolder viewHolder = holder as RecyclerViewHolder;
                    position = position - 1;
                    viewHolder.itemName.Text = hOMEDATAs[position].name;
                    viewHolder.itemdate.Text = hOMEDATAs[position].postedDate;
                    viewHolder.itemcity.Text = hOMEDATAs[position].city;
                    viewHolder.itemcityname.Text = hOMEDATAs[position].city;
                    viewHolder.itemdesc.Text = hOMEDATAs[position].about;
                    if (hOMEDATAs[position].userPicture!= null)
                    {
                        Picasso.Get()
                              .Load(hOMEDATAs[position].userPicture)
                              
                              .Into(viewHolder.profileImage);

                    }
                   
                    if (string.IsNullOrEmpty(hOMEDATAs[position].picposted))
                    {
                        viewHolder.imageView.Visibility = ViewStates.Gone;
                        var uri = Android.Net.Uri.Parse(hOMEDATAs[position].videoposted);
                        viewHolder.videoView1.SetVideoURI(uri);
                        viewHolder.videoView1.Start();
                        viewHolder.videoView1.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        viewHolder.videoView1.Visibility = ViewStates.Gone;
                        viewHolder.imageView.Visibility = ViewStates.Visible;

                        Picasso.Get()
                           .Load(hOMEDATAs[position].picposted)

                           .Into(viewHolder.imageView);
                    }
                }
                catch (Exception) { }
            }

        }
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }


        // What layout to be inflated?
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            if (viewType == TYPE_ITEM)
            {
                // Data
                LayoutInflater inflater = LayoutInflater.From(parent.Context);
                View itemView = inflater.Inflate(Resource.Layout.recyclerViewAdapterLayout, parent, false);
                return new RecyclerViewHolder(itemView, ctx);
            }
            else if (viewType == TYPE_HEADER)
            {
                //Inflating header view
                LayoutInflater inflater = LayoutInflater.From(parent.Context);
                View itemView = inflater.Inflate(Resource.Layout.content_main, parent, false);
                this.parent = parent;

                return new HeaderViewHolder(itemView, ctx);
            }
            else return null;
        }

        // Get Number Of Items
        public override int ItemCount
        {
            get
            {
                return hOMEDATAs.Count+1; // +1 due to the header
            }
        }

        // Custom Override to decided whether its the header or the data
        public override int GetItemViewType(int position)
        {
            if (position == 0)
            {
                return TYPE_HEADER;
            }
            else if (position == hOMEDATAs.Count + 1) //+1 wegen des Headers
            {
                return TYPE_ITEM; ;
            }
            return TYPE_ITEM;
        }

    }

    // class for header
    public class HeaderViewHolder : RecyclerView.ViewHolder
    {
        public LinearLayout horizontalview;

        public HeaderViewHolder(View itemView, Context ctx) : base(itemView)
        {
            horizontalview = itemView.FindViewById<LinearLayout>(Resource.Id.horizontalview);
        }
    }

    // xml
    class RecyclerViewHolder : RecyclerView.ViewHolder
    {
        public ImageView imageView { get; set; }
        public CircleImageView profileImage { get; set; }
        public TextView itemName { get; set; }
        public TextView itemdate { get; set; }
        public TextView itemcity { get; set; }
        public TextView itemcityname { get; set; }
        public TextView itemdesc { get; set; }
        private Context ctx;
        public VideoView videoView1 { get; set; }

        public RecyclerViewHolder(View itemView, Context ctx) : base(itemView)
        {
            this.ctx = ctx;
            imageView = (ImageView)itemView.FindViewById(Resource.Id.imageView);
            profileImage = (CircleImageView)itemView.FindViewById(Resource.Id.profileImage);
            itemName = itemView.FindViewById<TextView>(Resource.Id.itemName);
            itemdate = itemView.FindViewById<TextView>(Resource.Id.itemdate);
            itemcity = itemView.FindViewById<TextView>(Resource.Id.itemcity);
            itemcityname = itemView.FindViewById<TextView>(Resource.Id.itemcityname);
            itemdesc = itemView.FindViewById<TextView>(Resource.Id.itemdesc);
            videoView1 = (VideoView)itemView.FindViewById(Resource.Id.videoView1);


        }
    }
   

    public class DataForProfile
    {
        public int imageId { get; set; }
        public Bitmap img { get; set; }
        public string description { get; set; }

    }
}
