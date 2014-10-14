// Decompiled with JetBrains decompiler
// Type: WintermintClient.Riot.ReleasePackage
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace WintermintClient.Riot
{
  public struct ReleasePackage
  {
    public readonly int Version;

    public string String
    {
      get
      {
        return string.Format("{0}.{1}.{2}.{3}", (object) (this.Version >> 24 & (int) byte.MaxValue), (object) (this.Version >> 16 & (int) byte.MaxValue), (object) (this.Version >> 8 & (int) byte.MaxValue), (object) (this.Version & (int) byte.MaxValue));
      }
    }

    public ReleasePackage(int version)
    {
      this = new ReleasePackage();
      this.Version = version;
    }

    public ReleasePackage(string s)
    {
      this = new ReleasePackage();
      string[] strArray = s.Split('.');
      if (strArray.Length != 4)
        throw new ArgumentOutOfRangeException();
      this.Version = Enumerable.Aggregate<string, int>((IEnumerable<string>) strArray, 0, (Func<int, string, int>) ((v, b) => v << 8 | (int) byte.Parse(b)));
    }

    public static bool IsVersionString(string s)
    {
      try
      {
        ReleasePackage releasePackage = new ReleasePackage(s);
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
    }

    public override string ToString()
    {
      return this.String;
    }

    public bool Equals(ReleasePackage other)
    {
      return this.Version == other.Version;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj) || !(obj is ReleasePackage))
        return false;
      else
        return this.Equals((ReleasePackage) obj);
    }

    public override int GetHashCode()
    {
      return this.Version;
    }
  }
}
