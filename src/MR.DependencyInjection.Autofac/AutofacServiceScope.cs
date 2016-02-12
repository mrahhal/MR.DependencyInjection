﻿using System;
using Autofac;
using MR.DependencyInjection.Abstractions;

namespace MR.DependencyInjection.Autofac
{
	internal class AutofacServiceScope : IServiceScope
	{
		private readonly ILifetimeScope _lifetimeScope;

		public AutofacServiceScope(ILifetimeScope lifetimeScope)
		{
			_lifetimeScope = lifetimeScope;
			ServiceProvider = _lifetimeScope.Resolve<IServiceProvider>();
		}

		public IServiceProvider ServiceProvider { get; }

		public void Dispose()
		{
			_lifetimeScope.Dispose();
		}
	}
}
