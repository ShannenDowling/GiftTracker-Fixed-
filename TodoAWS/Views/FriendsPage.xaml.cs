using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.People.v1;
using Google.Apis.People.v1.Data;
using Google.Apis.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoAWSSimpleDB
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPage : ContentPage
    {
        public FriendsPage()
        {
            InitializeComponent();
        }

        

...

        static void Main(string[] args)
    {
        // Create OAuth credential.
        UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = "CLIENT_ID",
                ClientSecret = "CLIENT_SECRET"
            },
            new[] { "profile", "https://www.googleapis.com/auth/contacts.readonly" },
            "me",
            CancellationToken.None).Result;

        // Create the service.
        var service = new PeopleService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "APP_NAME",
        });
    }
}