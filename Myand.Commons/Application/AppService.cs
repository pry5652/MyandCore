using Myand.Commons.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Myand.Commons.Application
{

	public class AppService<T> : IAppService<T> where T : class
	{
		protected readonly IGenericRepository<T> _repository;

		public AppService(IGenericRepository<T> repository)
		{
			_repository = repository;
		}

		public void Save(T entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			_repository.Add(entity);
			_repository.Save();
		}

		public async Task SaveAsync(T entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			_repository.Add(entity);
			await _repository.SaveAsync();			
		}
		public void Update(T entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			_repository.Update(entity);
			_repository.Save();
		}
		public async Task UpdateAsync(T entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			_repository.Update(entity);
			await _repository.SaveAsync();
		}
		public void Delete(T entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			_repository.Delete(entity);
			_repository.Save();
		}
		public async Task DeleteAsync(T entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			 _repository.Delete(entity);
			await _repository.SaveAsync();
		}

		public IEnumerable<T> Get()
		{
			return _repository.Get();
		}

		public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
		{
			return _repository.Get(predicate);
		}

		public async Task<IEnumerable<T>> GetAsync()
		{
			return await _repository.GetAsync();
		}

		public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
		{
			return await _repository.GetAsync(predicate);
		}
		
	}
}
