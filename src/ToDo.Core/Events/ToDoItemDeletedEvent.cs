using ToDo.Core.Entities;
using ToDo.Core.SharedKernel;

namespace ToDo.Core.Events
{
	public class ToDoItemDeletedEvent : BaseDomainEvent
	{
		public ToDoItem DeletedItem { get; set; }

		public ToDoItemDeletedEvent(ToDoItem deletedItem)
		{
			DeletedItem = deletedItem;
		}
	}
}
