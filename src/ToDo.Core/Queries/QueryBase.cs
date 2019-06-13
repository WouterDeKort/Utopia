using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.SharedKernel;

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
