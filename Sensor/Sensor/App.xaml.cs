using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sensor
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart ()
        {
            if(MainPage is MainPage)
            {
                ((Sensor.MainPage)MainPage).Start_Compass();
            }
        }

        protected override void OnSleep ()
        {
            if (MainPage is MainPage)
            {
                ((Sensor.MainPage)MainPage).Stop_Compass();
            }
        }

        protected override void OnResume ()
        {
            if (MainPage is MainPage)
            {
                ((Sensor.MainPage)MainPage).Start_Compass();
            }
        }
    }
}

