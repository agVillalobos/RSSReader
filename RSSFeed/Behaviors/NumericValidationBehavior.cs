using System;
using RSSFeed.CustomRenderers;
using Xamarin.Forms;

namespace RSSFeed.Behaviors
{
	public class NumericValidationBehavior : Behavior<CustomEntry>
	{
		protected override void OnAttachedTo(CustomEntry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			base.OnAttachedTo(entry);
		}

		protected override void OnDetachingFrom(CustomEntry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
			base.OnDetachingFrom(entry);
		}

		void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			double result;
			bool isValid = double.TryParse(args.NewTextValue, out result);
			((CustomEntry)sender).BorderColor = isValid ? Color.Black : Color.Red;
		}
	}
}
