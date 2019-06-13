﻿using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
using Xunit;
using System.Threading.Tasks;

namespace ToDo.Tests.Integration.Data
{
    public class EfRepositoryShould
    {
        private AppDbContext _dbContext;

        [Fact]
        public async Task AddItemAndSetId()
        {
            var repository = RepositoryHelper.GetRepository();
            var item = new ToDoItemBuilder().Build();

            await repository.AddAsync(item);

            var newItem = (await repository.ListAsync<ToDoItem>()).FirstOrDefault();

            Assert.Equal(item, newItem);
            Assert.True(newItem?.Id > 0);
        }

        [Fact]
        public async Task PageItems()
        {
			var repository = RepositoryHelper.GetRepository();

			for (int x = 0; x < 100; x++)
            {
                await repository.AddAsync(new ToDoItemBuilder().WithTitle(x.ToString()).Build());
            }

            var result = await repository.PageAsync<ToDoItem>(1, 10);

            Assert.Equal(10, result.NumberOfPages);
            Assert.Equal(10, result.Items.Count);
        }

        [Fact]
        public async Task UpdateItemAfterAddingIt()
        {
			// add an item
			var repository = RepositoryHelper.GetRepository();
			var initialTitle = Guid.NewGuid().ToString();
            var item = new ToDoItemBuilder().WithTitle(initialTitle).Build();

            await repository.AddAsync(item);

            // detach the item so we get a different instance
            _dbContext.Entry(item).State = EntityState.Detached;

            // fetch the item and update its title
            var newItem = (await repository.ListAsync<ToDoItem>())
                .FirstOrDefault(i => i.Title == initialTitle);
            Assert.NotNull(newItem);
            Assert.NotSame(item, newItem);
            var newTitle = Guid.NewGuid().ToString();
            newItem.Title = newTitle;

            // Update the item
            await repository.UpdateAsync(newItem);
            var updatedItem = (await repository.ListAsync<ToDoItem>())
                .FirstOrDefault(i => i.Title == newTitle);

            Assert.NotNull(updatedItem);
            Assert.NotEqual(item.Title, updatedItem.Title);
            Assert.Equal(newItem.Id, updatedItem.Id);
        }

        [Fact]
        public async Task DeleteItemAfterAddingIt()
        {
			// add an item
			var repository = RepositoryHelper.GetRepository();
			var initialTitle = Guid.NewGuid().ToString();
            var item = new ToDoItemBuilder().WithTitle(initialTitle).Build();
            await repository.AddAsync(item);

            // delete the item
            await repository.DeleteAsync(item);

            // verify it's no longer there
            Assert.DoesNotContain(await repository.ListAsync<ToDoItem>(),
                i => i.Title == initialTitle);
        }
    }
}
