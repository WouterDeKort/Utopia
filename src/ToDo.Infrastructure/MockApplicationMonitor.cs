using System.Collections.Generic;
using ToDo.Core.Interfaces;

namespace ToDo.Infrastructure
{
	public class MockApplicationMonitor : IApplicationMonitor
	{
		public MockApplicationMonitor()
		{
			
		}

		public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
		{
		}
	}
}
