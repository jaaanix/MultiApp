using MultiApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

using Xamarin.Forms;
using System.Collections.Generic;

namespace MultiApp
{
    public partial class UserPage : ContentPage
    {
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                if (isRefreshing == value)
                    return;
                isRefreshing = value;
                OnPropertyChanged("IsRefreshing");
            }
        }
        private Command refreshTasks;
        public Command RefreshTasks
        {
            get
            {
                return refreshTasks ?? (refreshTasks = new Command(getTasks, () =>
                {
                    return !IsRefreshing;
                }));
            }
        }

        public UserPage()
        {
            InitializeComponent();
            getUser();
            getTasks();
        }

        private async void getTasks()
        {
            if (IsRefreshing)
                return;
            IsRefreshing = true;
            RefreshTasks.ChangeCanExecute();
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

            IsRefreshing = false;
            RefreshTasks.ChangeCanExecute();
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

        private async void updateTaskStatus(Task task)
        {
            if (task.Completed.Equals("false"))
            {
                HttpContent content = new StringContent("true");
                var client = new HttpClient();
                await client.PutAsync("http://jsonplaceholder.typicode.com/todos/" + task.Id, content);
            }
            else
            {
                HttpContent content = new StringContent("false");
                var client = new HttpClient();
                await client.PutAsync("http://jsonplaceholder.typicode.com/todos/" + task.Id, content);
            }
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event
            Debug.WriteLine("Tapped: " + e.Item);
            Debug.WriteLine("Task Id: " + ((Task)e.Item).Id);
            updateTaskStatus((Task)e.Item);
            ((ListView)sender).SelectedItem = null; // de-select the row
        }

    }
}
