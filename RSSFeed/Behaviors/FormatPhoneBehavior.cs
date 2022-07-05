using System;
using Xamarin.Forms;

namespace RSSFeed.Behaviors
{
    public class FormatPhoneBehavior: Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue)) return;

            var textValue = e.NewTextValue;
            var entry = (Entry)sender;
            var length = textValue.Length;

            if (length == 1 || length == 5 || length == 9)
            {
                entry.Text += "-";
                return;
            }

            entry.Text = textValue;
        }
    }
}
