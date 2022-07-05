using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using RSSFeed.CustomRenderers;
using RSSFeed.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace RSSFeed.Droid.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var view = (CustomEntry)Element;
                if (view.IsCurvedCornersEnabled)
                {
                    var gradientDrawable = new GradientDrawable();
                    gradientDrawable.SetShape(ShapeType.Rectangle);
                    gradientDrawable.SetColor(view.BackgroundColor.ToAndroid());
                    gradientDrawable.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());
                    gradientDrawable.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));

                    Control.SetBackground(gradientDrawable);
                }
                Control.SetPadding((int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop, (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);
            }
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            var metrics = context.Resources.DisplayMetrics;

            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}
