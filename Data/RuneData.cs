// Decompiled with JetBrains decompiler
// Type: WintermintClient.Data.RuneData
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using FileDatabase;
using System.Collections.Generic;
using System.Threading.Tasks;
using WintermintClient.Data.Extensions;

namespace WintermintClient.Data
{
  internal static class RuneData
  {
    public static Dictionary<int, Rune> Runes { get; private set; }

    public static async Task Initialize(IFileDb fileDb)
    {
      string json = await fileDb.GetStringAsync("data/game/runes.json");
      RuneData.Runes = DataHelperExtensions.Deserialize<Dictionary<int, Rune>>(json);
    }
  }
}
