using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crowe.Business;

namespace Crowe.Data.Test
{
	[TestClass]
	public class DataTest
	{
		[TestMethod]
		public void TestData()
		{
			using var dbContext = CroweDbContextFactory.CreateDbContext(@"Server=(localdb)\mssqllocaldb;Database=CroweInterviewTest;Trusted_Connection=True;MultipleActiveResultSets=true");

			try
			{
				// test Add and Read
				var message = new Message { Value = "Hello World" };
				dbContext.Messages.Add(message);
				dbContext.SaveChanges();
				Assert.AreEqual(dbContext.Messages.Count(), 1);
				var message0 = dbContext.Messages.Where(m => m.Id == 1).First();
				Assert.IsNotNull(message0);
				Assert.AreEqual(message0.Value, message.Value);

				// test update
				var newmessage = String.Concat(message.Value, "_0");
				dbContext.Update(message0).Entity.Value = newmessage;
				dbContext.SaveChanges();
				var message1 = dbContext.Messages.Where(m => String.Compare(m.Value, newmessage) == 0).First();
				Assert.IsNotNull(message1);
				Assert.AreEqual(message1.Value, newmessage);
				
				// test Remove
				dbContext.Remove(message1);
				dbContext.SaveChanges();
				Assert.AreEqual(dbContext.Messages.Count(), 0);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
			finally
			{
				dbContext.Database.EnsureDeleted();
			}
		}
	}
}
