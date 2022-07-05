using System;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Foundation;
using RSSFeed.iOS.Effects;

[assembly:ResolutionGroupName("RSSFeed")]
[assembly: ExportEffect(typeof(UnderlineLabelEffect), nameof(UnderlineLabelEffect))]
namespace RSSFeed.iOS.Effects
{
    public class UnderlineLabelEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is UILabel)
            {
                var label = (UILabel)Control;
                var element = (Label)Element;
                var newString = new NSMutableAttributedString(element.Text ?? "");
                var attributes = new UIStringAttributes();
                attributes.UnderlineStyle = NSUnderlineStyle.Single;
                newString.AddAttributes(attributes, new NSRange(0, newString.Length));
                label.AttributedText = newString;
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
