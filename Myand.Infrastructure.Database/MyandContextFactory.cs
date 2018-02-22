using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myand.Infrastructure.Database
{
	public class MyandContextFactory : IDesignTimeDbContextFactory<MyandContext>
	{
		public MyandContextFactory()
		{
		}


		public MyandContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<MyandContext>();
			builder.UseSqlServer(
				"Server=(localdb)\\mssqllocaldb;Database=myand;Trusted_Connection=True;MultipleActiveResultSets=true");

			return new MyandContext(builder.Options);
		}
	}
}
