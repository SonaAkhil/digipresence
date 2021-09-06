using Android.App;
using Android.OS;
using Android.Widget;
using Refit;
using System.Collections.Generic;
using Android.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System;
using Android.Support.V7.Widget;
using Firebase.Database;
using System.Threading.Tasks;
using Firebase.Firestore;
using Firebase;
using Android.Gms.Tasks;

namespace DigiPresence
{
    [Activity(Label = "DigiPresence", MainLauncher = true, Icon = "@mipmap/ic_launcher_round")]
    public class MainActivity : Activity, IOnSuccessListener
    {
        IDigiPresenceGetApi gitHubApi;

        Button button1;

        IListAdapter ListAdapter;

        RecyclerView listView;

        FirebaseFirestore firestore;

        List<HOMEDATA> hOMEDATAs = new List<HOMEDATA>();
            
        protected override void OnCreate(Bundle bundle)
        {


            try
            {
                base.OnCreate(bundle);

                SetContentView(Resource.Layout.activity_main);
                listView = (RecyclerView)FindViewById(Resource.Id.recyclerview);
                //firestore = GetDataBase();
                var options = new Firebase.FirebaseOptions.Builder()
              .SetProjectId("digipresence-aa2a1")
              .SetApplicationId("digipresence-aa2a1")
              .SetApiKey("AIzaSyCTeOepROm5-DtMo9ix92fLWVPr1OD_UqU")
              .SetDatabaseUrl("https://digipresence-aa2a1-default-rtdb.firebaseio.com")
              .SetStorageBucket("digipresence-aa2a1.appspot.com")
              .Build();
                var app = FirebaseApp.InitializeApp(this, options);
                firestore = FirebaseFirestore.GetInstance(app);
                firestore.Collection("HOMEDATA").Get()
                    .AddOnSuccessListener(this);
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = { new StringEnumConverter() }
                };

                gitHubApi = RestService.For<IDigiPresenceGetApi>("https://api.npoint.io");
                if (HelperClass.IsNetWorkConnected(this))
                {
                    getUsers();

                }
                else
                {
                    HelperClass.ShowOkAlert(this, "Internet May Not be Available, Please try again later");
                }

            }
            catch (System.Exception ex)
            {
                Log.Error("Ozioma See", ex.Message);
            }
        }



        public FirebaseFirestore GetDataBase()
        {
            FirebaseFirestore firebase;

            var options = new Firebase.FirebaseOptions.Builder()
                .SetProjectId("digipresence-aa2a1")
                .SetApplicationId("digipresence-aa2a1")
                .SetApiKey("AIzaSyCTeOepROm5-DtMo9ix92fLWVPr1OD_UqU")
                .SetDatabaseUrl("https://digipresence-aa2a1-default-rtdb.firebaseio.com")
                .SetStorageBucket("digipresence-aa2a1.appspot.com")
                .Build();
            var app = FirebaseApp.InitializeApp(this, options);
            firebase = FirebaseFirestore.GetInstance(app);
            return firebase;
        }

        private async void getUsers()
        {
            try
            {
                List<PostDetails> PostDetails = await gitHubApi.GetUser();
                System.Diagnostics.Debug.WriteLine("Response is" + PostDetails);

                if (PostDetails.Count != 0)
                {
                    var mLayoutManager = new LinearLayoutManager(this);
                    listView.SetLayoutManager(mLayoutManager);
                    HorizontalItemAdapter mAdapter = new HorizontalItemAdapter(this, PostDetails, hOMEDATAs);
                    listView.SetAdapter(mAdapter);
                }

            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, ex.StackTrace, ToastLength.Long).Show();

            }

        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var snapshot =(QuerySnapshot) result;

            if (!snapshot.IsEmpty)
            {
                var document = snapshot.Documents;
                hOMEDATAs.Clear();

                foreach (DocumentSnapshot item in document)
                {
                    HOMEDATA hOMEDATA = new HOMEDATA();

                    hOMEDATA.userPicture = item.Get("userPicture").ToString();
                    hOMEDATA.index = (int)item.Get("index");
                    hOMEDATA._id = item.Get("_id").ToString();
                    hOMEDATA.about = item.Get("about").ToString();
                    hOMEDATA.city = item.Get("city").ToString();
                    hOMEDATA.guid = item.Get("guid").ToString();
                    hOMEDATA.isLiked = (bool)item.Get("isLiked");
                    hOMEDATA.name = item.Get("name").ToString();
                    hOMEDATA.postedDate = item.Get("postedDate").ToString();
                    hOMEDATA.price = item.Get("price").ToString();
                    hOMEDATA.picposted = item.Get("picposted").ToString();

                    hOMEDATA.videoposted = item.Get("videoposted").ToString();

                    //var fff = item.ToObject(MediaModel.);
                    var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("MediaItem");
                    var postDoc = new Dictionary<string, Java.Lang.Object>();
                    hOMEDATAs.Add(hOMEDATA);
                }
            }
        }
    }
}
