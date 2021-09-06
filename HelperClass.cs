
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DigiPresence
{
    public class HelperClass
    {
        public static bool IsNetWorkConnected(Context context)
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Android.App.Activity.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
            return isOnline;
        }
        public static void ShowOkAlert(Android.App.Activity activity, string message)
        {
            try
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(activity);
                alert.SetMessage(message);
                alert.SetPositiveButton("ok", (senderAlert, args) =>
                {


                });
                Android.App.Dialog dialog = alert.Create();
                dialog.Show();
            }
            catch (Exception ex)
            {
            }
        }
        public static string ConvertDate(string datetime, string from, string to)
        {
            try
            {
                DateTime dateTime = DateTime.ParseExact(datetime, from, CultureInfo.InvariantCulture);
                return dateTime.ToString(to);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("DateHelperConvertDate: " + e.Message);
                return "";
            }
        }
    }
}
