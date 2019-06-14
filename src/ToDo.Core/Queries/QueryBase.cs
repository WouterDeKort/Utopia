using System;
using System.Linq;

namespace Todo.Core.Queries
{
	public abstract class QueryBase<T> where T : class
	{
		protected Func<IQueryable<T>, IQueryable<T>> Query { private get; set; }

		public IQueryable<T> Call(IQueryable<T> set)
		{
			return Query.Invoke(set);
		}
	}
}
