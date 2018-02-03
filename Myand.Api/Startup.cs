using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Myand.Infrastructure.CrossCutting.IoC;
using Myand.Infrastructure.Database;
using Swashbuckle.AspNetCore.Swagger;

namespace Myand.Api
{
	public class Startup
	{
		public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<MyandContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString(Globals.api_database_connection_string_name)));

			services.AddCors();

			services.AddMvcCore()
				.AddAuthorization()
				.AddJsonFormatters(j =>
				j.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
				).AddApiExplorer();


			services.AddSwaggerGen(s =>
			{
				s.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "Myand",
					Description = "MyandAPI Swagger surface",
					Contact = new Contact { Name = "Nurul Hadi", Email = "n.hadi1996@gmail.com", Url = "https://github.com/NHadi" }
				});
			});


			// .NET Native DI Abstraction
			RegisterServices(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app,
							  IHostingEnvironment env,
							  ILoggerFactory loggerFactory,
							  IHttpContextAccessor accessor)
		{
			loggerFactory.AddConsole();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(c =>
			{
				c.AllowAnyHeader();
				c.AllowAnyMethod();
				c.AllowAnyOrigin();
			});

			app.UseAuthentication();
			app.UseMvc();

			app.UseSwagger();
			app.UseSwaggerUI(s =>
			{
				s.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAnd API v1.1");
			});
		}

		private static void RegisterServices(IServiceCollection services)
		{
			// Adding dependencies from another layers (isolated from Presentation)
			InjectorBootstraper.RegisterServices(services);
			
		}
	}
}
