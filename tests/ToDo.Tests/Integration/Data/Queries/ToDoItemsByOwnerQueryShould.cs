using System.Threading.Tasks;
using Todo.Core.Queries;
using Xunit;

namespace ToDo.Tests.Integration.Data.Queries
{
	public class ToDoItemsByOwnerQueryShould
	{
		[Fact]
		public async Task xxx()
		{
			ToDoItemsByOwnerQuery query = new ToDoItemsByOwnerQuery("wouterdekort");

			var repository = RepositoryHelper.GetRepository();

			await repository.AddAsync(new ToDoItemBuilder().WithOwnerId("wouterdekort").Build());
			await repository.AddAsync(new ToDoItemBuilder().WithOwnerId("wouterdekort").Build());
			await repository.AddAsync(new ToDoItemBuilder().WithOwnerId("wouterdekort").Build());
			await repository.AddAsync(new ToDoItemBuilder().WithOwnerId("someoneelse").Build());
			await repository.AddAsync(new ToDoItemBuilder().WithOwnerId("someoneelse").Build());

			var itemsOwnedByWouter = await repository.ListAsync(query);

			Assert.Equal(3, itemsOwnedByWouter.Count);
			Assert.All(itemsOwnedByWouter, i => Assert.Equal("wouterdekort", i.OwnerId));

		}
	}
}
