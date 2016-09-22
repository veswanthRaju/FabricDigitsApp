using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Com.Digits.Sdk.Android;
using System;
using Com.Twitter.Sdk.Android.Core;
using IO.Fabric.Sdk.Android;

namespace DigitsApp
{
    [Activity(Label = "DigitsApp", MainLauncher = true, Theme = "@style/MyTheme", Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity, IAuthCallback
    {
        private const string TWITTER_KEY = "AJFiFHfvcGdPVD1FQsEvYPggH";
        private const string TWITTER_SECRET = "bYxCuSQnfou2Aa7xGagFnXHeGHe8yCXp6MYTZWHGrY0MwG70T9";
        private Button logoutbtn;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            TwitterAuthConfig authConfig = new TwitterAuthConfig(TWITTER_KEY, TWITTER_SECRET);
            Fabric.With(this, new TwitterCore(authConfig), new Digits());
            
            SetContentView(Resource.Layout.Main);
            
            var AuthButton = FindViewById<DigitsAuthButton>(Resource.Id.auth_button);
            logoutbtn = FindViewById<Button>(Resource.Id.logout_button);
            AuthButton.SetCallback(this);

            logoutbtn.Enabled = false;
            logoutbtn.Click += Logoutbtn_Click;
        }

        private void Logoutbtn_Click(object sender, EventArgs e)
        {
            Digits.SessionManager.ClearActiveSession();
            Toast.MakeText(this, "Hey! You are LoggedOut!!", ToastLength.Short).Show();
        }

        public void Success(DigitsSession session, string phoneNumber)
        {   
            Toast.MakeText(this, "Hey! It's Done!!"+ phoneNumber, ToastLength.Short).Show();
            logoutbtn.Enabled = true;
        }

        public void Failure(DigitsException error)
        {
            Toast.MakeText(this, "Oops! Something went Wrong!!", ToastLength.Short).Show();
        }
    }
}

