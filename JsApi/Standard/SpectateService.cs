// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.SpectateService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using RiotGames.Platform.Game;
using RiotGames.Platform.Summoner;
using RiotSpectate.Spectate;
using RiotSpectate.Spectate.RiotDto;
using RtmpSharp.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient.Data;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Helpers;
using WintermintClient.JsApi.Notification;
using WintermintClient.Riot;
using WintermintData.Helpers.Matches;
using WintermintData.Matches;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("spectate")]
  public class SpectateService : JsApiService
  {
    public static Task<PlatformGameLifecycleDTO> GetSpectatorGameThrowable(string realmId, string summonerName)
    {
      return JsApiService.AccountBag.Get(realmId).InvokeCachedAsync<PlatformGameLifecycleDTO>("gameService", "retrieveInProgressSpectatorGameInfo", (object) summonerName, TimeSpan.FromSeconds(5.0));
    }

    public static async Task<PlatformGameLifecycleDTO> GetSpectatorGame(string realmId, string summonerName)
    {
      PlatformGameLifecycleDTO gameLifecycleDto;
      try
      {
        gameLifecycleDto = await SpectateService.GetSpectatorGameThrowable(realmId, summonerName);
        goto label_5;
      }
      catch
      {
      }
      gameLifecycleDto = (PlatformGameLifecycleDTO) null;
label_5:
      return gameLifecycleDto;
    }

    [MicroApiMethod("spectate")]
    public async Task SpectateGame(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site4 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (SpectateService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site4;
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (SpectateService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site5.Target((CallSite) SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site5, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site6 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (SpectateService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site6;
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (SpectateService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site7.Target((CallSite) SpectateService.\u003CSpectateGame\u003Eo__SiteContainer3.\u003C\u003Ep__Site7, args);
      string summonerName = func2((CallSite) callSite2, obj2);
      RiotAccount account = JsApiService.AccountBag.Get(realm);
      PlatformGameLifecycleDTO game = await SpectateService.GetSpectatorGame(realm, summonerName);
      if (game == null)
        throw new JsApiException("no-game");
      int num = await GameMaestroService.TryStartSpectatorGame(realm, account.Endpoints.Replay.PlatformId, game.PlayerCredentials) ? 1 : 0;
    }

    [MicroApiMethod("find")]
    public async Task<SpectateService.JsSpectatorThing> FindGame(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site11 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (SpectateService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site11.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site11;
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (SpectateService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site12.Target((CallSite) SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site12, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site13 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site13 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (SpectateService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site13.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site13;
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site14 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site14 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (SpectateService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site14.Target((CallSite) SpectateService.\u003CFindGame\u003Eo__SiteContainer10.\u003C\u003Ep__Site14, args);
      string summonerName = func2((CallSite) callSite2, obj2);
      PublicSummoner summoner = await JsApiService.GetSummoner(realm, summonerName);
      SpectateService.JsSpectatorThing jsSpectatorThing;
      if (summoner != null)
      {
        try
        {
          RiotAccount account = JsApiService.AccountBag.Get(realm);
          PlatformGameLifecycleDTO game = await SpectateService.GetSpectatorGameThrowable(realm, summonerName);
          jsSpectatorThing = new SpectateService.JsSpectatorThing("in-game", game.Game, account.RealmId, summoner.SummonerId, TimeSpan.FromSeconds((double) game.ReconnectDelay));
        }
        catch (InvocationException ex)
        {
          string str = (ex.FaultString ?? "").ToLowerInvariant();
          jsSpectatorThing = !str.Contains("not started") ? (!str.Contains("not observable") ? new SpectateService.JsSpectatorThing("out-of-game") : new SpectateService.JsSpectatorThing("observer-disabled")) : new SpectateService.JsSpectatorThing("game-assigned");
        }
        catch
        {
          jsSpectatorThing = new SpectateService.JsSpectatorThing("error");
        }
      }
      else
        jsSpectatorThing = new SpectateService.JsSpectatorThing("no-summoner");
      return jsSpectatorThing;
    }

    [MicroApiMethod("findMore")]
    public async Task<object> FindMore(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (SpectateService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f;
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (SpectateService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20.Target((CallSite) SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21 = CallSite<Func<CallSite, object, long>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (long), typeof (SpectateService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, long> func2 = SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, long>> callSite2 = SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21;
      // ISSUE: reference to a compiler-generated field
      if (SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "matchId", typeof (SpectateService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22.Target((CallSite) SpectateService.\u003CFindMore\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22, args);
      long matchId = func2((CallSite) callSite2, obj2);
      RiotAccount account = JsApiService.AccountBag.Get(realm);
      SpectateClient client = new SpectateClient(account.RealmId, account.Endpoints.Replay.PlatformId, ChampionNameData.NameToId, account.Endpoints.Replay.SpectateUri);
      GameDescription description = await client.GetGameDescriptionAsync(matchId);
      return (object) new
      {
        Started = description.StartTime,
        Featured = description.FeaturedGame,
        Elo = description.InterestScore,
        Ended = description.GameEnded
      };
    }

    public class JsSpectatorThing
    {
      public object Status;
      public object Game;
      public object Dude;
      public object StandardSpectateBegins;

      public JsSpectatorThing(string status)
      {
        this.Status = (object) status;
      }

      public JsSpectatorThing(string status, GameDTO game, string realmId, long summonerId, TimeSpan observerDelay)
      {
        Match match = InGameMatchTransformer.Transform(game, realmId);
        this.Status = (object) status;
        this.Game = (object) match;
        this.Dude = (object) Enumerable.First<TeamMember>(Enumerable.SelectMany<Team, TeamMember>((IEnumerable<Team>) match.Teams, (Func<Team, IEnumerable<TeamMember>>) (x => (IEnumerable<TeamMember>) x.Members)), (Func<TeamMember, bool>) (x =>
        {
          long? nullable = x.SummonerId;
          long num = summonerId;
          if (nullable.GetValueOrDefault() == num)
            return nullable.HasValue;
          else
            return false;
        }));
        this.StandardSpectateBegins = (object) (DateTime.UtcNow + observerDelay);
      }
    }
  }
}
