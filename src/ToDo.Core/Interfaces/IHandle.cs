using ToDo.Core.SharedKernel;

namespace ToDo.Core.Interfaces
{
	public interface IHandle<in T> where T : BaseDomainEvent
	{
		void Handle(T domainEvent);
	}
}