using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Sensor
{
	public class Emailer : Utility
    {
		public async Task SendEmail(string subject, string body, List<string> recipients)
		{
			try
			{
				var msg = new EmailMessage
				{
					Subject = subject,
					Body = body,
					To = recipients
				};
				await Email.ComposeAsync(msg);
			}
			catch (FeatureNotSupportedException e)
			{
				await ShowAlert("Email not supported", "This device doesn't support email...");
				LogError("Email not supported", e);
			}
			catch (Exception e)
			{
				LogError("Unknown", e);
			}
		}
	}
}

