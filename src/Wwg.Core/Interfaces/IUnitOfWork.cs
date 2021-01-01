using Wwg.Core.Entities;

namespace Wwg.Core.Interfaces
{
	/// <summary>
	/// An aggregation of the repositories to keep track of their transations and alter the database as result of work.
	/// </summary>
	public interface IUnitOfWork
	{
		/// <summary>
		/// Commit all changes made in the entity.
		/// </summary>
		void Commit();

		/// <summary>
		/// Get the repository associated with the entity.
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <returns></returns>
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
	}
}
