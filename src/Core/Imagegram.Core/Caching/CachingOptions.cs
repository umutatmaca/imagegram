using System;

namespace Imagegram.Core.Caching
{
    public class CachingOptions
    {
        /// <summary>
        /// amount of time realtive to now to force-clear the cache without any condition 
        /// </summary>
        public TimeSpan ExpirationPeriod { get; set; }

        /// <summary>
        /// amount of cache idle/not being used time. will be cleared after inactive period
        /// </summary>
        public TimeSpan InactivePeriod { get; set; }
    }
}
