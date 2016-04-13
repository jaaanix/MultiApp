using MultiApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MultiApp
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerClick);
        }

        private bool OnTimerClick()
        {
            DateTime dateTime = DateTime.Now;
            timeLabel.Text = dateTime.ToString("T");
            dateLabel.Text = dateTime.ToString("D");
            return true;
        }

        public void calendarBtnClicked(object s, EventArgs e)
        {
            /*
            var fileName = "database.sqlite";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);
            using (var conn = new SQLite.SQLiteConnection(path))
            {
                conn.CreateTable<Events>();
                var events = new Events { Title = "Anlassen", Date = "10.04.2016" };
                conn.Insert(events);
            }
            */
        }
    }
}
