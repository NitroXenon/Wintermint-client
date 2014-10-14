// Decompiled with JetBrains decompiler
// Type: WintermintClient.Data.Extensions.DataHelperExtensions
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WintermintClient.Data.Extensions
{
  internal static class DataHelperExtensions
  {
    public static T Deserialize<T>(this string json)
    {
      return JsonConvert.DeserializeObject<T>(json);
    }

    public static Dictionary<string, T> Desensitize<T>(this Dictionary<string, T> dictionary)
    {
      return new Dictionary<string, T>((IDictionary<string, T>) dictionary, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }
  }
}
