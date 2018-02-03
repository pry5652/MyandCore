using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Myand.Commons.Infrastructure.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly IUnitOfWork _unitOfWork;
		public GenericRepository(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public void Add(T entity)
		{
			_unitOfWork.Context.Set<T>().Add(entity);
		}		
		public void Update(T entity)
		{
			_unitOfWork.Context.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(T entity)
		{
			_unitOfWork.Context.Set<T>().Remove(entity);
		}

		public IEnumerable<T> Get()
		{
			return _unitOfWork.Context.Set<T>().AsEnumerable<T>();
		}		

		public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
		{
			return _unitOfWork.Context.Set<T>().Where(predicate).AsEnumerable<T>();
		}

		public async Task<IEnumerable<T>> GetAsync()
		{
			return await _unitOfWork.Context.Set<T>().ToListAsync();
		}

		public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
		{
			return await _unitOfWork.Context.Set<T>().Where(predicate).ToListAsync();
		}

		public void Save()
		{
			_unitOfWork.Commit();
		}

		public async Task SaveAsync()
		{
			await _unitOfWork.CommitAsync();
		}

	}
}
