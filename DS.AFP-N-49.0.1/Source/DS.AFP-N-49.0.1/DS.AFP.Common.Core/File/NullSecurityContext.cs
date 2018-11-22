
using System;

namespace DS.AFP.Common.Core
{
	
	public sealed class NullSecurityContext : SecurityContext
	{
		
		public static readonly NullSecurityContext Instance = new NullSecurityContext();

		
		private NullSecurityContext()
		{
		}

		
		public override IDisposable Impersonate(object state)
		{
			return null;
		}
	}
}
