using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Google;
//using Google.Apis.Auth.OAuth2;
//using Google.Apis.People.v1;
//using Google.Apis.People.v1.Data;
//using Google.Apis.Services;
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


        WebView webView = new WebView
        {
            Source = new UrlWebViewSource
            {
                Url = "https://plus.google.com/u/0/people?pli=1",
            },
            VerticalOptions = LayoutOptions.FillAndExpand
        };        
    }
}



#region code from Google for People API
/*static void Main(string[] args)
{
    // Create OAuth credential.
    //UserCredential credential1 = ServiceCredential.

    UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        new ClientSecrets
        {
            ClientId = "",
            ClientSecret = ""
        },
        new[] { "profile", "https://www.googleapis.com/auth/contacts.readonly" }, "me", CancellationToken.None).Result;

    // Create the service.
    var peopleService = new PeopleService(new BaseClientService.Initializer()
    {
        HttpClientInitializer = credential,
        ApplicationName = "MyProject",
    });

    PeopleResource.ConnectionsResource.ListRequest peopleRequest = peopleService.People.Connections.List("people/me");
    //peopleRequest.PersonFields = "names,emailAddresses";
    peopleRequest.Fields = "names,emailAddress";
    ListConnectionsResponse connectionsResponse = peopleRequest.Execute();
    IList<Person> connections = connectionsResponse.Connections;
}*/
#endregion