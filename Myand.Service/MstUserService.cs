using AutoMapper;
using Myand.Commons.Application;
using Myand.Commons.Infrastructure.Data;
using Myand.Commons.Securities;
using Myand.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myand.Service
{
	public class MstUserService : AppService<MstUser>,  IMstUserService
	{		
		public MstUserService(IGenericRepository<MstUser> repository) : base(repository)
		{
		}
		public IEnumerable<MstUser> GetAllActive()
		{			
			var users = _repository.Get(x => x.Status == true);
			return users;

		}
		
	}
}
