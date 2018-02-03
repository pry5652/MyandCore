using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myand.Api.ApiModels
{
	public class ApiBadRequestResponse : ApiResponse
	{
		public int Code { get; set; }

		public ApiBadRequestResponse(int code)
			: base(400)
		{
			Code = code;
		}
	}
}
