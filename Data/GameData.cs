// Decompiled with JetBrains decompiler
// Type: WintermintClient.Data.GameData
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using FileDatabase;
using System.Collections.Generic;
using System.Threading.Tasks;
using WintermintClient.Data.Extensions;

namespace WintermintClient.Data
{
  internal static class GameData
  {
    public static Dictionary<string, int[]> SummonerSpells;
    public static Dictionary<int, string> MapIdToName;

    public static async Task Initialize(IFileDb fileDb)
    {
      string json = await fileDb.GetStringAsync("data/game/spells/map-assignments.json");
      GameData.SummonerSpells = DataHelperExtensions.Desensitize<int[]>(DataHelperExtensions.Deserialize<Dictionary<string, int[]>>(json));
      json = await fileDb.GetStringAsync("data/game/maps.json");
      GameData.MapIdToName = DataHelperExtensions.Deserialize<Dictionary<int, string>>(json);
    }

    public static int[] GetAvailableSummonerSpells(string gameMode)
    {
      int[] numArray;
      if (!GameData.SummonerSpells.TryGetValue(gameMode.ToLowerInvariant(), out numArray))
        return new int[0];
      else
        return numArray;
    }

    public static string GetMapClassification(int mapId)
    {
      string str;
      if (!GameData.MapIdToName.TryGetValue(mapId, out str))
        return "unknown";
      else
        return str;
    }
  }
}
