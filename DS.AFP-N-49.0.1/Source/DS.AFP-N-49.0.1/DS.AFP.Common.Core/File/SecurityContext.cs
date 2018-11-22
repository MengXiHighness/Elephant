
using System;

namespace DS.AFP.Common.Core
{
	
	public abstract class SecurityContext
	{
		
		public abstract IDisposable Impersonate(object state);
	}
}
