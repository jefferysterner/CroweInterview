using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Crowe.Business;
using Crowe.Data;

namespace Crowe.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HelloWorldController : ControllerBase
	{
		private readonly CroweDbContext _dbContext;
		public HelloWorldController(CroweDbContext dbContext) => _dbContext = dbContext;

		[HttpGet]
		public string Get(string message)
		{
			if (!String.IsNullOrEmpty(message) && _dbContext.Messages.FirstOrDefault(m => String.Compare(m.Value, message) == 0) == null)
				SaveMessage(message);
			return _dbContext.Messages.FirstOrDefault(m => String.Compare(m.Value, message) == 0)?.Value
				?? _dbContext.Messages.FirstOrDefault(m => m.Id == 1).Value;
		}

		private void SaveMessage(string message)
		{
			_dbContext.Messages.Add(new Message { Value = message });
			_dbContext.SaveChanges();
		}
	}
}
