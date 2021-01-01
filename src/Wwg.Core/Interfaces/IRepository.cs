using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Wwg.Core.Entities;

namespace Wwg.Core.Interfaces
{
	/// <summary>
	/// Interface of a repository.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IRepository<TEntity> where TEntity : BaseEntity
	{
		void Add(TEntity entity);
		int Count(Expression<Func<TEntity, bool>> predicate = null);
		IReadOnlyList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
		TEntity Single(Expression<Func<TEntity, bool>> predicate);
		TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, TEntity defaultValue = default);
		IReadOnlyList<TEntity> GetPagedAll(int page, int pageSize, Expression<Func<TEntity, bool>> predicate = null);
		void Remove(TEntity entity);
		void Update(TEntity entity);
	}
}
