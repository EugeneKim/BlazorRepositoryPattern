using System;

namespace Wwg.Core.Entities
{
	public abstract class BaseEntity
	{
		public virtual Guid Id { get; protected set; }
	}
}
