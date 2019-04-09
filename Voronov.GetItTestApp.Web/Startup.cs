namespace Voronov.GetItTestApp.Web
{
	using System.Text;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.IdentityModel.Tokens;

	using Autofac;

	using Voronov.GetItTestApp.Web.Infrastructure;
	using Voronov.GetItTestApp.Core.Utilities;

	public class Startup
	{
		public IConfiguration Configuration { get; }

		public IServiceCollection CoreServices { get; private set; }

		public IContainer ApplicationContainer { get; private set; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			CoreServices = services;

			services
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = Configuration["Jwt:Issuer"],
						ValidAudience = Configuration["Jwt:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
					};
				});

			services
				.AddMvcCore(options =>
				{
					options.RespectBrowserAcceptHeader = true;
				})
				.AddAuthorization()
				.AddFormatterMappings()
				.AddCors()
				.AddJsonFormatters()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

#if DEBUG
			//postman/insomnia/standalone-watcher-client
			services.AddCors(o => o.AddPolicy(Constants.AllCorsName, builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader();
			}));
#endif

			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			AssemblyFinder.LoadAllAssembliesInDomain();

			DependencyRegister.Register(builder, CoreServices);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
		{
			app.UseMiddleware<ApiErrorHandlingMiddleware>();

			if (env.IsDevelopment())
			{
				app.UseHsts();
			}

			app.UseAuthentication();
			app.UseHttpsRedirection();
			//letting the application know that we need access to wwwroot folder.
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});

			appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
		}
	}
}
