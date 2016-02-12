using System.Collections.Generic;

namespace MR.DependencyInjection.Abstractions
{
	/// <summary>
	/// Specifies the contract for a collection of service descriptors.
	/// </summary>
	public interface IServiceCollection : IList<ServiceDescriptor>
	{
	}
}
