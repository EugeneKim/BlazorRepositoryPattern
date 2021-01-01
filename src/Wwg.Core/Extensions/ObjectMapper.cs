using System;

namespace Wwg.Core.Extensions
{
	/// <summary>
	/// Automatically maps the properties between two objects based on the type and name.
	/// </summary>
	/// <remarks>
	/// This is a simplier but similar way to the AutoMapper: https://automapper.org/
	/// </remarks>
	public sealed class ObjectMapper<T1, T2>
	{
		public T1 Map(T2 source)
		{
			T1 targetItem = Activator.CreateInstance<T1>();

			var properties = typeof(T1).GetProperties();
			var targetProps = typeof(T2).GetProperties();

			foreach (var p in properties)
			{
				foreach (var targetProp in targetProps)
				{
					if (p.Name == p.Name)
						targetProp.SetValue(targetItem, p.GetValue(source));
				}
			}

			return targetItem;
		}
	}
}
