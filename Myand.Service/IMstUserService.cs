using Myand.Commons.Application;
using Myand.Domain;
using System;
using System.Collections.Generic;

namespace Myand.Service
{
    public interface IMstUserService : IAppService<MstUser>
	{
		IEnumerable<MstUser> GetAllActive();
	}
}
