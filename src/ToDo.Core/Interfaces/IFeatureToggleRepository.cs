
using ToDo.Core.Entities;

namespace ToDo.Core.Interfaces
{
	public interface IFeatureToggleRepository
	{
		bool StatisicsIsEnabled(User user);
	}
}
