// Decompiled with JetBrains decompiler
// Type: WintermintClient.GlobalExtensions
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

namespace WintermintClient
{
  public static class GlobalExtensions
  {
    public static string ToProperCase(this string str)
    {
      if (string.IsNullOrEmpty(str))
        return str;
      else
        return (string) (object) char.ToUpperInvariant(str[0]) + (object) str.Substring(1).ToLower();
    }
  }
}
