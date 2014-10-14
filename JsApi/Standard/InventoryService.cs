// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.InventoryService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using RiotGames.Platform.Catalog.Champion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WintermintClient.Data;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Helpers;
using WintermintClient.Riot;
using WintermintData.Storage;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("inventory")]
  public class InventoryService : JsApiService
  {
    private static ChampionGroup[] NoChampionGroups = new ChampionGroup[0];

    private async Task<InventoryService.JsChampion[]> GetAllChampions()
    {
      RiotAccount account = JsApiService.RiotAccount;
      InventoryService.JsChampion[] existing = JsApiService.Cache.Get<InventoryService.JsChampion[]>("riot:champions");
      long? accountHandle = JsApiService.Cache.Get<long?>("riot:champions:handle");
      InventoryService.JsChampion[] jsChampionArray;
      if (existing != null && accountHandle.HasValue)
      {
        long? nullable = accountHandle;
        long num = (long) account.Handle;
        if ((nullable.GetValueOrDefault() != num ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
        {
          jsChampionArray = existing;
          goto label_10;
        }
      }
      ChampionDTO[] champions = await account.InvokeAsync<ChampionDTO[]>("inventoryService", "getAvailableChampions");
      foreach (ChampionDTO championDto in champions)
      {
        championDto.ChampionSkins = Enumerable.ToList<ChampionSkinDTO>((IEnumerable<ChampionSkinDTO>) Enumerable.OrderBy<ChampionSkinDTO, double>((IEnumerable<ChampionSkinDTO>) championDto.ChampionSkins, (Func<ChampionSkinDTO, double>) (x => x.SkinId)));
        List<ChampionSkinDTO> list = championDto.ChampionSkins;
        ChampionSkinDTO championSkinDto = new ChampionSkinDTO()
        {
          ChampionId = (double) championDto.ChampionId,
          Owned = true,
          SkinId = list[0].SkinId - 1.0
        };
        list.Insert(0, championSkinDto);
        if (!Enumerable.Any<ChampionSkinDTO>((IEnumerable<ChampionSkinDTO>) list, (Func<ChampionSkinDTO, bool>) (x => x.LastSelected)))
          championSkinDto.LastSelected = true;
      }
      InventoryService.JsChampion[] jsChampions = Enumerable.ToArray<InventoryService.JsChampion>(Enumerable.Select<ChampionDTO, InventoryService.JsChampion>((IEnumerable<ChampionDTO>) champions, new Func<ChampionDTO, InventoryService.JsChampion>(InventoryService.ToJsChampion)));
      JsApiService.Cache.Set("riot:champions", (object) jsChampions);
      JsApiService.Cache.Set("riot:champions:handle", (object) new long?((long) account.Handle));
      jsChampionArray = jsChampions;
label_10:
      return jsChampionArray;
    }

    [MicroApiMethod("getChampionGroups")]
    public async Task<object> GetChampionsGroups()
    {
      Task<ChampionGroupPreferences> championGroupsTask = JsApiService.Client.Invoke<ChampionGroupPreferences>("storage.get", (object) "champion-groups");
      Task<InventoryService.JsChampion[]> allChampionsTask = this.GetAllChampions();
      ChampionGroup[] localChampionGroups = this.GetLocalChampionGroups();
      InventoryService.JsChampion[] allChampions = await allChampionsTask;
      ChampionGroupPreferences remoteChampionGroups = await championGroupsTask;
      IEnumerable<ChampionGroup> allChampionGroups = Enumerable.Concat<ChampionGroup>((IEnumerable<ChampionGroup>) remoteChampionGroups.Groups, (IEnumerable<ChampionGroup>) localChampionGroups);
      return (object) Enumerable.ToArray<InventoryService.JsChampionGroup>(Enumerable.Concat<InventoryService.JsChampionGroup>(Enumerable.Select<ChampionGroup, InventoryService.JsChampionGroup>(allChampionGroups, (Func<ChampionGroup, InventoryService.JsChampionGroup>) (group => new InventoryService.JsChampionGroup()
      {
        Name = group.Name,
        Champions = Enumerable.ToArray<InventoryService.JsChampion>(Enumerable.Where<InventoryService.JsChampion>((IEnumerable<InventoryService.JsChampion>) allChampions, (Func<InventoryService.JsChampion, bool>) (champion => Enumerable.Any<int>((IEnumerable<int>) group.Champions, (Func<int, bool>) (x => x == champion.Id)))))
      })), (IEnumerable<InventoryService.JsChampionGroup>) new InventoryService.JsChampionGroup[1]
      {
        new InventoryService.JsChampionGroup()
        {
          Name = "Available",
          Champions = allChampions
        }
      }));
    }

    private ChampionGroup[] GetLocalChampionGroups()
    {
      try
      {
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "wintermint", "experimental", "champion-groups"));
        if (!directoryInfo.Exists)
          return InventoryService.NoChampionGroups;
        else
          return Enumerable.ToArray<ChampionGroup>(Enumerable.Select<FileInfo, ChampionGroup>((IEnumerable<FileInfo>) directoryInfo.GetFiles("*.txt"), (Func<FileInfo, ChampionGroup>) (file =>
          {
            Dictionary<string, int> map = ChampionNameData.NameToId;
            IEnumerable<string> source = Enumerable.Select<string, string>((IEnumerable<string>) File.ReadAllLines(file.FullName, Encoding.UTF8), (Func<string, string>) (x => x.Trim()));
            return new ChampionGroup()
            {
              Name = Path.GetFileNameWithoutExtension(file.Name),
              Champions = Enumerable.ToArray<int>(Enumerable.Select<string, int>(Enumerable.Where<string>(source, new Func<string, bool>(map.ContainsKey)), (Func<string, int>) (x => map[x])))
            };
          })));
      }
      catch (Exception ex)
      {
        return InventoryService.NoChampionGroups;
      }
    }

    [MicroApiMethod("getChampions")]
    public async Task<object> GetChampions()
    {
      return (object) await this.GetAllChampions();
    }

    [MicroApiMethod("getRunes")]
    public Task<object> GetRunes()
    {
      RiotAccount active = JsApiService.AccountBag.Active;
      return InventoryHelper.GetRuneSetups(active.RealmId, (double) active.SummonerId);
    }

    [MicroApiMethod("getMasteries")]
    public Task<object> GetMasteries()
    {
      RiotAccount active = JsApiService.AccountBag.Active;
      return InventoryHelper.GetMasterySetups(active.RealmId, (double) active.SummonerId);
    }

    private static InventoryService.JsChampion ToJsChampion(ChampionDTO champion)
    {
      return new InventoryService.JsChampion()
      {
        Id = champion.ChampionId,
        Owned = champion.Owned,
        Free = champion.FreeToPlay,
        Skins = Enumerable.ToArray<InventoryService.JsChampionSkin>(Enumerable.Select<ChampionSkinDTO, InventoryService.JsChampionSkin>((IEnumerable<ChampionSkinDTO>) champion.ChampionSkins, (Func<ChampionSkinDTO, InventoryService.JsChampionSkin>) (s => new InventoryService.JsChampionSkin()
        {
          Id = (int) s.SkinId,
          Owned = s.Owned
        })))
      };
    }

    [Serializable]
    private class JsChampion
    {
      public int Id;
      public bool Owned;
      public bool Free;
      public InventoryService.JsChampionSkin[] Skins;
    }

    [Serializable]
    private class JsChampionSkin
    {
      public int Id;
      public bool Owned;
    }

    [Serializable]
    private class JsChampionGroup
    {
      public string Name;
      public InventoryService.JsChampion[] Champions;
    }
  }
}
