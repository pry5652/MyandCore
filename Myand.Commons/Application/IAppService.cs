using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Myand.Commons.Application
{
	public interface IAppService<T> where T : class
	{
		IEnumerable<T> Get();
		IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
		Task<IEnumerable<T>> GetAsync();
		Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
		void Save(T entity);
		Task SaveAsync(T entity);
		void Update(T entity);
		Task UpdateAsync(T entity);
		void Delete(T entity);
		Task DeleteAsync(T entity);		

	}
}
