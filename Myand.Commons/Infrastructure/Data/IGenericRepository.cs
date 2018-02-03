using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Myand.Commons.Infrastructure.Data
{
	public interface IGenericRepository<T> where T : class
	{		
		IEnumerable<T> Get();
		IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
		Task<IEnumerable<T>> GetAsync();
		Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
		void Add(T entity);
		void Delete(T entity);
		void Update(T entity);
		void Save();
		Task SaveAsync();

	}
}
