// Decompiled with JetBrains decompiler
// Type: WintermintClient.Data.ChampionNameData
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using FileDatabase;
using System.Collections.Generic;
using System.Threading.Tasks;
using WintermintClient.Data.Extensions;

namespace WintermintClient.Data
{
  internal static class ChampionNameData
  {
    public static Dictionary<string, int> NameToId;
    public static Dictionary<int, string> LegacyIdToClientName;

    public static async Task Initialize(IFileDb fileDb)
    {
      string json = await fileDb.GetStringAsync("data/game/champions/mappings/name-to-id.json");
      ChampionNameData.NameToId = DataHelperExtensions.Desensitize<int>(DataHelperExtensions.Deserialize<Dictionary<string, int>>(json));
      json = await fileDb.GetStringAsync("data/game/champions/mappings/legacy/id-to-client-name.json");
      ChampionNameData.LegacyIdToClientName = DataHelperExtensions.Deserialize<Dictionary<int, string>>(json);
    }

    public static int GetChampionId(string key)
    {
      int num;
      if (key == null || !ChampionNameData.NameToId.TryGetValue(key, out num))
        return 0;
      else
        return num;
    }

    public static string GetLegacyChampionClientNameOrSoraka(int championId)
    {
      string str;
      if (!ChampionNameData.LegacyIdToClientName.TryGetValue(championId, out str))
        return "Soraka";
      else
        return str;
    }
  }
}
