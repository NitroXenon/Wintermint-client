// Decompiled with JetBrains decompiler
// Type: WintermintClient.Data.PracticeGameData
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using FileDatabase;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WintermintClient.Data
{
  internal static class PracticeGameData
  {
    private static Regex[] DefaultGameTests;

    public static async Task Initialize(IFileDb fileDb)
    {
      string json = await fileDb.GetStringAsync("data/game/practice-names.json");
      PracticeGameData.DefaultGameTests = Enumerable.ToArray<Regex>(Enumerable.Select<string, Regex>(Enumerable.Distinct<string>(Enumerable.Select<JToken, string>((IEnumerable<JToken>) Extensions.Values((IEnumerable<JToken>) JObject.Parse(json)), (Func<JToken, string>) (x => (string) x))), (Func<string, Regex>) (x => new Regex(x, RegexOptions.IgnoreCase | RegexOptions.Compiled))));
    }

    public static bool IsDefaultGame(string gameName)
    {
      return Enumerable.Any<Regex>((IEnumerable<Regex>) PracticeGameData.DefaultGameTests, (Func<Regex, bool>) (x => x.IsMatch(gameName)));
    }
  }
}
