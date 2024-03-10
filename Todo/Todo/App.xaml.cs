using System;
using System.Collections.Generic;
using System.IO;
using Todo.Data;
using Todo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();
            MainPage = new MainPage();
            MessagingCenter.Subscribe<MainPage, TodoItem>(this, "SaveItem", async (sender, todoItem) =>
            {
                await Database.SaveTodoItem(todoItem);
            });
        }

        private static TodoDatabase database = null;

        public static TodoDatabase Database
        {
            get
            {

                if(database == null)
                {
                    database = new TodoDatabase(
                        Path.Combine(
                            Environment.GetFolderPath(
                                Environment.SpecialFolder.LocalApplicationData
                            ),
                        "todo.sqlite")
                    );
                }
                return database;
            }
        }

        protected async override void OnStart()
        {
            List<TodoItem> items = await Database.GetTodoItems();
            MessagingCenter.Send<App, List<TodoItem>>(this, "LoadItems", items);
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }

        
    }
}

