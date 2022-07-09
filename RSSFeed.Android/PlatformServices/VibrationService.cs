using System;
using Android.Content;
using Android.OS;
using RSSFeed.Droid.PlatformFeatures;
using RSSFeed.Interfaces;
using Xamarin.Forms;

[assembly:Dependency(typeof(VibrationService))]
namespace RSSFeed.Droid.PlatformFeatures
{
    public class VibrationService : IVibrationService
    {
        public Context Context { get; private set; }

        public VibrationService(Context context)
        {
            Context = context;
        }

        public void Vibrate()
        {
            try
            {
                var vibrator = (Vibrator)Context.GetSystemService(Context.VibratorService);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    var vibrteEffect = VibrationEffect.CreateOneShot(500, VibrationEffect.DefaultAmplitude);
                    vibrator.Vibrate(vibrteEffect);
                }
                else
                {
                    vibrator.Vibrate(500);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Vibration Service failed: {ex.Message}");
            }
        }
    }
}