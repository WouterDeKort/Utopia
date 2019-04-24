using ToDo.Core.Interfaces;
using ToDo.Core.SharedKernel;

namespace ToDo.Tests
{
    public class NoOpDomainEventDispatcher : IDomainEventDispatcher
    {
        public void Dispatch(BaseDomainEvent domainEvent) { }
    }
}
