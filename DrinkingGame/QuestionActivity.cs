using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using DrinkingGame.Extensions;

namespace DrinkingGame
{
    [Activity(Label = "DrinkingGame", Icon = "@drawable/icon",Theme = "@style/Theme.AppCompat")]
    public class QuestionActivity : Activity
    {
        
        [InjectView(Resource.Id.forwardBtn)] private Button _forward;
        [InjectView(Resource.Id.backBtn)] private Button _back;
        [InjectView(Resource.Id.questionText)] private TextView _questionText;

        private string[] _questions;
        private int _index;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Question);
            Cheeseknife.Inject(this);

            _questions = Intent.Extras.GetStringArray("questions").ToArray();

            //scramble the questions array
            new Random().Shuffle(_questions);
            
            _questionText.Text = _questions[_index++];
        }
        
        [InjectOnClick(Resource.Id.backBtn)]
        private void BackButtonOnClick(object sender, EventArgs e)
        {
            if (_index == 0)
            {
                Snackbar.Make(_back, "No more questions",Snackbar.LengthLong).Show();
                return;
            }
            _questionText.Text = _questions[_index--];
        }
        
        [InjectOnClick(Resource.Id.forwardBtn)]
        private void ForwardButtonOnClick(object sender, EventArgs e)
        {
            if (_index == _questions.Length -1)
            {
                _questionText.Text = "HOPE YOU ARE FUCKING DRUNK NOW BBITCH!";
                Snackbar.Make(_forward, "No more questions",Snackbar.LengthLong).Show();
                return;
            }
            _questionText.Text = _questions[_index++];
        }
    }
}