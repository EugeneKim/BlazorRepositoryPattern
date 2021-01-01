using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Wwg.Core.Interfaces
{
	/// <summary>
	/// Interface of <see cref="DbContext"/>.
	/// </summary>
	/// <remarks>
	/// Purpose of this interface is mainly for the dependency injection.
	/// See the constructor of the <see cref="Generic.GenericRepository{TEntity}"/>.
	/// </remarks>
	public interface IDbContext
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
		int SaveChanges();
		int SaveChanges(bool acceptAllChangesOnSuccess);
		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		void Dispose();
	}
}
