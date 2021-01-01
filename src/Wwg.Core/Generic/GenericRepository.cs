using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wwg.Core.Entities;
using Wwg.Core.Interfaces;

namespace Wwg.Core.Generic
{
	/// <summary>
	/// A repository which is pre-implemented for generic purpose.
	/// </summary>
	/// <remarks>
	/// Implement <see cref="IRepository{TEntity}"/> if you need customised repository.
	/// </remarks>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly DbSet<TEntity> dbSet;

		public GenericRepository(IDbContext dbContext) => dbSet = dbContext.Set<TEntity>();

		public void Add(TEntity entity) => dbSet.Add(entity);

		public int Count(Expression<Func<TEntity, bool>> predicate = null) =>
			(predicate == null ? dbSet : dbSet.Where(predicate)).Count();

		public IReadOnlyList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null) =>
			(predicate == null ? dbSet : dbSet.Where(predicate)).ToList();

		public TEntity Single(Expression<Func<TEntity, bool>> predicate) =>
			(predicate == null ? dbSet : dbSet.Where(predicate)).Single(predicate);

		public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, TEntity defaultValue = default) =>
			(predicate == null ? dbSet : dbSet.Where(predicate)).SingleOrDefault(predicate) ?? default;

		public IReadOnlyList<TEntity> GetPagedAll(int page, int pageSize, Expression<Func<TEntity, bool>> predicate = null) =>
			(predicate == null ? dbSet : dbSet.Where(predicate)).Skip(page * pageSize).Take(pageSize).ToList();

		public void Remove(TEntity entity) => dbSet.Remove(entity);

		public void Update(TEntity entity) => dbSet.Update(entity);
	}
}
