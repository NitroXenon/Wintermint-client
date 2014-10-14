// Decompiled with JetBrains decompiler
// Type: WintermintClient.Data.ReplayData
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using FileDatabase;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WintermintClient.Data.Extensions;
using WintermintData.Matches;

namespace WintermintClient.Data
{
  public class ReplayData
  {
    private static Dictionary<string, JObject> spectator;

    public static async Task Initialize(IFileDb fileDb)
    {
      string json = await fileDb.GetStringAsync("riot/endpoints/spectator.json");
      ReplayData.spectator = DataHelperExtensions.Desensitize<JObject>(DataHelperExtensions.Deserialize<Dictionary<string, JObject>>(json));
    }

    public string GetSpectatorUri(string realmId, string type, params object[] args)
    {
      return string.Format((string) ReplayData.spectator[realmId][type], args);
    }

    public async Task<Match> GetSpectatorMatchStats(string realmId, long matchId)
    {
      await Task.Delay(1000);
      return new Match();
    }
  }
}
