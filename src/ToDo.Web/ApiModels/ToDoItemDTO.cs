using System.ComponentModel.DataAnnotations;
using ToDo.Core.Entities;

namespace ToDo.Web.ApiModels
{
	public class ToDoItemDTO
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsDone { get; private set; }

		public static ToDoItemDTO FromToDoItem(ToDoItem item)
		{
			return new ToDoItemDTO()
			{
				Id = item.Id,
				Title = item.Title,
				Description = item.Description,
				IsDone = item.IsDone
			};
		}
	}
}
