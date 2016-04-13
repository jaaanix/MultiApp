using MultiApp.Models;
using Newtonsoft.Json;
using System.Net.Http;

using Xamarin.Forms;

namespace MultiApp
{
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            InitializeComponent();
            getUser();
        }

        public async void getUser()
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync("http://jsonplaceholder.typicode.com/users/2");
            if (result != null)
            {
                userName.Text = JsonConvert.DeserializeObject<User>(result).name;
                userAcc.Text = JsonConvert.DeserializeObject<User>(result).username;
                userMail.Text = JsonConvert.DeserializeObject<User>(result).email;
            }
        }
    }
}
