using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Crowe.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crowe.Api.Test
{
	[TestClass]
	public class ApiTest
	{
		private static readonly HttpClient client = new HttpClient();
		private const string _url = @"http://localhost:54920/HelloWorld";
		private const string _testmessage = "testmessage";

		[TestMethod]
		public async Task TestApi()
		{
			try
			{
				// test returned value
				Assert.AreEqual(await client.GetStringAsync(_url), "Hello World");
				Assert.AreEqual(await client.GetStringAsync($"{_url}?message={_testmessage}"), _testmessage);

				// test that specified value saved to database
				using var db = CroweDbContextFactory.CreateDbContext(@"Server=(localdb)\mssqllocaldb;Database=CroweInterview;Trusted_Connection=True;MultipleActiveResultSets=true");
				Assert.AreEqual(db.Messages.First(m => String.Compare(m.Value, _testmessage) == 0).Value, _testmessage);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
