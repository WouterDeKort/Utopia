using Ardalis.GuardClauses;
using ToDo.Core.Events;
using ToDo.Core.Interfaces;

namespace ToDo.Core.Services
{
	public class ToDoItemService : IHandle<ToDoItemCompletedEvent>, IHandle<ToDoItemDeletedEvent>
	{
		public void Handle(ToDoItemCompletedEvent domainEvent)
		{
			Guard.Against.Null(domainEvent, nameof(domainEvent));

			// Do Nothing
		}

		public void Handle(ToDoItemDeletedEvent domainEvent)
		{
			Guard.Against.Null(domainEvent, nameof(domainEvent));

			// Do Nothing
		}
	}
}
