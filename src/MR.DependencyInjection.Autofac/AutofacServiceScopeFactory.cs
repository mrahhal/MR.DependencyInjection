using Autofac;
using MR.DependencyInjection.Abstractions;

namespace MR.DependencyInjection.Autofac
{
	internal class AutofacServiceScopeFactory : IServiceScopeFactory
	{
		private readonly ILifetimeScope _lifetimeScope;

		public AutofacServiceScopeFactory(ILifetimeScope lifetimeScope)
		{
			_lifetimeScope = lifetimeScope;
		}

		public IServiceScope CreateScope()
		{
			return new AutofacServiceScope(_lifetimeScope.BeginLifetimeScope());
		}
	}
}
