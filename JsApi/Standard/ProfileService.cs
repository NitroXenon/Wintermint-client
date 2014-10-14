// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.ProfileService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using RiotGames.Leagues.Pojo;
using RiotGames.Platform.Leagues.Client.Dto;
using RiotGames.Platform.Statistics;
using RiotGames.Platform.Summoner;
using RiotGames.Platform.Summoner.Runes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Helpers;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("profile")]
  public class ProfileService : JsApiService
  {
    private const string kMainRatedQueue = "RANKED_SOLO_5x5";

    private static string GetRatingTier(string tier)
    {
      if (string.IsNullOrEmpty(tier))
        return "UNRANKED";
      else
        return tier;
    }

    private async Task<object> GetPreviousRatings(string realm, long accountId)
    {
      RiotAccount account = JsApiService.AccountBag.Get(realm);
      AllPublicSummonerDataDTO publicSummonerData = await account.InvokeCachedAsync<AllPublicSummonerDataDTO>("summonerService", "getAllPublicSummonerDataByAccount", (object) accountId);
      BasePublicSummonerDTO summoner = publicSummonerData.Summoner;
      return (object) new \u003C\u003Ef__AnonymousType1a<int, string>[2]
      {
        new
        {
          Season = 2,
          Tier = ProfileService.GetRatingTier(summoner.SeasonTwoTier)
        },
        new
        {
          Season = 1,
          Tier = ProfileService.GetRatingTier(summoner.SeasonOneTier)
        }
      };
    }

    private async Task<ProfileService.JsLeague[]> GetLeagues(string realm, long summonerId)
    {
      RiotAccount account = JsApiService.AccountBag.Get(realm);
      SummonerLeaguesDTO leagues = await account.InvokeCachedAsync<SummonerLeaguesDTO>("leaguesServiceProxy", "getAllLeaguesForPlayer", (object) summonerId);
      IEnumerable<ProfileService.JsLeague> jsLeagues = Enumerable.Select(Enumerable.Select(Enumerable.Select((IEnumerable<LeagueListDTO>) leagues.SummonerLeagues, league => new
      {
        league = league,
        entries = league.Entries
      }), param0 => new
      {
        \u003C\u003Eh__TransparentIdentifier6 = param0,
        me = Enumerable.FirstOrDefault<LeagueItemDTO>((IEnumerable<LeagueItemDTO>) param0.entries, (Func<LeagueItemDTO, bool>) (x => x.PlayerOrTeamName == param0.league.RequestorsName))
      }), param0 => new ProfileService.JsLeague()
      {
        Id = string.Format("/{0}/{1}/", (object) param0.\u003C\u003Eh__TransparentIdentifier6.league.Queue, (object) param0.\u003C\u003Eh__TransparentIdentifier6.league.RequestorsName),
        Name = param0.\u003C\u003Eh__TransparentIdentifier6.league.RequestorsName,
        Queue = param0.\u003C\u003Eh__TransparentIdentifier6.league.Queue,
        League = GlobalExtensions.ToProperCase(param0.\u003C\u003Eh__TransparentIdentifier6.league.Tier),
        LeagueName = param0.\u003C\u003Eh__TransparentIdentifier6.league.Name,
        Division = param0.\u003C\u003Eh__TransparentIdentifier6.league.RequestorsRank,
        Divisions = Enumerable.Select<IGrouping<string, LeagueItemDTO>, ProfileService.JsLeagueDivision>(Enumerable.GroupBy<LeagueItemDTO, string>((IEnumerable<LeagueItemDTO>) param0.\u003C\u003Eh__TransparentIdentifier6.league.Entries, (Func<LeagueItemDTO, string>) (entry => entry.Rank)), (Func<IGrouping<string, LeagueItemDTO>, ProfileService.JsLeagueDivision>) (division => new ProfileService.JsLeagueDivision()
        {
          LeagueName = param0.\u003C\u003Eh__TransparentIdentifier6.league.Name,
          League = GlobalExtensions.ToProperCase(param0.\u003C\u003Eh__TransparentIdentifier6.league.Tier),
          Division = division.Key,
          Members = (IEnumerable<object>) Enumerable.Select((IEnumerable<LeagueItemDTO>) Enumerable.OrderByDescending<LeagueItemDTO, int>((IEnumerable<LeagueItemDTO>) division, (Func<LeagueItemDTO, int>) (x => x.LeaguePoints)), (entry, i) => new
          {
            Id = entry.PlayerOrTeamId,
            Position = i + 1,
            Name = entry.PlayerOrTeamName,
            Wins = entry.Wins,
            Losses = entry.Losses,
            Points = entry.LeaguePoints,
            Requestor = entry.PlayerOrTeamName == param0.\u003C\u003Eh__TransparentIdentifier6.league.RequestorsName
          })
        })),
        Points = param0.me.LeaguePoints,
        Wins = param0.me.Wins,
        Losses = param0.me.Losses
      });
      return Enumerable.ToArray<ProfileService.JsLeague>(jsLeagues);
    }

    private async Task<ProfileService.JsLeague[]> GetLeagueSummary(string realm, long summonerId)
    {
      ProfileService.JsLeague[] leagues = await this.GetLeagues(realm, summonerId);
      foreach (ProfileService.JsLeague jsLeague in leagues)
        jsLeague.Divisions = (IEnumerable<ProfileService.JsLeagueDivision>) null;
      return leagues;
    }

    private static async Task<object[]> GetRankedChampions(string realm, long accountId, string gameMode, string season)
    {
      RiotAccount account = JsApiService.AccountBag.Get(realm);
      AggregatedStats summoner = await account.InvokeCachedAsync<AggregatedStats>("playerStatsService", "getAggregatedStats", new object[3]
      {
        (object) accountId,
        (object) gameMode,
        (object) season
      });
      IEnumerable<\u003C\u003Ef__AnonymousType23<int, double, double, double, double, double, double, double, double>> statistics = Enumerable.Select(Enumerable.Where(Enumerable.Select(Enumerable.Select(Enumerable.Select(Enumerable.Select(Enumerable.Select(Enumerable.GroupBy<AggregatedStat, double>((IEnumerable<AggregatedStat>) summoner.LifetimeStatistics, (Func<AggregatedStat, double>) (x => x.ChampionId)), championStatistic => new
      {
        championStatistic = championStatistic,
        championId = (int) championStatistic.Key
      }), param0 => new
      {
        \u003C\u003Eh__TransparentIdentifier25 = param0,
        stats = Enumerable.ToDictionary<AggregatedStat, string, double>((IEnumerable<AggregatedStat>) param0.championStatistic, (Func<AggregatedStat, string>) (x => x.StatType), (Func<AggregatedStat, double>) (x => x.Value))
      }), param0 => new
      {
        \u003C\u003Eh__TransparentIdentifier26 = param0,
        games = JsApiService.GetGameStat(param0.stats, "TOTAL_SESSIONS_PLAYED")
      }), param0 => new
      {
        \u003C\u003Eh__TransparentIdentifier27 = param0,
        lifetimeGold = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier26.stats, "TOTAL_GOLD_EARNED")
      }), param0 => new
      {
        \u003C\u003Eh__TransparentIdentifier28 = param0,
        lifetimeCreeps = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier27.\u003C\u003Eh__TransparentIdentifier26.stats, "TOTAL_MINION_KILLS") + JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier27.\u003C\u003Eh__TransparentIdentifier26.stats, "TOTAL_NEUTRAL_MINIONS_KILLED")
      }), param0 =>
      {
        if (param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.games > 0.0)
          return param0.\u003C\u003Eh__TransparentIdentifier28.lifetimeGold > 0.0;
        else
          return false;
      }), param0 => new
      {
        ChampionId = param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.\u003C\u003Eh__TransparentIdentifier26.\u003C\u003Eh__TransparentIdentifier25.championId,
        Games = param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.games,
        Wins = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.\u003C\u003Eh__TransparentIdentifier26.stats, "TOTAL_SESSIONS_WON"),
        Losses = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.\u003C\u003Eh__TransparentIdentifier26.stats, "TOTAL_SESSIONS_LOST"),
        Kills = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.\u003C\u003Eh__TransparentIdentifier26.stats, "TOTAL_CHAMPION_KILLS") / param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.games,
        Deaths = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.\u003C\u003Eh__TransparentIdentifier26.stats, "TOTAL_DEATHS_PER_SESSION") / param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.games,
        Assists = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.\u003C\u003Eh__TransparentIdentifier26.stats, "TOTAL_ASSISTS") / param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.games,
        Gold = param0.\u003C\u003Eh__TransparentIdentifier28.lifetimeGold / param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.games,
        Creeps = param0.lifetimeCreeps / param0.\u003C\u003Eh__TransparentIdentifier28.\u003C\u003Eh__TransparentIdentifier27.games
      });
      return (object[]) Enumerable.ToArray(statistics);
    }

    private async Task<object> GetMatchHistory(string realm, long accountId)
    {
      RiotAccount account = JsApiService.AccountBag.Get(realm);
      GameService.JsGameMap[] maps = await GameService.GetMaps(account);
      RecentGames games = await account.InvokeCachedAsync<RecentGames>("playerStatsService", "getRecentGames", (object) accountId);
      return (object) Enumerable.Select(Enumerable.Select(Enumerable.Select(Enumerable.Select(Enumerable.Select(Enumerable.Select((IEnumerable<PlayerGameStats>) Enumerable.OrderByDescending<PlayerGameStats, DateTime>((IEnumerable<PlayerGameStats>) games.GameStatistics, (Func<PlayerGameStats, DateTime>) (game => game.CreateDate)), game => new
      {
        game = game,
        map = Enumerable.FirstOrDefault<GameService.JsGameMap>((IEnumerable<GameService.JsGameMap>) maps, (Func<GameService.JsGameMap, bool>) (m => (double) m.Id == game.GameMapId))
      }), param0 => new
      {
        \u003C\u003Eh__TransparentIdentifier44 = param0,
        stats = Enumerable.ToDictionary<RawStat, string, double>((IEnumerable<RawStat>) param0.game.Statistics, (Func<RawStat, string>) (x => x.StatType), (Func<RawStat, double>) (x => x.Value))
      }), param0 => new
      {
        \u003C\u003Eh__TransparentIdentifier45 = param0,
        win = JsApiService.GetGameStat(param0.stats, "WIN") != 0.0
      }), param0 => new
      {
        \u003C\u003Eh__TransparentIdentifier46 = param0,
        lose = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier45.stats, "LOSE") != 0.0
      }), param0 => new
      {
        \u003C\u003Eh__TransparentIdentifier47 = param0,
        outcome = param0.\u003C\u003Eh__TransparentIdentifier46.win ? "win" : (param0.lose ? "loss" : "afk")
      }), param0 => new
      {
        RealmId = realm,
        MatchId = param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.\u003C\u003Eh__TransparentIdentifier44.game.GameId,
        MapId = param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.\u003C\u003Eh__TransparentIdentifier44.map != null ? param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.\u003C\u003Eh__TransparentIdentifier44.map.Id : -1,
        Queue = param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.\u003C\u003Eh__TransparentIdentifier44.game.QueueType,
        ChampionId = param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.\u003C\u003Eh__TransparentIdentifier44.game.ChampionId,
        Started = param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.\u003C\u003Eh__TransparentIdentifier44.game.CreateDate,
        Duration = 0,
        Ip = param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.\u003C\u003Eh__TransparentIdentifier44.game.IpEarned,
        Outcome = param0.outcome,
        Kills = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "CHAMPIONS_KILLED"),
        Deaths = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "NUM_DEATHS"),
        Assists = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "ASSISTS"),
        MultiKill = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "LARGEST_MULTI_KILL"),
        Gold = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "GOLD_EARNED"),
        Creeps = JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "MINIONS_KILLED") + JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "NEUTRAL_MINIONS_KILLED"),
        Spells = new double[2]
        {
          param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.\u003C\u003Eh__TransparentIdentifier44.game.Spell1,
          param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.\u003C\u003Eh__TransparentIdentifier44.game.Spell2
        },
        Items = new double[6]
        {
          JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "ITEM0"),
          JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "ITEM1"),
          JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "ITEM2"),
          JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "ITEM3"),
          JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "ITEM4"),
          JsApiService.GetGameStat(param0.\u003C\u003Eh__TransparentIdentifier47.\u003C\u003Eh__TransparentIdentifier46.\u003C\u003Eh__TransparentIdentifier45.stats, "ITEM5")
        }
      });
    }

    [MicroApiMethod("getOverview")]
    public async Task GetOverview(object args, JsApiService.JsResponse onProgress, JsApiService.JsResponse onResult)
    {
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site65 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site65 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site65.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site65;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site66 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site66 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site66.Target((CallSite) ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site66, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site67 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site67 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site67.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site67;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site68 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site68 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site68.Target((CallSite) ProfileService.\u003CGetOverview\u003Eo__SiteContainer64.\u003C\u003Ep__Site68, args);
      string summonerName = func2((CallSite) callSite2, obj2);
      ConcurrentDictionary<string, object> dictionary = new ConcurrentDictionary<string, object>();
      Action publish = (Action) (() => onProgress((object) dictionary));
      dictionary["summonerName"] = (object) summonerName;
      publish();
      PublicSummoner summoner = await JsApiService.GetSummoner(realm, summonerName);
      if (summoner == null)
        throw new JsApiException("summoner-not-found");
      dictionary["summonerName"] = (object) summoner.Name;
      dictionary["summonerId"] = (object) summoner.SummonerId;
      dictionary["accountId"] = (object) summoner.AccountId;
      dictionary["level"] = (object) summoner.SummonerLevel;
      dictionary["iconId"] = (object) summoner.ProfileIconId;
      publish();
      await Task.WhenAll(this.GetPreviousRatings(realm, summoner.AccountId).ContinueWith(JsApiService.CreatePublisher<object>(dictionary, onProgress, "pastRatings")), this.GetLeagueSummary(realm, summoner.SummonerId).ContinueWith((Action<Task<ProfileService.JsLeague[]>>) (task =>
      {
        if (task.Status != TaskStatus.RanToCompletion)
          return;
        ProfileService.JsLeague[] result = task.Result;
        dictionary["currentRating"] = (object) (Enumerable.FirstOrDefault<ProfileService.JsLeague>((IEnumerable<ProfileService.JsLeague>) result, (Func<ProfileService.JsLeague, bool>) (x => x.Queue == "RANKED_SOLO_5x5")) ?? ProfileService.JsLeague.Unranked);
        dictionary["otherRatings"] = (object) Enumerable.Where<ProfileService.JsLeague>((IEnumerable<ProfileService.JsLeague>) result, (Func<ProfileService.JsLeague, bool>) (x => x.Queue != "RANKED_SOLO_5x5"));
        publish();
      })), this.GetMatchHistory(realm, summoner.AccountId).ContinueWith(JsApiService.CreatePublisher<object>(dictionary, onProgress, "games")), ProfileService.GetRankedChampions(realm, summoner.AccountId, "CLASSIC", "CURRENT").ContinueWith(JsApiService.CreatePublisher<object[]>(dictionary, onProgress, "champions")));
    }

    [MicroApiMethod("getMainLeague")]
    public async Task<object> GetMainLeague(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site79 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site79 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site79.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site79;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7a == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7a = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7a.Target((CallSite) ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7a, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7b == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7b = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7b.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7b;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7c == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7c = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7c.Target((CallSite) ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7c, args);
      string summonerName = func2((CallSite) callSite2, obj2);
      long summonerId;
      if (summonerName == null)
      {
        // ISSUE: reference to a compiler-generated field
        if (ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7d == null)
        {
          // ISSUE: reference to a compiler-generated field
          ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7d = CallSite<Func<CallSite, object, long>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (long), typeof (ProfileService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, long> func3 = ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7d.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, long>> callSite3 = ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7d;
        // ISSUE: reference to a compiler-generated field
        if (ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7e == null)
        {
          // ISSUE: reference to a compiler-generated field
          ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7e = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerId", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7e.Target((CallSite) ProfileService.\u003CGetMainLeague\u003Eo__SiteContainer78.\u003C\u003Ep__Site7e, args);
        summonerId = func3((CallSite) callSite3, obj3);
      }
      else
      {
        PublicSummoner summoner = await JsApiService.GetSummoner(realm, summonerName);
        summonerId = summoner.SummonerId;
      }
      ProfileService.JsLeague[] leagues = await this.GetLeagueSummary(realm, summonerId);
      return (object) (Enumerable.FirstOrDefault<ProfileService.JsLeague>((IEnumerable<ProfileService.JsLeague>) leagues, (Func<ProfileService.JsLeague, bool>) (x => x.Queue == "RANKED_SOLO_5x5")) ?? ProfileService.JsLeague.Unranked);
    }

    [MicroApiMethod("getGames")]
    public async Task<object> GetGames(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8b == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8b = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8b.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8b;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8c == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8c = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8c.Target((CallSite) ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8c, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8d == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8d = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8d.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8d;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8e == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8e = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8e.Target((CallSite) ProfileService.\u003CGetGames\u003Eo__SiteContainer8a.\u003C\u003Ep__Site8e, args);
      string summonerName = func2((CallSite) callSite2, obj2);
      PublicSummoner summoner = await JsApiService.GetSummoner(realm, summonerName);
      return await this.GetMatchHistory(realm, summoner.AccountId);
    }

    [MicroApiMethod("getLeagues")]
    public async Task<object> ClientGetLeagues(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site97 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site97 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site97.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site97;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site98 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site98 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site98.Target((CallSite) ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site98, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site99 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site99 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site99.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site99;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site9a == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site9a = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site9a.Target((CallSite) ProfileService.\u003CClientGetLeagues\u003Eo__SiteContainer96.\u003C\u003Ep__Site9a, args);
      string summonerName = func2((CallSite) callSite2, obj2);
      PublicSummoner summoner = await JsApiService.GetSummoner(realm, summonerName);
      ProfileService.JsLeague[] leagues = await this.GetLeagues(realm, summoner.SummonerId);
      foreach (ProfileService.JsLeague jsLeague in leagues)
        jsLeague.Divisions = (IEnumerable<ProfileService.JsLeagueDivision>) null;
      return (object) leagues;
    }

    [MicroApiMethod("getDivision")]
    public async Task<object> GetDivision(object args)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ProfileService.\u003C\u003Ec__DisplayClassae cDisplayClassae = new ProfileService.\u003C\u003Ec__DisplayClassae();
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea4 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea4;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea5.Target((CallSite) ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea5, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea6 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea6;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea7.Target((CallSite) ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea7, args);
      string summonerName = func2((CallSite) callSite2, obj2);
      // ISSUE: variable of a compiler-generated type
      ProfileService.\u003C\u003Ec__DisplayClassae cDisplayClassae1 = cDisplayClassae;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea8 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func3 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea8.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite3 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea8;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "id", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea9.Target((CallSite) ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Sitea9, args);
      string str1 = func3((CallSite) callSite3, obj3);
      // ISSUE: reference to a compiler-generated field
      cDisplayClassae1.id = str1;
      // ISSUE: variable of a compiler-generated type
      ProfileService.\u003C\u003Ec__DisplayClassae cDisplayClassae2 = cDisplayClassae;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Siteaa == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Siteaa = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func4 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Siteaa.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite4 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Siteaa;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Siteab == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Siteab = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "division", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Siteab.Target((CallSite) ProfileService.\u003CGetDivision\u003Eo__SiteContainera3.\u003C\u003Ep__Siteab, args);
      string str2 = func4((CallSite) callSite4, obj4);
      // ISSUE: reference to a compiler-generated field
      cDisplayClassae2.requestedDivision = str2;
      PublicSummoner summoner = await JsApiService.GetSummoner(realm, summonerName);
      ProfileService.JsLeague[] leagues = await this.GetLeagues(realm, summoner.SummonerId);
      // ISSUE: reference to a compiler-generated method
      ProfileService.JsLeague league = Enumerable.FirstOrDefault<ProfileService.JsLeague>((IEnumerable<ProfileService.JsLeague>) leagues, new Func<ProfileService.JsLeague, bool>(cDisplayClassae.\u003CGetDivision\u003Eb__ac));
      object obj5;
      if (league == null)
      {
        obj5 = (object) null;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        cDisplayClassae.requestedDivision = cDisplayClassae.requestedDivision ?? league.Division;
        // ISSUE: reference to a compiler-generated method
        obj5 = (object) Enumerable.FirstOrDefault<ProfileService.JsLeagueDivision>(league.Divisions, new Func<ProfileService.JsLeagueDivision, bool>(cDisplayClassae.\u003CGetDivision\u003Eb__ad));
      }
      return obj5;
    }

    [MicroApiMethod("getChampions")]
    public async Task<object> GetChampions(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Siteba == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Siteba = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Siteba.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Siteba;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebb == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebb = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebb.Target((CallSite) ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebb, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebc == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebc = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebc.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebc;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebd == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebd = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebd.Target((CallSite) ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebd, args);
      string summonerName = func2((CallSite) callSite2, obj2);
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebe == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebe = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func3 = ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebe.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite3 = ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebe;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebf == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebf = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "season", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebf.Target((CallSite) ProfileService.\u003CGetChampions\u003Eo__SiteContainerb9.\u003C\u003Ep__Sitebf, args);
      string season = func3((CallSite) callSite3, obj3);
      PublicSummoner summoner = await JsApiService.GetSummoner(realm, summonerName);
      return (object) await ProfileService.GetRankedChampions(realm, summoner.AcctId, "CLASSIC", ProfileService.GetCompetitiveSeasonFromJs(season));
    }

    [MicroApiMethod("runes.available")]
    public async Task<object> GetAvailableRunes(object args)
    {
      RiotAccount account = JsApiService.RiotAccount;
      long summonerId = account.SummonerId;
      SummonerRuneInventory response = await account.InvokeCachedAsync<SummonerRuneInventory>("summonerRuneService", "getSummonerRuneInventory", (object) summonerId);
      return (object) Enumerable.ToArray(Enumerable.Select((IEnumerable<SummonerRune>) response.SummonerRunes, x => new
      {
        Id = x.RuneId,
        Count = x.Quantity
      }));
    }

    [MicroApiMethod("runes.get")]
    public Task<object> GetRuneSetups(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited1;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited2.Target((CallSite) ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited2, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited3 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited3;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited4.Target((CallSite) ProfileService.\u003CGetRuneSetups\u003Eo__SiteContainerd0.\u003C\u003Ep__Sited4, args);
      string summonerName = func2((CallSite) callSite2, obj2);
      return InventoryHelper.GetRuneSetups(realm, summonerName);
    }

    [MicroApiMethod("runes.set")]
    public Task SetRuneSetups(JObject args)
    {
      return InventoryHelper.SetRuneSetups((string) args["realm"], (string) args["summonerName"], (JToken) args).ContinueWith((Action<Task>) (task => JsApiService.Push("riot:runes:updated", (object) null)));
    }

    [MicroApiMethod("masteries.get")]
    public Task<object> GetMasterySetups(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sited8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sited8 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sited8.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sited8;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sited9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sited9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sited9.Target((CallSite) ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sited9, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Siteda == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Siteda = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ProfileService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Siteda.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Siteda;
      // ISSUE: reference to a compiler-generated field
      if (ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sitedb == null)
      {
        // ISSUE: reference to a compiler-generated field
        ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sitedb = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (ProfileService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sitedb.Target((CallSite) ProfileService.\u003CGetMasterySetups\u003Eo__SiteContainerd7.\u003C\u003Ep__Sitedb, args);
      string summonerName = func2((CallSite) callSite2, obj2);
      return InventoryHelper.GetMasterySetups(realm, summonerName);
    }

    [MicroApiMethod("masteries.set")]
    public Task SetMasterySetups(JObject obj)
    {
      return InventoryHelper.SetMasterySetups(obj).ContinueWith((Action<Task>) (task => JsApiService.Push("riot:masteries:updated", (object) null)));
    }

    private static string GetCompetitiveSeasonFromJs(string js)
    {
      js = js ?? string.Empty;
      switch (js.ToLowerInvariant())
      {
        case "one":
          return "ONE";
        case "two":
          return "TWO";
        default:
          return "CURRENT";
      }
    }

    [Serializable]
    private class JsLeague
    {
      public static readonly ProfileService.JsLeague Unranked = new ProfileService.JsLeague()
      {
        League = "Unranked"
      };
      public string Id;
      public string LeagueName;
      public string Name;
      public string Queue;
      public string League;
      public string Division;
      public IEnumerable<ProfileService.JsLeagueDivision> Divisions;
      public int Points;
      public int Wins;
      public int Losses;
    }

    [Serializable]
    private class JsLeagueDivision
    {
      public string LeagueName;
      public string League;
      public string Division;
      public IEnumerable<object> Members;
    }
  }
}
