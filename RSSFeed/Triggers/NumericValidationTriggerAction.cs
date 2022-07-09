using System;
using RSSFeed.CustomRenderers;
using Xamarin.Forms;

namespace RSSFeed.Triggers
{
    public class NumericValidationTriggerAction : TriggerAction<CustomEntry>
    {
        protected override void Invoke(CustomEntry entry)
        {
            bool isValid = Double.TryParse(entry.Text, out var result);
            entry.BackgroundColor = isValid ? Color.Default : Color.Red;
        }
    }
}
