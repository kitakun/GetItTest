namespace Voronov.GetItTestApp.Web.Infrastructure
{
	using System;
	using System.IO;
	using System.Net;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Logging;

	using Newtonsoft.Json;

	public class ApiErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ApiErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, ILogger<ApiErrorHandlingMiddleware> logger)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, ex.ToString());
				await HandleExceptionAsync(context, ex);
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			if (context.Response.HasStarted)
			{
				//response already written earlier in the pipeline
				return;
			}

			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var error = new
			{
				message = ex.Message
			};

			context.Response.ContentType = "application/json";

			using (var writer = new StreamWriter(context.Response.Body))
			{
				new JsonSerializer().Serialize(writer, error);
				await writer.FlushAsync().ConfigureAwait(false);
			}

			return;
		}
	}
}
