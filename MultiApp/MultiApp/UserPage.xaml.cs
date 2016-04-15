using MultiApp.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;

using Xamarin.Forms;
using System;
using System.Collections.Generic;

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
            List<Task> liste = new List<Task>();
            
            var client = new HttpClient();
            int todoId = 1;
            while (todoId <= 10) // 10 ToDos fetchen
            {
                var result = await client.GetStringAsync("http://jsonplaceholder.typicode.com/todos/" + todoId.ToString());
                todoId++;
                if (result != null)
                {
                    // Füllt Liste mit Items (ListView-Einträgen)
                    liste.Add(JsonConvert.DeserializeObject<Task>(result));
                    
                }
            }
            // Die ListView an die Liste Datenliste mit TODOs binden und dem DataTemplate
            tasksListView.ItemTemplate = dataTemplate;
            tasksListView.ItemsSource = liste;
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
                userPhone.Text = JsonConvert.DeserializeObject<User>(result).phone;
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
