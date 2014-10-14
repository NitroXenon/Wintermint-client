// Decompiled with JetBrains decompiler
// Type: WintermintClient.Data.RiotEndpointData
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using FileDatabase;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WintermintClient.Data
{
  internal static class RiotEndpointData
  {
    private static JObject spectatorEndpoints;
    private static HttpClient http;

    public static async Task Initialize(IFileDb fileDb)
    {
      string json = await fileDb.GetStringAsync("data/game/runes.json");
      RiotEndpointData.spectatorEndpoints = JObject.Parse(json);
    }

    private static class Spectate
    {
      public static Task<string> GetVersion(string realmId)
      {
        return RiotEndpointData.Spectate.GetAsync(realmId, "version");
      }

      public static Task<string> GetMeta(string realmId, long gameId)
      {
        return RiotEndpointData.Spectate.GetAsync(realmId, "meta", (object) gameId);
      }

      private static string GetUri(string realmId, string type, params object[] args)
      {
        return string.Format((string) RiotEndpointData.spectatorEndpoints[realmId][(object) type], args);
      }

      private static Task<string> GetAsync(string realmId, string type, params object[] args)
      {
        string uri = RiotEndpointData.Spectate.GetUri(realmId, type, args);
        return RiotEndpointData.http.GetStringAsync(uri);
      }
    }
  }
}
