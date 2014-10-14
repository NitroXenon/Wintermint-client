// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.JsApiService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Chat;
using FileDatabase;
using RiotGames.Platform.Summoner;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WintermintClient;
using WintermintClient.Riot;

namespace WintermintClient.JsApi
{
  public abstract class JsApiService
  {
    private static Regex IntegerRegex = new Regex("\\d+", RegexOptions.Compiled);
    internal static JsApiService.JsResponse NullResponse = (JsApiService.JsResponse) (_ => {});
    internal static LittleClient Client = Instances.Client;
    internal static CacheHelper Cache = new CacheHelper();
    internal static JsApiService.JsPush Push;
    internal static JsApiService.JsPushJson PushJson;

    internal static IFileDb SupportFiles
    {
      get
      {
        return Instances.SupportFiles;
      }
    }

    internal static IFileDb MediaFiles
    {
      get
      {
        return Instances.MediaFiles;
      }
    }

    internal static RiotAccountBag AccountBag
    {
      get
      {
        return Instances.AccountBag;
      }
    }

    internal static RiotAccount RiotAccount
    {
      get
      {
        return Instances.AccountBag.Active;
      }
    }

    protected static Action<Task<T>> CreatePublisher<T>(ConcurrentDictionary<string, object> dictionary, JsApiService.JsResponse updateFunction, string dictionaryKey)
    {
      return (Action<Task<T>>) (task =>
      {
        if (task.Status != TaskStatus.RanToCompletion)
          return;
        dictionary[dictionaryKey] = (object) task.Result;
        updateFunction((object) dictionary);
      });
    }

    protected static string GetSummonerJidFromId(long summonerId)
    {
      return string.Format("sum{0}@pvp.net", (object) summonerId);
    }

    protected static long GetSummonerIdFromJid(string jid)
    {
      string user = new JabberId(jid).User;
      return long.Parse(JsApiService.IntegerRegex.Match(user).Value);
    }

    protected static Task<string> GetSummonerNameByJid(string realm, string jid)
    {
      long summonerIdFromJid = JsApiService.GetSummonerIdFromJid(jid);
      return JsApiService.GetSummonerNameBySummonerId(realm, summonerIdFromJid);
    }

    protected static async Task<string> GetSummonerNameBySummonerId(string realmId, long summonerId)
    {
      RiotAccount account = JsApiService.AccountBag.Get(realmId);
      string[] names = await account.InvokeCachedAsync<string[]>("summonerService", "getSummonerNames", (object) new long[1]
      {
        summonerId
      });
      return Enumerable.First<string>((IEnumerable<string>) names);
    }

    protected static void PushIfActive(RiotAccount account, string key, object obj)
    {
      if (JsApiService.AccountBag.Active != account)
        return;
      JsApiService.Push(key, obj);
    }

    protected static double GetGameStat(Dictionary<string, double> dictionary, string key)
    {
      double num;
      if (!dictionary.TryGetValue(key, out num))
        return 0.0;
      else
        return num;
    }

    protected static async Task<PublicSummoner> GetSummoner(string realmId, string summonerName)
    {
      RiotAccount account = JsApiService.AccountBag.Get(realmId);
      PublicSummoner response = await account.InvokeCachedAsync<PublicSummoner>("summonerService", "getSummonerByName", (object) summonerName);
      if (response == null)
        account.RemoveCached<PublicSummoner>("summonerService", "getSummonerByName", (object) summonerName);
      return response;
    }

    protected static bool IsGameStateExitable(string gameState)
    {
      switch (gameState)
      {
        case "IN_PROGRESS":
        case "START_REQUESTED":
          return false;
        default:
          return true;
      }
    }

    public delegate void JsPush(string key, object obj);

    public delegate void JsPushJson(string key, string json);

    public delegate void JsResponse(object obj);
  }
}
