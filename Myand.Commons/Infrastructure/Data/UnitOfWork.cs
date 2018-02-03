using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Myand.Commons.Infrastructure.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		public DbContext Context { get; }

		public UnitOfWork(DbContext context)
		{
			Context = context;
		}
		public void Commit()
		{
			Context.SaveChanges();
		}

		public async Task CommitAsync()
		{
			await Context.SaveChangesAsync();
		}

		public void Dispose()
		{
			Context.Dispose();

		}
	}
}
