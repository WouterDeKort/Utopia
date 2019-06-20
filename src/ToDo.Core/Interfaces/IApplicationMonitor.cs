using System.Collections.Generic;

namespace ToDo.Core.Interfaces
{
	public interface IApplicationMonitor
	{
		void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
	}
}
