using MultiApp.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;

using Xamarin.Forms;
using System;

namespace MultiApp
{
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            InitializeComponent();
            getUser();
            getTasks();
        }

        private async void getTasks()
        {

            Task tasks = new Task();
            
            var client = new HttpClient();
            var result = await client.GetStringAsync("http://jsonplaceholder.typicode.com/todos/1");
            if (result != null)
            {
                // Füllt Observeable Collection mit Items (ListView-Einträgen)
                tasks.Items.Add(JsonConvert.DeserializeObject<Task>(result).Title);
                tasks.ChildItems.Add(JsonConvert.DeserializeObject<Task>(result).Completed);
                tasksListViewDataTemplate.SetBinding(TextCell.TextProperty, JsonConvert.DeserializeObject<Task>(result).Title);

                tasksListViewDataTemplate.SetValue(TextCell.TextColorProperty, Color.Red);
                //tasksListViewDataTemplate.SetBinding(TextCell.TextProperty, tasks.Title);
                //tasksListViewDataTemplate.SetBinding(TextCell.DetailProperty, tasks.Completed);
                //tasksListViewDataTemplate.SetValue(TextCell.TextProperty, "rofls");

                // Setzt BindingContext für die ListView
                tasksListView.BindingContext = tasks;
            }

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

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event
            Debug.WriteLine("Tapped: " + e.Item);
            ((ListView)sender).SelectedItem = null; // de-select the row
        }
    }
}
