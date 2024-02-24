using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sensor
{
	abstract public class Utility
	{
        protected async Task ShowAlert(string title, string msg, string cancel = "OK")
        {
            if (Device.IsInvokeRequired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert(title, msg, cancel);
                });
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(title, msg, cancel);
            }
        }

        protected void Log(string msg)
        {
            Debug.WriteLine($"INFO: {msg}");
        }

        protected void LogError(string msg, Exception e = null)
        {
            if (e == null)
            {
                Debug.WriteLine($"ERROR: {msg}");
            }
            else
            {
                Debug.WriteLine($"ERROR: {msg}, Exception: {e.Message}");
            }
        }
    }
}

