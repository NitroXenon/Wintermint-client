// Decompiled with JetBrains decompiler
// Type: WintermintClient.Riot.RiotAccount
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Complete.Async;
using RiotGames.Platform.Clientfacade.Domain;
using RiotGames.Platform.Game;
using RiotGames.Platform.Messaging;
using RiotGames.Platform.Summoner;
using RiotSharp;
using RtmpSharp.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;
using WintermintClient;
using WintermintClient.Data;
using WintermintData.Riot.Account;

namespace WintermintClient.Riot
{
  public class RiotAccount
  {
    public readonly int Handle = Interlocked.Increment(ref RiotAccount.handleCounter);
    private readonly AsyncLock reconnectLock = new AsyncLock();
    private const int kDefaultCacheLifeSeconds = 300;
    private static int handleCounter;
    private static readonly MemoryCache Cache;
    public bool CanConnect;
    public GameDTO Game_;
    public string PlatformId;
    private volatile ConnectionState state;
    private int pendingInvocations;
    private readonly UberChatClient chat;
    private readonly RiotRtmpClient rtmp;
    private readonly RollingList<DateTime> reconnectAttempts;
    private int pendingReconnects;
    private static int debugFlushCounter;

    public static TimeSpan CachedTtl { get; set; }

    public string Username { get; private set; }

    public string Password { get; private set; }

    public long AccountId { get; private set; }

    public long SummonerId { get; private set; }

    public string SummonerInternalName { get; private set; }

    public string SummonerName { get; private set; }

    public string RealmId { get; private set; }

    public string RealmName { get; private set; }

    public string RealmFullName { get; private set; }

    public Endpoints Endpoints { get; private set; }

    public UberChatClient Chat
    {
      get
      {
        return this.chat;
      }
    }

    public List<GameTypeConfigDTO> GameTypeConfigs { get; private set; }

    public List<GameTypeConfigDTO> PracticeGameTypeConfigs { get; private set; }

    public GameDTO LastNonNullGame { get; private set; }

    public GameDTO Game
    {
      get
      {
        return this.Game_;
      }
      set
      {
        if (this.Game_ != null)
          this.LastNonNullGame = this.Game_;
        if (this.LastNonNullGame == null)
          this.LastNonNullGame = value;
        this.Game_ = value;
      }
    }

    public Dictionary<string, Func<string>> Blockers { get; private set; }

    public Dictionary<string, object> Storage { get; private set; }

    public bool IsBlocked
    {
      get
      {
        if (this.state == ConnectionState.Connected)
          return !Enumerable.All<string>(Enumerable.Select<Func<string>, string>((IEnumerable<Func<string>>) this.Blockers.Values, (Func<Func<string>, string>) (x => x())), new Func<string, bool>(string.IsNullOrEmpty));
        else
          return false;
      }
    }

    public string ErrorReason { get; private set; }

    public int QueuePosition { get; private set; }

    public DateTime WaitingUntil { get; private set; }

    public ConnectionState State
    {
      get
      {
        return this.state;
      }
    }

    public int PendingInvocations
    {
      get
      {
        return this.pendingInvocations;
      }
    }

    public event EventHandler Connected;

    public event EventHandler Disconnected;

    public event EventHandler<StateChangedEventArgs> StateChanged;

    public event EventHandler<int> QueuePositionChanged;

    public event EventHandler<DateTime> WaitDelayChanged;

    public event EventHandler<MessageReceivedEventArgs> MessageReceived;

    public event EventHandler<InvocationResultEventArgs> InvocationResult;

    static RiotAccount()
    {
      RiotAccount.CachedTtl = TimeSpan.FromSeconds(300.0);
      RiotAccount.Cache = new MemoryCache("riot-account-invocation-cache", new NameValueCollection()
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
    }

    public RiotAccount(AccountConfig config)
    {
      this.CanConnect = true;
      this.Blockers = new Dictionary<string, Func<string>>();
      this.Storage = new Dictionary<string, object>();
      this.RealmId = config.RealmId;
      this.RealmName = config.RealmName;
      this.RealmFullName = config.RealmFullName;
      this.Username = config.Username;
      this.Password = config.Password;
      this.Endpoints = config.Endpoints;
      this.rtmp = new RiotRtmpClient(RtmpSharpData.SerializationContext, config.Endpoints.Rtmp.Uri, config.Username, config.Password, config.Endpoints.Rtmp.AuthDomain, config.Endpoints.Rtmp.ServiceEndpoint, config.Endpoints.Rtmp.Versions, config.Endpoints.Rtmp.LoginQueueUri);
      this.rtmp.Disconnected += (EventHandler) ((sender, e) => this.OnDisconnected(e));
      this.rtmp.LoginQueuePositionChanged += (EventHandler<int>) ((sender, i) => this.OnLoginQueuePositionChanged(i));
      this.rtmp.MessageReceived += (EventHandler<MessageReceivedEventArgs>) ((sender, e) => this.OnMessageReceived(e));
      this.chat = ChatHost.Create(config, this);
      this.chat.Disconnected += new EventHandler(this.OnChatDisconnected);
      this.reconnectAttempts = new RollingList<DateTime>(15);
    }

    public async Task ConnectAsync()
    {
      lock (this)
      {
        if (this.pendingReconnects <= 1)
          ++this.pendingReconnects;
        else
          goto label_23;
      }
      try
      {
        using (this.reconnectLock.LockAsync())
        {
          if (this.CanConnect)
          {
            try
            {
              if (this.state != ConnectionState.Disconnected && this.state != ConnectionState.Error)
              {
                if (this.state != ConnectionState.Waiting)
                  goto label_23;
              }
              this.UpdateState(ConnectionState.Connecting);
              this.rtmp.Reset();
              await this.rtmp.Connect();
              this.chat.Connect();
              LoginDataPacket login = await this.rtmp.Invoke<LoginDataPacket>("clientFacadeService", "getLoginDataPacketForUser");
              if (login.AllSummonerData != null && login.AllSummonerData.Summoner != null)
              {
                Summoner summoner = login.AllSummonerData.Summoner;
                this.SummonerName = summoner.Name;
                this.SummonerInternalName = summoner.InternalName;
                this.SummonerId = (long) summoner.SumId;
                this.AccountId = summoner.AccountId;
              }
              this.GameTypeConfigs = login.GameTypeConfigs;
              this.PracticeGameTypeConfigs = Enumerable.ToList<GameTypeConfigDTO>(Enumerable.Where<GameTypeConfigDTO>(Enumerable.Select<int, GameTypeConfigDTO>((IEnumerable<int>) login.ClientSystemStates.PracticeGameTypeConfigIdList, (Func<int, GameTypeConfigDTO>) (id => Enumerable.FirstOrDefault<GameTypeConfigDTO>((IEnumerable<GameTypeConfigDTO>) this.GameTypeConfigs, (Func<GameTypeConfigDTO, bool>) (config => config.Id == (double) id)))), (Func<GameTypeConfigDTO, bool>) (x => x != null)));
              this.UpdateState(ConnectionState.Connected);
              this.OnConnected();
            }
            catch (LoginException ex)
            {
              this.ErrorReason = ex.Reason;
              this.UpdateState(ex.Type == LoginException.ResponseType.Failed ? ConnectionState.Error : ConnectionState.Disconnected);
            }
            catch (Exception ex)
            {
              this.UpdateState(ConnectionState.Disconnected);
            }
          }
        }
      }
      catch
      {
        lock (this)
          --this.pendingReconnects;
      }
label_23:;
    }

    public void Close()
    {
      try
      {
        this.chat.Close();
      }
      catch
      {
      }
      try
      {
        this.rtmp.Close();
      }
      catch
      {
      }
    }

    public Task ReconnectAsync()
    {
      return this.ConnectAsync();
    }

    public async Task ReconnectThrottledAsync()
    {
      this.UpdateState(ConnectionState.Waiting);
      TimeSpan delay = this.GetReconnectDelay();
      this.OnWaitDelayChanged(DateTime.UtcNow + delay);
      await Task.Delay(delay);
      this.reconnectAttempts.Push(DateTime.UtcNow);
      await this.ReconnectAsync();
    }

    private TimeSpan GetReconnectDelay()
    {
      Func<TimeSpan, int> func = (Func<TimeSpan, int>) (period => this.reconnectAttempts.Count((Func<DateTime, bool>) (x => DateTime.UtcNow - x <= period)));
      if (func(TimeSpan.FromMinutes(10.0)) > 10)
        return TimeSpan.FromSeconds(30.0);
      if (func(TimeSpan.FromMinutes(2.0)) > 0)
        return TimeSpan.FromSeconds(10.0);
      else
        return TimeSpan.Zero;
    }

    public Task<T> InvokeAsync<T>(string destination, string method)
    {
      return this.InvokeAsync<T>(destination, method, new object[0]);
    }

    public Task<T> InvokeAsync<T>(string destination, string method, object argument)
    {
      return this.InvokeAsync<T>(destination, method, new object[1]
      {
        argument
      });
    }

    public async Task<T> InvokeAsync<T>(string destination, string method, object[] arguments)
    {
      Interlocked.Increment(ref this.pendingInvocations);
      T result;
      try
      {
        result = await this.rtmp.Invoke<T>(destination, method, arguments);
      }
      catch (Exception ex)
      {
        this.OnInvocationResult(destination, method, false, (object) ex);
        throw;
      }
      finally
      {
        Interlocked.Decrement(ref this.pendingInvocations);
      }
      GameDTO game = (object) result as GameDTO;
      if (game != null)
        this.Game = game;
      this.OnInvocationResult(destination, method, true, (object) result);
      return result;
    }

    public Task<T> InvokeCachedAsync<T>(string destination, string method, TimeSpan life)
    {
      return this.InvokeCachedAsync<T>(destination, method, new object[0], life);
    }

    public Task<T> InvokeCachedAsync<T>(string destination, string method, object argument, TimeSpan life)
    {
      return this.InvokeCachedAsync<T>(destination, method, new object[1]
      {
        argument
      }, life);
    }

    public Task<T> InvokeCachedAsync<T>(string destination, string method, object[] arguments, TimeSpan life)
    {
      string realmId = this.RealmId;
      string cacheKey = this.GetCacheKey<T>(destination, method, arguments);
      lock (RiotAccount.Cache)
      {
        Task<T> local_1 = (Task<T>) RiotAccount.Cache.Get(cacheKey, (string) null);
        if (local_1 != null && (!local_1.IsCompleted || local_1.Status == TaskStatus.RanToCompletion))
          return local_1;
        Task<T> local_1_1 = this.InvokeAsync<T>(destination, method, arguments);
        ((ObjectCache) RiotAccount.Cache).Set(cacheKey, (object) local_1_1, DateTimeOffset.UtcNow + life, (string) null);
        return local_1_1;
      }
    }

    public Task<T> InvokeCachedAsync<T>(string destination, string method)
    {
      return this.InvokeCachedAsync<T>(destination, method, new object[0]);
    }

    public Task<T> InvokeCachedAsync<T>(string destination, string method, object argument)
    {
      return this.InvokeCachedAsync<T>(destination, method, new object[1]
      {
        argument
      });
    }

    public Task<T> InvokeCachedAsync<T>(string destination, string method, object[] arguments)
    {
      return this.InvokeCachedAsync<T>(destination, method, arguments, TimeSpan.FromSeconds(300.0));
    }

    public void RemoveCached<T>(string destination, string method)
    {
      this.RemoveCached<T>(destination, method, new object[0]);
    }

    public void RemoveCached<T>(string destination, string method, object argument)
    {
      this.RemoveCached<T>(destination, method, new object[1]
      {
        argument
      });
    }

    public void RemoveCached<T>(string destination, string method, object[] arguments)
    {
      string cacheKey = this.GetCacheKey<T>(destination, method, arguments);
      RiotAccount.Cache.Remove(cacheKey, (string) null);
    }

    private string GetCacheKey<T>(string destination, string method, object[] arguments)
    {
      return string.Format("{0}>{1}/{2}/{3}/{4}/{5}", (object) typeof (T).FullName, (object) this.RealmId, (object) destination, (object) method, (object) arguments.Length, (object) RiotAccount.JoinArguments(arguments));
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

    private void UpdateState(ConnectionState state)
    {
      ConnectionState oldState = this.state;
      if (oldState == ConnectionState.Error && state == ConnectionState.Disconnected)
        return;
      this.state = state;
      this.OnStateChanged(oldState, state);
    }

    private void OnInvocationResult(string service, string method, bool success, object result)
    {
      if (this.InvocationResult == null)
        return;
      this.InvocationResult((object) this, new InvocationResultEventArgs(service, method, success, result));
    }

    private void OnLoginQueuePositionChanged(int position)
    {
      this.QueuePosition = position;
      if (this.QueuePositionChanged == null)
        return;
      this.QueuePositionChanged((object) this, position);
    }

    private void OnWaitDelayChanged(DateTime waitingUntil)
    {
      this.WaitingUntil = waitingUntil;
      if (this.WaitDelayChanged == null)
        return;
      this.WaitDelayChanged((object) this, waitingUntil);
    }

    private void OnConnected()
    {
      this.UpdateState(ConnectionState.Connected);
      if (this.Connected == null)
        return;
      this.Connected((object) this, new EventArgs());
    }

    private void OnDisconnected(EventArgs e)
    {
      this.UpdateState(ConnectionState.Disconnected);
      if (this.Disconnected == null)
        return;
      this.Disconnected((object) this, e);
    }

    private void OnMessageReceived(MessageReceivedEventArgs args)
    {
      if (this.MessageReceived != null)
        this.MessageReceived((object) this, args);
      if (!(args.Body is ClientLoginKickNotification))
        return;
      this.ErrorReason = "login_invalidated";
      this.UpdateState(ConnectionState.Error);
      this.Close();
      this.ErrorReason = "login_invalidated";
      this.UpdateState(ConnectionState.Error);
    }

    private void OnStateChanged(ConnectionState oldState, ConnectionState newState)
    {
      if (this.StateChanged == null || oldState == newState)
        return;
      this.StateChanged((object) this, new StateChangedEventArgs(oldState, newState));
    }

    private async void OnChatDisconnected(object sender, EventArgs e)
    {
      if (this.CanConnect && string.IsNullOrEmpty(this.ErrorReason))
      {
        await Task.Delay(1000);
        this.chat.Connect();
      }
    }
  }
}
