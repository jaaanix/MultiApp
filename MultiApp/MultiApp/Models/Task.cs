using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiApp.Models
{
    class Task
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Completed { get; set; }

        // 'Items' wird als 'Binding'-Property der ListView angegeben
        public ObservableCollection<string> Items { get; private set; }

        public ObservableCollection<string> ChildItems { get; private set; }

        public Task()
        {
            // Items und ChildItems vom Typ String Collection initialisieren
            Items = new ObservableCollection<string> { };
            ChildItems = new ObservableCollection<string> { };
        }
    }
}
