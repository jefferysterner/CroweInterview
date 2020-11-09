using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Crowe.Business;

namespace Crowe.Data
{
	public class CroweDbContext : DbContext
	{
		public CroweDbContext(DbContextOptions<CroweDbContext> options) : base(options) { }
		public DbSet<Message> Messages { get; set; }
	}

	public class CroweDbContextFactory : IDesignTimeDbContextFactory<CroweDbContext>
	{
		public static CroweDbContext CreateDbContext(string connstr) { return new CroweDbContextFactory().CreateDbContext(new string[] { connstr }); }
		public CroweDbContext CreateDbContext(string[] args)
		{
			var db = new CroweDbContext(new DbContextOptionsBuilder<CroweDbContext>().UseSqlServer(args[0]).Options);
			db.Database.EnsureCreated();
			return db;
		}
	}
}
