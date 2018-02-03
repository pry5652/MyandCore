using Myand.Commons.Domain;
using System;

namespace Myand.Domain
{
	public partial class MstUser : EntityBase
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool? Status { get; set; }
	}
}
