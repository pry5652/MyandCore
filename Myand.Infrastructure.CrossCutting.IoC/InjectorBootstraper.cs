using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Myand.Commons.Application;
using Myand.Commons.Infrastructure.CrossCutting.IoC;
using Myand.Commons.Infrastructure.Data;
using Myand.Infrastructure.Database;
using Myand.Service;

namespace Myand.Infrastructure.CrossCutting.IoC
{
	public class InjectorBootstraper : CommonInjectorBootstraper
	{
		public static void RegisterServices(IServiceCollection services)
		{
			// Register Common
			RegisterCommonServices(services);

			// ASP.NET HttpContext dependency
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			// Infra - Data						
			services.AddTransient<IMstUserService, MstUserService>();
			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddTransient<DbContext, MyandContext>();

		}
	}
}
