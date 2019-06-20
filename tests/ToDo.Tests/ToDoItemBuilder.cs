using ToDo.Core.Entities;

namespace ToDo.Tests
{
	public class ToDoItemBuilder
	{
		private readonly ToDoItem _todo = new ToDoItem();

		public ToDoItemBuilder WithId(int id)
		{
			_todo.Id = id;
			return this;
		}

		public ToDoItemBuilder WithTitle(string title)
		{
			_todo.Title = title;
			return this;
		}

		public ToDoItemBuilder WithDescription(string description)
		{
			_todo.Description = description;
			return this;
		}

		public ToDoItemBuilder WithOwnerId(string ownerId)
		{
			_todo.OwnerId = ownerId;
			return this;
		}

		public ToDoItem Build() => _todo;
	}
}
