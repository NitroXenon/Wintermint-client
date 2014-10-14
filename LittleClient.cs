// Decompiled with JetBrains decompiler
// Type: WintermintClient.LittleClient
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Astral.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WintermintData.Account;
using WintermintData.Protocol;

namespace WintermintClient
{
  internal class LittleClient
  {
    private static readonly TimeSpan DefaultCacheTime = TimeSpan.FromMinutes(5.0);
    private static readonly MemoryCache Cache = new MemoryCache("LittleClientInvokeCache", new NameValueCollection()
    {
      {
        "CacheMemoryLimitMegabytes",
        "10"
      },
      {
        "PhysicalMemoryLimitPercentage",
        "10"
      }
    });
    public AccountDetails Account;
    private AstralClient client;

    public bool Connected
    {
      get
      {
        if (this.client != null)
          return this.client.State == AstralClientState.Connected;
        else
          return false;
      }
    }

    public event EventHandler Disconnected;

    public async Task AuthenticateAsync(string email, string password)
    {
      if (!this.Connected)
      {
        this.ReconstructClient();
        await this.client.ConnectAsync();
      }
      await this.NegotiateProtocolAsync();
      AccountDetails account = await this.client.InvokeAsync<AccountDetails>("account.login", new object[2]
      {
        (object) email,
        (object) password
      });
      this.Account = account;
    }

    private async Task NegotiateProtocolAsync()
    {
      NegotiationResponse response = await this.client.InvokeAsync<NegotiationResponse>("protocol.negotiate", (object) new NegotiationRequest()
      {
        Protocols = new long[1]
        {
          -9999997L
        }
      });
      if (response.ReassignedServer != (Uri) null)
        throw new NotSupportedException("LittleClient (branch: master) does not support server re-assignment.");
      if (response.Protocol != -9999997L)
        throw new ProtocolVersionNegotiationException(response.Protocol > -9999997L);
    }

    public int Subscribe(string topic, AstralMessageHandler handler)
    {
      return this.client.Subscribe(topic, handler);
    }

    public int Subscribe(Regex topic, AstralMessageHandler handler)
    {
      return this.client.Subscribe(topic, handler);
    }

    public void Unsubscribe(string topic)
    {
      this.client.Unsubscribe(topic);
    }

    public void Unsubscribe(AstralMessageHandler handler)
    {
      this.client.Unsubscribe(handler);
    }

    public void Unsubscribe(int subscriptionId)
    {
      this.client.Unsubscribe(subscriptionId);
    }

    public Task Invoke(string method)
    {
      return (Task) this.Invoke<object>(method);
    }

    public Task Invoke(string method, object argument)
    {
      return (Task) this.Invoke<object>(method, argument);
    }

    public Task Invoke(string method, object[] arguments)
    {
      return (Task) this.Invoke<object>(method, arguments);
    }

    public Task<T> Invoke<T>(string method)
    {
      return this.Invoke<T>(method, new object[0]);
    }

    public Task<T> Invoke<T>(string method, object argument)
    {
      return this.Invoke<T>(method, new object[1]
      {
        argument
      });
    }

    public Task<T> Invoke<T>(string method, object[] arguments)
    {
      return this.client.InvokeAsync<T>(method, arguments);
    }

    public Task<T> InvokeCached<T>(string method)
    {
      return this.InvokeCached<T>(method, new object[0]);
    }

    public Task<T> InvokeCached<T>(string method, object argument)
    {
      return this.InvokeCached<T>(method, new object[1]
      {
        argument
      });
    }

    public Task<T> InvokeCached<T>(string method, object[] arguments)
    {
      return this.InvokeCached<T>(method, arguments, LittleClient.DefaultCacheTime);
    }

    public Task<T> InvokeCached<T>(string method, TimeSpan life)
    {
      return this.InvokeCached<T>(method, new object[0], life);
    }

    public Task<T> InvokeCached<T>(string method, object argument, TimeSpan life)
    {
      return this.InvokeCached<T>(method, new object[1]
      {
        argument
      }, life);
    }

    public Task<T> InvokeCached<T>(string method, object[] arguments, TimeSpan life)
    {
      string cacheKey = LittleClient.GetCacheKey<T>(method, arguments);
      lock (LittleClient.Cache)
      {
        Task<T> local_1 = (Task<T>) LittleClient.Cache.Get(cacheKey, (string) null);
        if (local_1 != null && (!local_1.IsCompleted || local_1.Status == TaskStatus.RanToCompletion))
          return local_1;
        Task<T> local_1_1 = this.Invoke<T>(method, arguments);
        ((ObjectCache) LittleClient.Cache).Set(cacheKey, (object) local_1_1, DateTimeOffset.UtcNow + life, (string) null);
        return local_1_1;
      }
    }

    public void Purge<T>(string method)
    {
      this.Purge<T>(method, new object[0]);
    }

    public void Purge<T>(string method, object argument)
    {
      this.Purge<T>(method, new object[1]
      {
        argument
      });
    }

    public void Purge<T>(string method, object[] arguments)
    {
      string cacheKey = LittleClient.GetCacheKey<T>(method, arguments);
      LittleClient.Cache.Remove(cacheKey, (string) null);
    }

    private static string GetCacheKey<T>(string method, object[] arguments)
    {
      return string.Format("{0}>{1}/{2}/{3}/{4}", (object) typeof (T).FullName, (object) arguments, (object) method, (object) arguments.Length, (object) LittleClient.JoinArguments(arguments));
    }

    private static string JoinArguments(object[] arguments)
    {
      return string.Join("`", Enumerable.Select<object, string>((IEnumerable<object>) arguments, (Func<object, string>) (x =>
      {
        if (!(x is Array) && !(x is IList))
          return x.ToString();
        else
          return string.Format("[{0}]", (object) string.Join("!", Enumerable.ToArray<object>(Enumerable.Cast<object>((IEnumerable) x))));
      })));
    }

    private void ReconstructClient()
    {
      this.client = new AstralClient("ws://1.api.client.wintermint.net:81");
      this.client.Disconnected += new EventHandler(this.OnDisconnected);
    }

    private void OnDisconnected(object sender, EventArgs e)
    {
      if (this.Disconnected == null)
        return;
      this.Disconnected((object) this, e);
    }
  }
}
