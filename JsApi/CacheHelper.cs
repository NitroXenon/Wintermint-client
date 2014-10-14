// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.CacheHelper
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.Caching;

namespace WintermintClient.JsApi
{
  internal class CacheHelper
  {
    private const int kCacheMemoryLimitMegabytes = 10;
    private const int kPhysicalMemoryLimitPercentage = 5;
    private MemoryCache cache;
    private CacheItemPolicy defaultCachePolicy;
    private CacheItemPolicy fiveMinuteCachePolicy;
    private CacheItemPolicy permanentCachePolicy;

    public int ItemCount
    {
      get
      {
        return (int) this.cache.GetCount((string) null);
      }
    }

    public CacheHelper()
    {
      this.cache = new MemoryCache("WintermintJsApiCache", new NameValueCollection()
      {
        {
          "CacheMemoryLimitMegabytes",
          10.ToString((IFormatProvider) CultureInfo.InvariantCulture)
        },
        {
          "PhysicalMemoryLimitPercentage",
          5.ToString((IFormatProvider) CultureInfo.InvariantCulture)
        }
      });
      this.defaultCachePolicy = new CacheItemPolicy();
      this.fiveMinuteCachePolicy = new CacheItemPolicy()
      {
        SlidingExpiration = TimeSpan.FromMinutes(5.0)
      };
      this.permanentCachePolicy = new CacheItemPolicy()
      {
        Priority = CacheItemPriority.NotRemovable
      };
    }

    public T Get<T>(string key)
    {
      return (T) this.cache.Get(key, (string) null);
    }

    public object Get(string key)
    {
      return this.cache.Get(key, (string) null);
    }

    public void Set(string key, object value)
    {
      ((ObjectCache) this.cache).Set(key, value, this.defaultCachePolicy, (string) null);
    }

    public void SetExpiring(string key, object value)
    {
      ((ObjectCache) this.cache).Set(key, value, this.fiveMinuteCachePolicy, (string) null);
    }

    public void SetPermanent(string key, object value)
    {
      ((ObjectCache) this.cache).Set(key, value, this.permanentCachePolicy, (string) null);
    }

    public void SetCustom(string key, object value, CacheItemPolicy cachePolicy)
    {
      ((ObjectCache) this.cache).Set(key, value, cachePolicy, (string) null);
    }

    public object Remove(string key)
    {
      return this.cache.Remove(key, (string) null);
    }
  }
}
