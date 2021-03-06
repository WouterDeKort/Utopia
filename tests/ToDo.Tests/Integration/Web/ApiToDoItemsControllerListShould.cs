﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Todo.Web;
using ToDo.Core.Entities;
using Xunit;

namespace ToDo.Tests.Integration.Web
{

	public class ApiToDoItemsControllerList : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _client;

		public ApiToDoItemsControllerList(CustomWebApplicationFactory factory)
		{
			_client = factory.CreateClient();
		}

		[Fact(Skip ="Figure out how to use UserManager in tests")]
		public async Task ReturnsTwoItems()
		{
			var response = await _client.GetAsync("/api/todoitems");
			response.EnsureSuccessStatusCode();
			var stringResponse = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(stringResponse).ToList();

			Assert.Equal(100, result.Count);
		}
	}
}
