using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Crowe.Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly IHttpClientFactory _httpClient;
		private readonly string _url;
		public IndexModel(IHttpClientFactory httpClient, IConfiguration config)
		{
			_httpClient = httpClient;
			_url = config["Settings:ApiUrl"];
		}

		public string Message { get; set; }
		public async Task OnGet()
		{
			try
			{
				Message = await _httpClient.CreateClient().GetStringAsync(_url+ "?message=angel");
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine("Exception Message :{0} ", ex.Message);
			}
		}
	}
}
