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
		private readonly AppDbContext _dbContext;

		public EfRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
		{
			return _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
		}

		public Task<List<T>> ListAsync<T>() where T : BaseEntity
		{
			return _dbContext.Set<T>().ToListAsync();
		}
		public async Task<(int NumberOfPages, List<T> Items)> PageAsync<T>(int page, int pageSize) where T : BaseEntity
		{
			return await PageAsync<T>(null, page, pageSize);
		}

		public async Task<(int NumberOfPages, List<T> Items)> PageAsync<T>(QueryBase<T> query, int page, int pageSize) where T : BaseEntity
		{
			var set = _dbContext.Set<T>().AsQueryable();

			if (query != null) set = query.Call(set);

			int numberOfPages = await set.CountAsync() / pageSize;
			var result = await set.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

			return (numberOfPages, result);
		}

		public Task<List<T>> ListAsync<T>(QueryBase<T> query) where T : BaseEntity
		{
			return query.Call(_dbContext.Set<T>()).ToListAsync();
		}

		public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
		{
			_dbContext.Set<T>().Add(entity);
			await _dbContext.SaveChangesAsync();

			return entity;
		}

		public Task DeleteAsync<T>(T entity) where T : BaseEntity
		{
			_dbContext.Set<T>().Remove(entity);
			return _dbContext.SaveChangesAsync();
		}

		public Task UpdateAsync<T>(T entity) where T : BaseEntity
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			return _dbContext.SaveChangesAsync();
		}

	}
}
