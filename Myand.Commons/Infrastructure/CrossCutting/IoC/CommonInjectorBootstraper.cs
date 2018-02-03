using Microsoft.Extensions.DependencyInjection;
using Myand.Commons.Application;
using Myand.Commons.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myand.Commons.Infrastructure.CrossCutting.IoC
{
	public class CommonInjectorBootstraper
	{
		public static void RegisterCommonServices(IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped(typeof(IAppService<>), typeof(AppService<>));
		}
	}
}
