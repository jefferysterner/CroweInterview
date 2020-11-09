using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Crowe.Business;
using Crowe.Data;

namespace Crowe.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration) => Configuration = configuration;

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddDbContext<CroweDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CroweInterviewConnStr")));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CroweDbContext dbContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => endpoints.MapControllers());
			SeedData(dbContext, "Hello World");
		}

		private void SeedData(CroweDbContext dbContext, string message)
		{
			dbContext.Database.EnsureCreated();
			if (dbContext.Messages.FirstOrDefault(m => String.Compare(m.Value, message) == 0) == null)
			{
				dbContext.Messages.Add(new Message() { Value = message });
				dbContext.SaveChanges();
			}
		}
	}
}
