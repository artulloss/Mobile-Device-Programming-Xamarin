using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Net.Mail;

namespace Sensor
{
    public partial class MainPage : ContentPage
    {
        private readonly CompassSensor _sensor;
        private readonly Emailer _emailer;
        private CompassData _compass_data;

        public MainPage()
        {
            InitializeComponent();
            _sensor = new CompassSensor(On_Compass_ReadingChanged);
            _emailer = new Emailer();
        }

        public void Start_Compass()
        {
            _sensor.SetCompass(true);
        }

        public void Stop_Compass()
        {
            _sensor.SetCompass(false);
        }


        void On_Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            var data = e.Reading;
            _compass_data = data;
            CompassData.Text = $"{data.HeadingMagneticNorth} degrees";
        }

        async void Email_Data(System.Object sender, System.EventArgs e)
        {

            if(Email.Text is null)
            {
                await DisplayAlert("Invalid Email", "Please provide a valid email address", "OK");
                return;
            }

            string[] emails = Email.Text.Split(' ');


            if(!ValidateEmails(emails))
            {
                await DisplayAlert("Invalid Email", "Please provide a valid email address", "OK");
                return;
            }

            string subject = "Compass Data";
            string body = $"" +
                $"The compass data reading is " +
                $"{_compass_data} degrees";

            await _emailer.SendEmail(subject, body, emails.ToList());
        }

        private bool ValidateEmails(string[] emails)
        {
            try
            {
                foreach (string email in emails)
                {
                    new MailAddress(Email.Text);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}

