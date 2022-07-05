using System;
using Android.Widget;
using RSSFeed.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ResolutionGroupName("RSSFeed")]
[assembly:ExportEffect(typeof(UnderlineLabelEffect), nameof(UnderlineLabelEffect))]
namespace RSSFeed.Droid.Effects
{
    public class UnderlineLabelEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is TextView)
            {
                var textView = (TextView)Control;
                var element = (Label)Element;
                textView.PaintFlags = textView.PaintFlags | Android.Graphics.PaintFlags.UnderlineText;
                textView.Text = element.Text ?? "";
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
