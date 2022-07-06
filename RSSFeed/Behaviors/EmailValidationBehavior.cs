using System;
using System.Text.RegularExpressions;
using RSSFeed.CustomRenderers;
using Xamarin.Forms;

namespace RSSFeed.Behaviors
{
    public class EmailValidationBehavior: Behavior<CustomEntry>
    {
        protected override void OnAttachedTo(CustomEntry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(CustomEntry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            string email = args.NewTextValue;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            ((CustomEntry)sender).BorderColor = match.Success ? Color.Black : Color.Red;

        }
    }
}
