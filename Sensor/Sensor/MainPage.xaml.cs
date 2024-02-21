using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sensor
{
    public partial class MainPage : ContentPage
    {
        private readonly CompassSensor _sensor;

        public MainPage()
        {
            InitializeComponent();
            _sensor = new CompassSensor(On_Compass_ReadingChanged);
        }

        void Toggle_Compass(System.Object sender, System.EventArgs e)
        {
            _sensor.ToggleCompass();
        }


        void On_Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            var data = e.Reading;
            CompassData.Text = $"{data.HeadingMagneticNorth} degrees";
        }

    }
}

