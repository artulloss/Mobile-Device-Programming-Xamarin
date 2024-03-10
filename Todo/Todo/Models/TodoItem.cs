using System;
using SQLite;

namespace Todo.Models
{
	public class TodoItem
	{
		public TodoItem() {}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Title { get; set; }
		public bool IsDone { get; set; }

		public DateTime Modified { get; set; }
		public DateTime Added { get; set; }
	}
}

