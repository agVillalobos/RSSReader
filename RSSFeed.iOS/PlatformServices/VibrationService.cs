using System;
using AudioToolbox;
using RSSFeed.Interfaces;
using RSSFeed.iOS.PlatformServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(VibrationService))]
namespace RSSFeed.iOS.PlatformServices
{
    public class VibrationService : IVibrationService
    {
        public void Vibrate() => SystemSound.Vibrate.PlayAlertSound();
    }
}
