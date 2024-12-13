using System;

namespace ToDoList
{
	public class Task
	{
		private string description;
		private bool isDone;

		public void SetDone()
		{
			isDone = true;
		}

		public string[] ShowTask()
		{
			return new string[2] { description, isDone ? "done" : ""};
		}

		public Task (string description)
		{
			this.description = description;
			isDone = false;
		}
	}
}
