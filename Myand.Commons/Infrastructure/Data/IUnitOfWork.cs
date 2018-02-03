using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Myand.Commons.Infrastructure.Data
{
	public interface IUnitOfWork : IDisposable
	{
		DbContext Context { get; }
		void Commit();
		Task CommitAsync();
	}
}
