// Decompiled with JetBrains decompiler
// Type: WintermintClient.Instances
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using FileDatabase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WintermintClient.Daemons;
using WintermintClient.Data;
using WintermintClient.Riot;

namespace WintermintClient
{
  internal static class Instances
  {
    public static LittleClient Client = new LittleClient();
    public static RiotAccountBag AccountBag = new RiotAccountBag();
    public static WintermintUpdateDaemon WintermintUpdater = new WintermintUpdateDaemon();
    public static RiotUpdateDaemon RiotUpdater = new RiotUpdateDaemon();
    public static Dictionary<string, IFileDb> FileDatabases;
    public static IFileDb SupportFiles;
    public static IFileDb MediaFiles;
    public static IntPtr WindowHandle;

    public static async Task InitializeAsync(string[] fileDatabases)
    {
      LaunchData.Initialize();
      Instances.FileDatabases = new Dictionary<string, IFileDb>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (string path in fileDatabases)
        Instances.FileDatabases[path] = FileDbFactory.Open(path);
      Instances.SupportFiles = Instances.FileDatabases["support"];
      Instances.MediaFiles = Instances.FileDatabases["media"];
      Instances.WintermintUpdater.Initialize();
      Instances.RiotUpdater.Initialize();
      ChatStateTransformData.Initialize();
      await Task.WhenAll(GameData.Initialize(Instances.SupportFiles), ChampionNameData.Initialize(Instances.SupportFiles), PracticeGameData.Initialize(Instances.SupportFiles), RuneData.Initialize(Instances.SupportFiles), ReplayData.Initialize(Instances.SupportFiles));
    }
  }
}
