using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Core.SharedKernel
{
	// This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
	public abstract class BaseEntity
	{
		public int Id { get; set; }

		public List<BaseDomainEvent> Events { get; } = new List<BaseDomainEvent>();
	}
}