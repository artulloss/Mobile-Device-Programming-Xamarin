using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;
using Xamarin.Forms;

namespace Todo
{
    public partial class MainPage : ContentPage
    {

        public ObservableCollection<TodoItem> TodoItems { get; set; }

        public MainPage()
        {
            InitializeComponent();
            TodoItems = new ObservableCollection<TodoItem>();
            TodoListView.ItemsSource = TodoItems;
            BindingContext = this;
            MessagingCenter.Subscribe<App, List<TodoItem>>(this, "LoadItems", (sender, todoItems) =>
            {
                foreach (var item in todoItems)
                {
                    TodoItems.Add(item);
                }
            });
        }

        public void OnAddClicked(System.Object sender, System.EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(NewItemEntry.Text))
            {
                var item = new TodoItem { Title = NewItemEntry.Text };
                TodoItems.Add(item);
                MessagingCenter.Send<MainPage, TodoItem>(this, "SaveItem", item);
                NewItemEntry.Text = string.Empty; // Reset our entry box
            }
        }
    }
}

