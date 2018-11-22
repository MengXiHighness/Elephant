using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace DS.AFP.Common.Core.Cache
{
    public class CacheConfigure
    {
        public DateTimeOffset AbsoluteExpiration
        {
            get;
            set;
        }
        public TimeSpan? Duration
        {
            get;
            set;
        }
        public TimeSpan SlidingExpiration
        {
            get;
            set;
        }
        private CacheItemPriority priority = CacheItemPriority.Default;
        public CacheItemPriority Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }

       
    }
}
