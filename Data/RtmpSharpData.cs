// Decompiled with JetBrains decompiler
// Type: WintermintClient.Data.RtmpSharpData
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using RiotGames;
using RtmpSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WintermintClient.Data
{
  internal static class RtmpSharpData
  {
    public static Type[] SerializableTypes = Enumerable.ToArray<Type>(RiotDto.GetSerializableTypes());
    public static SerializationContext SerializationContext = new SerializationContext((IEnumerable<Type>) RtmpSharpData.SerializableTypes);
  }
}
