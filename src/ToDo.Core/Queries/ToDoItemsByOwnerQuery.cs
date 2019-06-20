using System;
using System.Linq;
using ToDo.Core.Entities;

namespace Todo.Core.Queries
{
	public class ToDoItemsByOwnerQuery : QueryBase<ToDoItem>
	{
		public ToDoItemsByOwnerQuery(string ownerId)
		{
			if (ownerId == null) throw new ArgumentNullException(nameof(ownerId));

			Query = set => set.Where(item => item.OwnerId == ownerId);
		}
	}
}
