namespace Voronov.GetItTestApp.Web
{
	using System.IO;

	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Hosting;

	using Autofac.Extensions.DependencyInjection;

	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args)
				.Build()
				.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost
				.CreateDefaultBuilder(args)
				.ConfigureServices(services => services.AddAutofac())
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseWebRoot(Directory.GetCurrentDirectory())
				.UseStartup<Startup>();
	}
}
