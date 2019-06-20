using System.Linq;
using ToDo.Core.Events;
using Xunit;

namespace ToDo.Tests.Core.Entities
{
	public class ToDoItemDeleteShould
	{
		[Fact]
		public void RaiseToDoItemCompletedEvent()
		{
			var item = new ToDoItemBuilder().Build();

			item.Delete();

			Assert.Single(item.Events);
			Assert.IsType<ToDoItemDeletedEvent>(item.Events.First());
		}
	}
}
