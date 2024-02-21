using System;
using Xamarin.Essentials;

namespace Sensor
{
    public class CompassSensor
    {
        // Set speed delay for monitoring changes.
        readonly SensorSpeed speed = SensorSpeed.UI;

        private bool _running = false;

        public delegate void CompassReadingHandler(object sender, CompassChangedEventArgs e);

        private CompassReadingHandler _onReadingChanged;

        public CompassSensor(CompassReadingHandler onReadingChanged)
        {
            // Register for reading changes, be sure to unsubscribe when finished
            Compass.ReadingChanged += Compass_ReadingChanged;
            _onReadingChanged = onReadingChanged;
        }

        void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            _onReadingChanged.Invoke(sender, e);
        }

        public void ToggleCompass()
        {
            _running = !_running;
            SetCompass(_running);
        }

        public void SetCompass(bool running = false)
        {
            try
            {
            if(running && !Compass.IsMonitoring)
            {
                Compass.Start(speed);
                _running = true;
            } else if (!running && Compass.IsMonitoring)
            {
                Compass.Stop();
                _running = false;
            }
            } catch (FeatureNotSupportedException e)
            {

            } catch (FeatureNotEnabledException e)
            {

            } catch (Exception e)
            {
                
            }
        }
    }
}

