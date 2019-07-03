using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Web.ApiModels;
using ToDo.Web.Filters;

namespace ToDo.Web.Api
{
	[Route("api/[controller]")]
	[ValidateModel]
	public class ToDoItemsController : Controller
	{
		private readonly IRepository _repository;

		public ToDoItemsController(IRepository repository)
		{
			_repository = repository;
		}

		// GET: api/ToDoItems
		[HttpGet]
		public async Task<IActionResult> ListAsync()
		{
			var items = (await _repository.ListAsync<ToDoItem>())
							.Select(ToDoItemDto.FromToDoItem);
			return Ok(items);
		}

		// GET: api/ToDoItems
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var item = ToDoItemDto.FromToDoItem(await _repository.GetByIdAsync<ToDoItem>(id));
			return Ok(item);
		}

		// POST: api/ToDoItems
		[HttpPost]
		public async Task<IActionResult> PostAsync([FromBody] ToDoItemDto item)
		{
			var todoItem = new ToDoItem()
			{
				Title = item.Title,
				Description = item.Description
			};
			await _repository.AddAsync(todoItem);
			return Ok(ToDoItemDto.FromToDoItem(todoItem));
		}

		[HttpPatch("{id:int}/complete")]
		public async Task<IActionResult> CompleteAsync(int id)
		{
			var toDoItem = await _repository.GetByIdAsync<ToDoItem>(id);
			toDoItem.MarkComplete();
			await _repository.UpdateAsync(toDoItem);

			return Ok(ToDoItemDto.FromToDoItem(toDoItem));
		}
	}
}
