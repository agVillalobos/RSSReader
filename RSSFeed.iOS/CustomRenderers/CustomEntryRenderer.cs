using System;
using RSSFeed.CustomRenderers;
using RSSFeed.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace RSSFeed.iOS.CustomRenderers
{
    public class CustomEntryRenderer: EntryRenderer
    {
        public CustomEntryRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);  
  
            if (e.NewElement != null)  
            {
                var view = (CustomEntry)Element;
                
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);  
            }
        }
    }
}
