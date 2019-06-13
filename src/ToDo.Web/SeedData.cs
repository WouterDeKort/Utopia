using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Infrastructure.Data;
using ToDo.Infrastructure.Identity;

namespace ToDo.Web
{
	public static class SeedData
	{
		public static async Task<int> PopulateTestDataAsync(AppDbContext context, UserManager<User> userManager)
		{
			var toDos = context.ToDoItems.ToList();
			foreach (var item in toDos)
			{
				context.Remove(item);
			}
			context.SaveChanges();

			Random rand = new Random(Guid.NewGuid().GetHashCode());
			const int numberOfItems = 100;
			for (int n = 0; n < numberOfItems; n++)
			{
				var person = UserNames.GetRandomName();

				var user = await userManager.FindByEmailAsync(person.email);
				if (user == null)
				{
					user = new User
					{
						Name = person.name,
						UserName = person.name.Replace(" ", ""),
						Email = person.email,
					};

					var result = await userManager.CreateAsync(user, "MySecretPassword1@");

					if (!result.Succeeded)
					{
						var s = " bla";
					}
				}

				var item = new ToDoItem
				{
					Description = TextGenerator.GetText(10, 25),
					DueDate = DateTime.Now.AddSeconds(rand.Next(60 * 60 * 4, 60 * 60 * 24 * 7)),
					Hours = rand.Next(1, 8),
					Owner = user.Name,
					OwnerId = user.Id,
					Title = TextGenerator.GetText(3, 10),
					Avatar = $"https://api.adorable.io/avatars/285/{user.UserName}.png"
				};

				context.Add(item);
			}
			context.SaveChanges();

			return numberOfItems;
		}

		private static class TextGenerator
		{
			private static string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
			private static Random rand = new Random(Guid.NewGuid().GetHashCode());

			public static string GetText(int minimumNumberOfWords, int maximumNumberOfWords)
			{
				int numberOfWords = rand.Next(minimumNumberOfWords, maximumNumberOfWords);

				IEnumerable<string> words = loremIpsum.Split();
				if (numberOfWords < words.Count())
				{
					words = words.Take(numberOfWords);
				}

				return string.Join(" ", words);
			}
		}

		private static class UserNames
		{
			public static (string name, string email) GetRandomName()
			{
				return people[rand.Next(0, people.Length)];
			}

			private static readonly Random rand = new Random(Guid.NewGuid().GetHashCode());
			private static (string name, string email)[] people = new (string, string)[]
			{
				( "Wouter de Kort", "wouter@utopia.com" ),
				( "Bill Gates", "bill@utopia.com" ),
				( "Satya Nadella", "satya@utopia.com" ),
			};
		}
	}
}
