using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using MR.DependencyInjection.Abstractions;

namespace MR.DependencyInjection.Autofac
{
	public static class AutofacRegistration
	{
		public static void Populate(
				this ContainerBuilder builder,
				IEnumerable<ServiceDescriptor> descriptors)
		{
			builder.RegisterType<AutofacServiceProvider>().As<IServiceProvider>();
			builder.RegisterType<AutofacServiceScopeFactory>().As<IServiceScopeFactory>();

			Register(builder, descriptors);
		}

		private static void Register(
				ContainerBuilder builder,
				IEnumerable<ServiceDescriptor> descriptors)
		{
			foreach (var descriptor in descriptors)
			{
				if (descriptor.ImplementationType != null)
				{
					// Test if the an open generic type is being registered
					var serviceTypeInfo = descriptor.ServiceType.GetTypeInfo();
					if (serviceTypeInfo.IsGenericTypeDefinition)
					{
						builder
							.RegisterGeneric(descriptor.ImplementationType)
							.As(descriptor.ServiceType)
							.ConfigureLifecycle(descriptor.Lifetime);
					}
					else
					{
						builder
							.RegisterType(descriptor.ImplementationType)
							.As(descriptor.ServiceType)
							.ConfigureLifecycle(descriptor.Lifetime);
					}
				}
				else if (descriptor.ImplementationFactory != null)
				{
					var registration = RegistrationBuilder.ForDelegate(descriptor.ServiceType, (context, parameters) =>
					{
						var serviceProvider = context.Resolve<IServiceProvider>();
						return descriptor.ImplementationFactory(serviceProvider);
					})
					.ConfigureLifecycle(descriptor.Lifetime)
					.CreateRegistration();

					builder.RegisterComponent(registration);
				}
				else
				{
					builder
						.RegisterInstance(descriptor.ImplementationInstance)
						.As(descriptor.ServiceType)
						.ConfigureLifecycle(descriptor.Lifetime);
				}
			}
		}

		private static IRegistrationBuilder<object, T, U> ConfigureLifecycle<T, U>(
				this IRegistrationBuilder<object, T, U> registrationBuilder,
				ServiceLifetime lifecycleKind)
		{
			switch (lifecycleKind)
			{
				case ServiceLifetime.Singleton:
					registrationBuilder.SingleInstance();
					break;

				case ServiceLifetime.Scoped:
					registrationBuilder.InstancePerLifetimeScope();
					break;

				case ServiceLifetime.Transient:
					registrationBuilder.InstancePerDependency();
					break;
			}

			return registrationBuilder;
		}
	}
}
