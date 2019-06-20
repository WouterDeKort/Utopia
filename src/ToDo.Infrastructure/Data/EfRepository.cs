using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Core.Queries;
using ToDo.Core.Interfaces;
using ToDo.Core.SharedKernel;

namespace ToDo.Infrastructure.Data
{
	public class EfRepository : IRepository
	{
		public AppDbContext DbContext { get; private set; }

		public EfRepository(AppDbContext dbContext)
		{
			DbContext = dbContext;
		}

		public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
		{
			return DbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
		}

		public Task<List<T>> ListAsync<T>() where T : BaseEntity
		{
			return DbContext.Set<T>().ToListAsync();
		}
		public async Task<(int NumberOfPages, List<T> Items)> PageAsync<T>(int page, int pageSize) where T : BaseEntity
		{
			return await PageAsync<T>(null, page, pageSize);
		}

		public async Task<(int NumberOfPages, List<T> Items)> PageAsync<T>(QueryBase<T> query, int page, int pageSize) where T : BaseEntity
		{
			var set = DbContext.Set<T>().AsQueryable();

			if (query != null) set = query.Call(set);

			int numberOfPages = await set.CountAsync() / pageSize;
			var result = await set.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

			return (numberOfPages, result);
		}

		public Task<List<T>> ListAsync<T>(QueryBase<T> query) where T : BaseEntity
		{
			return query.Call(DbContext.Set<T>()).ToListAsync();
		}

		public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
		{
			DbContext.Set<T>().Add(entity);
			await DbContext.SaveChangesAsync();

			return entity;
		}

		public Task DeleteAsync<T>(T entity) where T : BaseEntity
		{
			DbContext.Set<T>().Remove(entity);
			return DbContext.SaveChangesAsync();
		}

		public Task UpdateAsync<T>(T entity) where T : BaseEntity
		{
			DbContext.Entry(entity).State = EntityState.Modified;
			return DbContext.SaveChangesAsync();
		}

	}
}
