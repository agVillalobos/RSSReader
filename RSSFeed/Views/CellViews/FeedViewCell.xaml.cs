using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RSSFeed.Views.CellViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedViewCell : ViewCell
    {
        public FeedViewCell()
        {
            InitializeComponent();
        }
    }
}
