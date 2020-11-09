using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crowe.Console_
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var config = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true).Build();
			var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();

			try
			{
				var message = await serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient()
					.GetStringAsync(config["Settings:ApiUrl"]);
				Console.WriteLine(message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			Console.ReadKey();
		}
	}
}
