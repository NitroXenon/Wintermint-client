// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Hybrid.MatchService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Browser;
using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using RiotGames.Platform.Messaging.Persistence;
using RiotGames.Platform.Statistics;
using RiotSpectate.Spectate;
using RtmpSharp.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient.Data;
using WintermintClient.JsApi;
using WintermintClient.Riot;
using WintermintData.Helpers.Matches;
using WintermintData.Matches;
using WintermintData.Riot.Account;

namespace WintermintClient.JsApi.Hybrid
{
  [MicroApiService("match", Preload = true)]
  public class MatchService : JsApiService
  {
    private static readonly EndOfGameMatchTransformer MatchTransformer = new EndOfGameMatchTransformer(ChampionNameData.NameToId);
    private readonly Dictionary<string, Match> lastMatches;

    public MatchService()
    {
      this.lastMatches = new Dictionary<string, Match>();
      JsApiService.AccountBag.AccountAdded += (EventHandler<RiotAccount>) ((sender, account) => account.MessageReceived += new EventHandler<MessageReceivedEventArgs>(this.OnMessageReceived));
      JsApiService.AccountBag.AccountRemoved += (EventHandler<RiotAccount>) ((sender, account) => account.MessageReceived -= new EventHandler<MessageReceivedEventArgs>(this.OnMessageReceived));
    }

    private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
      RiotAccount riotAccount = sender as RiotAccount;
      EndOfGameStats end = e.Body as EndOfGameStats;
      if (end != null)
      {
        Match match1 = MatchService.MatchTransformer.Transform(end, riotAccount.LastNonNullGame, riotAccount.RealmId);
        match1.AdditionalData = (object) new MatchService.GameRewards()
        {
          InfluencePoints = (int) end.IpEarned,
          Experience = (int) end.ExperienceEarned,
          RiotPoints = 0
        };
        this.lastMatches[match1.RealmId] = match1;
        MatchService.CacheMatch(match1);
        this.NotifyMatchCompleted(match1);
      }
      SimpleDialogMessage simpleDialogMessage = e.Body as SimpleDialogMessage;
      if (simpleDialogMessage == null || !(simpleDialogMessage.Type == "leagues"))
        return;
      Match match = this.lastMatches[riotAccount.RealmId];
      if (match == null)
        return;
      JObject jobject = Enumerable.FirstOrDefault<JObject>(Enumerable.Select<string, JObject>(Enumerable.OfType<string>((IEnumerable) simpleDialogMessage.Params), new Func<string, JObject>(JObject.Parse)), (Func<JObject, bool>) (x => (long) x["gameId"] == match.MatchId));
      if (jobject == null)
        return;
      MatchService.GameRewards gameRewards = match.AdditionalData as MatchService.GameRewards;
      if (gameRewards == null)
        return;
      gameRewards.LeaguePoints = (int) jobject["leaguePointsDelta"];
      this.NotifyMatchCompleted(match);
    }

    private void NotifyMatchCompleted(Match match)
    {
      JsApiService.Push("riot:match:end-of-game", (object) new
      {
        id = match.Id,
        realmId = match.RealmId,
        matchId = match.MatchId
      });
    }

    [MicroApiMethod("details")]
    public async Task<object> GetMatchDetails(object args, JsApiService.JsResponse progress, JsApiService.JsResponse result)
    {
      // ISSUE: reference to a compiler-generated field
      if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site9 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (MatchService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site9.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site9;
      // ISSUE: reference to a compiler-generated field
      if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realmId", typeof (MatchService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea.Target((CallSite) MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea, args);
      string realmId = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb = CallSite<Func<CallSite, object, long>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (long), typeof (MatchService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, long> func2 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, long>> callSite2 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb;
      // ISSUE: reference to a compiler-generated field
      if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "matchId", typeof (MatchService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec.Target((CallSite) MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec, args);
      long matchId = func2((CallSite) callSite2, obj2);
      // ISSUE: reference to a compiler-generated field
      if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sited == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sited = CallSite<Func<CallSite, object, JObject>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (JObject), typeof (MatchService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, JObject> func3 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sited.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, JObject>> callSite3 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sited;
      // ISSUE: reference to a compiler-generated field
      if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "fill", typeof (MatchService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee.Target((CallSite) MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee, args);
      JObject fill = func3((CallSite) callSite3, obj3);
      Match clrMatch = await this.GetMatchDetailsInternal(realmId, matchId, (Action<string>) (source => progress((object) source)));
      object obj4;
      if (clrMatch == null)
      {
        obj4 = (object) null;
      }
      else
      {
        if (fill != null)
        {
          JToken jtoken = fill["mapId"];
          if (clrMatch.MapId <= 0 && jtoken != null)
            clrMatch.MapId = (int) jtoken;
          string str = (string) fill["queue"];
          if (string.IsNullOrEmpty(clrMatch.Queue) && !string.IsNullOrEmpty(str))
            clrMatch.Queue = str;
        }
        JObject jObject = JObject.FromObject((object) clrMatch, SerializerSettings.JsonSerializer);
        object match = (object) jObject;
        MatchService.GameRewards gameRewards = clrMatch.AdditionalData as MatchService.GameRewards;
        if (gameRewards != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitef == null)
          {
            // ISSUE: reference to a compiler-generated field
            MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitef = CallSite<Func<CallSite, object, JObject, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "rewards", typeof (MatchService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj5 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitef.Target((CallSite) MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Sitef, match, JObject.FromObject((object) gameRewards, SerializerSettings.JsonSerializer));
          jObject.Remove("additionalData");
        }
        double matchLength = clrMatch.Length.TotalMinutes;
        int teamCount = clrMatch.Teams.Length;
        for (int index = 0; index < teamCount; ++index)
        {
          // ISSUE: reference to a compiler-generated field
          if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site10 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site10 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (MatchService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, int, object> func4 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site10.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, int, object>> callSite4 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site10;
          // ISSUE: reference to a compiler-generated field
          if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site11 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "teams", typeof (MatchService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj5 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site11.Target((CallSite) MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site11, match);
          int num1 = index;
          object obj6 = func4((CallSite) callSite4, obj5, num1);
          // ISSUE: reference to a compiler-generated field
          if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site12 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site12 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "index", typeof (MatchService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj7 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site12.Target((CallSite) MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site12, obj6, index);
          // ISSUE: reference to a compiler-generated field
          if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site13 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site13 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (MatchService)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, IEnumerable> func5 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site13.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, IEnumerable>> callSite5 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site13;
          // ISSUE: reference to a compiler-generated field
          if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site14 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site14 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "members", typeof (MatchService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj8 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site14.Target((CallSite) MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site14, obj6);
          foreach (object obj9 in func5((CallSite) callSite5, obj8))
          {
            // ISSUE: reference to a compiler-generated field
            if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site15 == null)
            {
              // ISSUE: reference to a compiler-generated field
              MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site15 = CallSite<Func<CallSite, object, double, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "goldPerMinute", typeof (MatchService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, double, object> func6 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site15.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, double, object>> callSite6 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site15;
            object obj10 = obj9;
            // ISSUE: reference to a compiler-generated field
            if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site16 == null)
            {
              // ISSUE: reference to a compiler-generated field
              MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site16 = CallSite<Func<CallSite, object, double>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (double), typeof (MatchService)));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, double> func7 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site16.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, double>> callSite7 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site16;
            // ISSUE: reference to a compiler-generated field
            if (MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site17 == null)
            {
              // ISSUE: reference to a compiler-generated field
              MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site17 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "gold", typeof (MatchService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj11 = MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site17.Target((CallSite) MatchService.\u003CGetMatchDetails\u003Eo__SiteContainer8.\u003C\u003Ep__Site17, obj9);
            double num2 = func7((CallSite) callSite7, obj11) / matchLength;
            object obj12 = func6((CallSite) callSite6, obj10, num2);
          }
        }
        obj4 = match;
      }
      return obj4;
    }

    public async Task<Match> GetMatchDetailsInternal(string realmId, long matchId, Action<string> source)
    {
      RiotAccount account = JsApiService.AccountBag.Get(realmId);
      source("local");
      Match match;
      Match match1;
      if (this.lastMatches.TryGetValue(realmId, out match) && match.MatchId == matchId)
      {
        match1 = match;
      }
      else
      {
        source("cache");
        string cacheKey = MatchService.GetMatchCacheKey(realmId, matchId);
        if (JsApiService.Cache.Get(cacheKey) != null)
        {
          match1 = (Match) JsApiService.Cache.Get(cacheKey);
        }
        else
        {
          source("spectator");
          Match spectator = await this.GetSpectatorMatchAsync(account, matchId);
          if (spectator != null)
          {
            JsApiService.Cache.Set(cacheKey, (object) spectator);
            match1 = spectator;
          }
          else
          {
            source("replay");
            Match replay = await this.GetReplayMatchAsync(account, matchId);
            if (replay != null)
            {
              JsApiService.Cache.Set(cacheKey, (object) replay);
              match1 = replay;
            }
            else
            {
              source("wintermint");
              Match wintermint = await this.GetWintermintMatchAsync(account, matchId);
              if (wintermint != null)
              {
                JsApiService.Cache.Set(cacheKey, (object) wintermint);
                match1 = wintermint;
              }
              else
                match1 = (Match) null;
            }
          }
        }
      }
      return match1;
    }

    private async Task<Match> GetSpectatorMatchAsync(RiotAccount account, long matchId)
    {
      Match match;
      try
      {
        ReplayConfig service = account.Endpoints.Replay;
        SpectateClient client = new SpectateClient(account.RealmId, service.PlatformId, ChampionNameData.NameToId, service.SpectateUri);
        match = await client.GetMatchOutcomeAsync(matchId);
      }
      catch
      {
        match = (Match) null;
      }
      return match;
    }

    private Task<Match> GetReplayMatchAsync(RiotAccount account, long matchId)
    {
      return Task.FromResult<Match>((Match) null);
    }

    private Task<Match> GetWintermintMatchAsync(RiotAccount account, long matchId)
    {
      return Task.FromResult<Match>((Match) null);
    }

    private static string GetMatchCacheKey(string realmId, long matchId)
    {
      return string.Format("game#{0}:{1}", (object) realmId, (object) matchId);
    }

    private static void CacheMatch(Match match)
    {
      JsApiService.Cache.SetCustom(MatchService.GetMatchCacheKey(match.RealmId, match.MatchId), (object) match, new CacheItemPolicy()
      {
        SlidingExpiration = TimeSpan.FromHours(1.0)
      });
    }

    private class GameRewards
    {
      public int InfluencePoints;
      public int RiotPoints;
      public int LeaguePoints;
      public int Experience;
    }
  }
}
