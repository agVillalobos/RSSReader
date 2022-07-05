using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RSSFeed.Views
{
    public partial class WebPage : ContentPage
    {
        public string WebviewSource { get; set; }

        public WebPage(string webViewSource)
        {
            InitializeComponent();
            WebviewSource = webViewSource;
            Title = "Web";
            BindingContext = this;
        }
    }
}
