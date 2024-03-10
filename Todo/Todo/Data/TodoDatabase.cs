using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Todo.Models;

namespace Todo.Data
{
	public class TodoDatabase
	{
        readonly SQLiteAsyncConnection database;

        public TodoDatabase(string dbPath)
		{
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TodoItem>().Wait();
        }

		public async Task<List<TodoItem>> GetTodoItems()
		{
			return await database.Table<TodoItem>().ToListAsync();
		}

		public async Task<TodoItem> GetTodoItemById(int id)
		{
			return await database.Table<TodoItem>()
								.Where(item => item.ID == id)
								.FirstOrDefaultAsync();
		}

		public async Task<int> SaveTodoItem(TodoItem item)
		{
			if(item.ID != 0)
			{
				return await database.UpdateAsync(item);
			} else
			{
				return await database.InsertAsync(item);
			}
		}

		public async Task<int> Delete(TodoItem item)
		{
			return await database.DeleteAsync(item);
		}

	}
}
