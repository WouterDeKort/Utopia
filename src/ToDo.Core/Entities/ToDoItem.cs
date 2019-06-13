using System;
using ToDo.Core.Events;
using ToDo.Core.SharedKernel;

namespace ToDo.Core.Entities
{
    public class ToDoItem : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public string Avatar { get; set; }

        public int? Hours { get; set; }

        public DateTime DueDate { get; set; }
        public bool IsDone { get; private set; }

		public string OwnerId { get; set; }

        public void MarkComplete()
        {
            IsDone = true;
            Events.Add(new ToDoItemCompletedEvent(this));
        }

        public void Delete()
        {
            Events.Add(new ToDoItemDeletedEvent(this));
        }
    }
}
