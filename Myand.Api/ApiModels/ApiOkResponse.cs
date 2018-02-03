using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myand.Api.ApiModels
{
	public class ApiOkResponse : ApiResponse
	{
		public object Result { get; }
		public int TotalRows { get; }

		public ApiOkResponse(object result, int totalRows = 0)
			: base(200)
		{
			Result = result;
			TotalRows = totalRows;
		}
	}
}
