using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wwg.Core.Entities;
using Wwg.Core.Interfaces;

namespace Wwg.Core.Generic
{
	/// <summary>
	/// A unit of work which is pre-implemented for generic purpose.
	/// </summary>
	/// <remarks>
	/// This will automatically create a <see cref="GenericRepository{TEntity}"/> repository on demand.
	/// Unit of Work 
	/// </remarks>
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IDbContext context;
		private readonly List<object> repositories = new List<object>();

		public UnitOfWork(IDbContext context)
		{
			this.context = context;
		}

		public void Commit() => context.SaveChanges();

		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
		{
			var repository = repositories.SingleOrDefault(r => r.GetType() == typeof(TEntity));

			if (repository == null)
			{
				if (typeof(TEntity) == typeof(Word))
					repository = new WordRepository(context);
				else
					repository = new GenericRepository<TEntity>(context);

				repositories.Add(repository);
			}
			
			return (IRepository<TEntity>)repository;
		}
	}
}
