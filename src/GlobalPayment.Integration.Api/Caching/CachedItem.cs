using System;

namespace GlobalPayment.Integration.Api.Caching;

public class CachedItem
{
    public object Value { get; set; }

    public DateTime CacheTime { get; set; }

    public TimeSpan TimeToLive { get; set; }

    public bool IsExpired()
    {
        return this.CacheTime.Add(TimeToLive) < DateTime.Now.ToUniversalTime();
    }
    
}