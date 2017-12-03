using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GiftTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowsePage : ContentPage
    {
        public BrowsePage()
        {
            InitializeComponent();
        }


        WebView webView = new WebView
        {
            Source = new UrlWebViewSource
            {
                Url = "https://www.amazon.com/",
            },
            VerticalOptions = LayoutOptions.FillAndExpand
        };
    }
}