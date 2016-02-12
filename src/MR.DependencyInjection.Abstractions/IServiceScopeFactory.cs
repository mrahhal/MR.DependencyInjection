namespace MR.DependencyInjection.Abstractions
{
	public interface IServiceScopeFactory
	{
		/// <summary>
		/// Create an <see cref="MR.DependencyInjection.IServiceScope"/> which
		/// contains an <see cref="System.IServiceProvider"/> used to resolve dependencies from a
		/// newly created scope.
		/// </summary>
		/// <returns>
		/// An <see cref="MR.DependencyInjection.IServiceScope"/> controlling the
		/// lifetime of the scope. Once this is disposed, any scoped services that have been resolved
		/// from the <see cref="MR.DependencyInjection.IServiceScope.ServiceProvider"/>
		/// will also be disposed.
		/// </returns>
		IServiceScope CreateScope();
	}
}
