using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Com.Lilarcor.Cheeseknife;

namespace DrinkingGame
{
    [Activity(Label = "DrinkingGame", MainLauncher = true, Icon = "@drawable/icon",Theme = "@style/Theme.AppCompat")]
    public class MainActivity : Activity
    {
        [InjectView(Resource.Id.i_have_never)] private Switch _iHaveNeverSwitch;
        [InjectView(Resource.Id.all_that_have)] private Switch _allThatHaveSwitch;
        [InjectView(Resource.Id.who)] private Switch _whoSwitch;
        [InjectView(Resource.Id.start_game)] private Button _startGameButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Cheeseknife.Inject(this);
        }

        [InjectOnClick(Resource.Id.start_game)]
        private void StartGameButtonOnClick(object sender, EventArgs e)
        {
            var questions = new List<string>();

            if (_iHaveNeverSwitch.Checked)
                questions.AddRange(Resources.GetStringArray(Resource.Array.i_have_never));

            if (_whoSwitch.Checked)
                questions.AddRange(Resources.GetStringArray(Resource.Array.who));

            if (_allThatHaveSwitch.Checked)
                questions.AddRange(Resources.GetStringArray(Resource.Array.all_that_have));

            if (questions.Count == 0)
            {
                Snackbar.Make(_startGameButton, "No questions selected", Snackbar.LengthLong).Show();
                return;
            }
            
            var activity = new Intent (this, typeof(QuestionActivity));
            activity.PutExtra("questions",questions.ToArray());
            StartActivity (activity);
        }
    }
}