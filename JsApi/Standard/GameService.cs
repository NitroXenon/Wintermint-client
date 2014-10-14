// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.GameService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using RiotGames.Platform.Game;
using RiotGames.Platform.Game.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Notification;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("game")]
  public class GameService : JsApiService
  {
    private static readonly Regex MapDisplayNameTransformer = new Regex("^(?:The )?(.*?)!?$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static async Task<GameService.JsGameMap[]> GetMaps(RiotAccount account)
    {
      GameMap[] maps = await account.InvokeCachedAsync<GameMap[]>("gameMapService", "getGameMapList", TimeSpan.FromDays(1.0));
      return Enumerable.ToArray<GameService.JsGameMap>((IEnumerable<GameService.JsGameMap>) Enumerable.OrderBy<GameService.JsGameMap, string>(Enumerable.Select<GameMap, GameService.JsGameMap>(Enumerable.Where<GameMap>((IEnumerable<GameMap>) maps, (Func<GameMap, bool>) (x => x.MapId != 4)), (Func<GameMap, GameService.JsGameMap>) (x => new GameService.JsGameMap()
      {
        Id = x.MapId,
        Name = GameService.TransformMapDisplayName(x.DisplayName),
        Players = x.TotalPlayers
      })), (Func<GameService.JsGameMap, string>) (x => x.Name)));
    }

    [MicroApiMethod("getMaps")]
    public Task<GameService.JsGameMap[]> ClientGetMaps()
    {
      return GameService.GetMaps(JsApiService.RiotAccount);
    }

    private static string TransformMapDisplayName(string mapDisplayName)
    {
      return GameService.MapDisplayNameTransformer.Match(mapDisplayName).Groups[1].Value.Trim();
    }

    [MicroApiMethod("getGameTypeConfigs")]
    public object GetGameTypeConfigs()
    {
      Func<GameTypeConfigDTO, object> selector = (Func<GameTypeConfigDTO, object>) (x => (object) new
      {
        Id = x.Id,
        Name = x.Name,
        TradesEnabled = ((x.AllowTrades ? 1 : 0) != 0),
        Timers = new double[3]
        {
          x.BanTimerDuration,
          x.MainPickTimerDuration,
          x.PostPickTimerDuration
        }
      });
      return (object) new
      {
        All = Enumerable.Select<GameTypeConfigDTO, object>((IEnumerable<GameTypeConfigDTO>) JsApiService.RiotAccount.GameTypeConfigs, selector),
        Practice = Enumerable.Select<GameTypeConfigDTO, object>((IEnumerable<GameTypeConfigDTO>) JsApiService.RiotAccount.PracticeGameTypeConfigs, selector)
      };
    }

    [MicroApiMethod("createTutorial")]
    public async Task CreateTutorialGame(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (GameService.\u003CCreateTutorialGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee == null)
      {
        // ISSUE: reference to a compiler-generated field
        GameService.\u003CCreateTutorialGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (bool), typeof (GameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func = GameService.\u003CCreateTutorialGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite = GameService.\u003CCreateTutorialGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee;
      // ISSUE: reference to a compiler-generated field
      if (GameService.\u003CCreateTutorialGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef == null)
      {
        // ISSUE: reference to a compiler-generated field
        GameService.\u003CCreateTutorialGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "basic", typeof (GameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = GameService.\u003CCreateTutorialGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef.Target((CallSite) GameService.\u003CCreateTutorialGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef, args);
      int arg = func((CallSite) callSite, obj1) ? 0 : 1;
      object obj2 = await JsApiService.RiotAccount.InvokeAsync<object>("gameService", "createTutorialGame", (object) arg);
    }

    [MicroApiMethod("unspectate")]
    public async Task DeclineObserverReconnect()
    {
      object obj = await JsApiService.RiotAccount.InvokeAsync<object>("gameService", "declineObserverReconnect");
    }

    [MicroApiMethod("reconnect")]
    public async Task Reconnect()
    {
      PlatformGameLifecycleDTO lifeCycle = await JsApiService.RiotAccount.InvokeAsync<PlatformGameLifecycleDTO>("gameService", "retrieveInProgressGameInfo");
      if (lifeCycle != null && lifeCycle.PlayerCredentials != null)
        GameMaestroService.StartGame(JsApiService.RiotAccount.RealmId, lifeCycle.PlayerCredentials);
    }

    [Serializable]
    public class JsGameMap
    {
      public int Id;
      public string Name;
      public int Players;
    }
  }
}
