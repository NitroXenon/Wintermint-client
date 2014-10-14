// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Helpers.InventoryHelper
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Newtonsoft.Json.Linq;
using RiotGames.Platform.Summoner;
using RiotGames.Platform.Summoner.Masterybook;
using RiotGames.Platform.Summoner.Spellbook;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WintermintClient.Data;
using WintermintClient.JsApi;
using WintermintClient.Riot;
using WintermintData.Storage;

namespace WintermintClient.JsApi.Helpers
{
  internal class InventoryHelper : JsApiService
  {
    public static InventoryHelper Instance = new InventoryHelper();
    private const int RunesPerType = 9;
    private const int MaximumMasteryPages = 20;

    public static async Task<object> GetRuneSetups(string realm, string summonerName)
    {
      PublicSummoner summoner = await JsApiService.GetSummoner(realm, summonerName);
      return await InventoryHelper.GetRuneSetups(realm, (double) summoner.SummonerId);
    }

    public static async Task<object> GetRuneSetups(string realm, double summonerId)
    {
      RiotAccount[] accounts = JsApiService.AccountBag.GetAll();
      RiotAccount account = JsApiService.AccountBag.Get(realm);
      SpellBookDTO book = await account.InvokeCachedAsync<SpellBookDTO>("spellBookService", "getSpellBook", (object) summonerId);
      SpellBookPageDTO activePage = Enumerable.FirstOrDefault<SpellBookPageDTO>((IEnumerable<SpellBookPageDTO>) book.BookPages, (Func<SpellBookPageDTO, bool>) (x => x.Current)) ?? new SpellBookPageDTO();
      return (object) new
      {
        Local = Enumerable.Any<RiotAccount>((IEnumerable<RiotAccount>) accounts, (Func<RiotAccount, bool>) (x =>
        {
          if (string.Equals(x.RealmId, realm, StringComparison.OrdinalIgnoreCase))
            return (double) x.SummonerId == summonerId;
          else
            return false;
        })),
        ActiveId = activePage.PageId,
        Setups = Enumerable.Select((IEnumerable<SpellBookPageDTO>) Enumerable.OrderBy<SpellBookPageDTO, double>((IEnumerable<SpellBookPageDTO>) book.BookPages, (Func<SpellBookPageDTO, double>) (page => page.PageId)), page => new
        {
          Id = page.PageId,
          Name = page.Name,
          Runes = Enumerable.Select(Enumerable.GroupBy<SlotEntry, int>((IEnumerable<SlotEntry>) page.SlotEntries, (Func<SlotEntry, int>) (entry => entry.RuneId)), entries => new
          {
            Id = entries.Key,
            Count = Enumerable.Count<SlotEntry>((IEnumerable<SlotEntry>) entries)
          })
        })
      };
    }

    public static async Task SetRuneSetups(string realm, string summonerName, JToken obj)
    {
      RiotAccount account = InventoryHelper.GetAccount(realm, summonerName);
      string activeId = (string) obj[(object) "activeId"];
      List<SpellBookPageDTO> pages = Enumerable.ToList<SpellBookPageDTO>(Enumerable.Select<InventoryHelper.RuneSetup, SpellBookPageDTO>(Enumerable.Select<JToken, InventoryHelper.RuneSetup>((IEnumerable<JToken>) obj[(object) "setups"], new Func<JToken, InventoryHelper.RuneSetup>(InventoryHelper.ToRuneSetup)), new Func<InventoryHelper.RuneSetup, SpellBookPageDTO>(InventoryHelper.ToSpellBookPage)));
      double[] pageIds = Enumerable.ToArray<double>((IEnumerable<double>) Enumerable.OrderBy<double, double>(Enumerable.Select<SpellBookPageDTO, double>((IEnumerable<SpellBookPageDTO>) pages, (Func<SpellBookPageDTO, double>) (x => x.PageId)), (Func<double, double>) (x => x)));
      for (int index = 0; index < pageIds.Length; ++index)
        pages[index].PageId = pageIds[index];
      Dictionary<string, string[]> duplicatesLookup = Enumerable.ToDictionary<IGrouping<string, string>, string, string[]>((IEnumerable<IGrouping<string, string>>) Enumerable.ToLookup<string, string>(Enumerable.Select<SpellBookPageDTO, string>((IEnumerable<SpellBookPageDTO>) pages, (Func<SpellBookPageDTO, string>) (x => x.Name)), (Func<string, string>) (x => x)), (Func<IGrouping<string, string>, string>) (x => x.Key), (Func<IGrouping<string, string>, string[]>) (x => Enumerable.ToArray<string>((IEnumerable<string>) x)));
      for (int index = 0; index < pages.Count; ++index)
      {
        SpellBookPageDTO spellBookPageDto = pages[index];
        spellBookPageDto.SummonerId = (double) account.SummonerId;
        if (string.IsNullOrEmpty(spellBookPageDto.Name) || duplicatesLookup[spellBookPageDto.Name].Length > 1)
          spellBookPageDto.Name = string.Format("#{0:00}. {1}", (object) index, (object) spellBookPageDto.Name);
        if (spellBookPageDto.PageId.ToString((IFormatProvider) CultureInfo.InvariantCulture) == activeId)
          spellBookPageDto.Current = true;
      }
      if (pages.Count > 0 && Enumerable.All<SpellBookPageDTO>((IEnumerable<SpellBookPageDTO>) pages, (Func<SpellBookPageDTO, bool>) (x => !x.Current)))
        pages[0].Current = true;
      RiotAccount riotAccount = account;
      string destination = "spellBookService";
      string method = "saveSpellBook";
      SpellBookDTO spellBookDto = new SpellBookDTO();
      spellBookDto.SummonerId = (double) account.SummonerId;
      spellBookDto.BookPages = pages;
      SpellBookDTO spellBookDto1 = spellBookDto;
      object obj1 = await riotAccount.InvokeAsync<object>(destination, method, (object) spellBookDto1);
      InventoryHelper.ClearRuneCache(account);
    }

    public static async Task SetActiveRuneSetup(string realm, string summonerName, JObject jSetup)
    {
      InventoryHelper.RuneSetup setup = InventoryHelper.ToRuneSetup((JToken) jSetup);
      SpellBookPageDTO page = InventoryHelper.ToSpellBookPage(setup);
      RiotAccount account = InventoryHelper.GetAccount(realm, summonerName);
      object obj = await account.InvokeAsync<object>("spellBookService", "selectDefaultSpellBookPage", (object) page);
      InventoryHelper.ClearRuneCache(account);
    }

    private static RuneType GetRuneType(int id)
    {
      WintermintClient.Data.Rune rune;
      if (RuneData.Runes.TryGetValue(id, out rune))
        return rune.Type;
      else
        return RuneType.Red;
    }

    private static InventoryHelper.RuneSetup ToRuneSetup(JToken obj)
    {
      return obj.ToObject<InventoryHelper.RuneSetup>();
    }

    private static SpellBookPageDTO ToSpellBookPage(InventoryHelper.RuneSetup setup)
    {
      List<SlotEntry> list = new List<SlotEntry>();
      foreach (IGrouping<RuneType, InventoryHelper.Rune> grouping in Enumerable.ToArray<IGrouping<RuneType, InventoryHelper.Rune>>((IEnumerable<IGrouping<RuneType, InventoryHelper.Rune>>) Enumerable.OrderBy<IGrouping<RuneType, InventoryHelper.Rune>, RuneType>(Enumerable.GroupBy<InventoryHelper.Rune, RuneType>((IEnumerable<InventoryHelper.Rune>) setup.Runes, (Func<InventoryHelper.Rune, RuneType>) (x => InventoryHelper.GetRuneType(x.Id))), (Func<IGrouping<RuneType, InventoryHelper.Rune>, RuneType>) (x => x.Key))))
      {
        RuneType key = grouping.Key;
        InventoryHelper.Rune[] runeArray = Enumerable.ToArray<InventoryHelper.Rune>((IEnumerable<InventoryHelper.Rune>) grouping);
        int num = (int) key * 9 + 1;
        for (int index1 = 0; index1 < runeArray.Length; ++index1)
        {
          InventoryHelper.Rune rune = runeArray[index1];
          for (int index2 = 0; index2 < rune.Count; ++index2)
          {
            list.Add(new SlotEntry()
            {
              RuneId = rune.Id,
              RuneSlotId = num
            });
            ++num;
          }
        }
      }
      SpellBookPageDTO spellBookPageDto = new SpellBookPageDTO();
      spellBookPageDto.PageId = setup.Id;
      spellBookPageDto.Name = setup.Name;
      spellBookPageDto.SlotEntries = Enumerable.ToList<SlotEntry>((IEnumerable<SlotEntry>) list);
      spellBookPageDto.Current = true;
      return spellBookPageDto;
    }

    private static void ClearRuneCache(RiotAccount account)
    {
      InventoryHelper.ClearRuneCache(account, account.SummonerId);
    }

    private static void ClearRuneCache(RiotAccount account, long summonerId)
    {
      account.RemoveCached<SpellBookDTO>("spellBookService", "getSpellBook", (object) summonerId);
    }

    public static async Task<object> GetMasterySetups(string realm, string summonerName)
    {
      PublicSummoner summoner = await JsApiService.GetSummoner(realm, summonerName);
      return await InventoryHelper.GetMasterySetups(realm, (double) summoner.SummonerId);
    }

    public static async Task<object> GetMasterySetups(string realm, double summonerId)
    {
      RiotAccount[] accounts = JsApiService.AccountBag.GetAll();
      bool isLocal = Enumerable.Any<RiotAccount>((IEnumerable<RiotAccount>) accounts, (Func<RiotAccount, bool>) (x =>
      {
        if (string.Equals(x.RealmId, realm, StringComparison.OrdinalIgnoreCase))
          return (double) x.SummonerId == summonerId;
        else
          return false;
      }));
      object obj;
      if (isLocal)
      {
        MasteryBook book = await JsApiService.Client.InvokeCached<MasteryBook>("storage.get", new object[1]
        {
          (object) "masteries"
        });
        obj = (object) new
        {
          Local = true,
          ActiveId = book.ActiveId,
          Setups = Enumerable.Select((IEnumerable<MasterySetup>) book.Setups, setup => new
          {
            Id = setup.Id,
            Name = setup.Name,
            Masteries = Enumerable.Select((IEnumerable<Mastery>) setup.Masteries, mastery => new
            {
              Id = mastery.Id,
              Points = mastery.Rank
            })
          })
        };
      }
      else
      {
        RiotAccount account = JsApiService.AccountBag.Get(realm);
        MasteryBookDTO riotBook = await account.InvokeCachedAsync<MasteryBookDTO>("masteryBookService", "getMasteryBook", (object) summonerId);
        List<MasteryBookPageDTO> riotBookPages = riotBook.BookPages;
        MasteryBookPageDTO activePage = Enumerable.FirstOrDefault<MasteryBookPageDTO>((IEnumerable<MasteryBookPageDTO>) riotBookPages, (Func<MasteryBookPageDTO, bool>) (x => x.Current)) ?? new MasteryBookPageDTO();
        obj = (object) new
        {
          Local = false,
          ActiveId = activePage.PageId.ToString((IFormatProvider) CultureInfo.InvariantCulture),
          Setups = Enumerable.Select((IEnumerable<MasteryBookPageDTO>) Enumerable.OrderBy<MasteryBookPageDTO, double>((IEnumerable<MasteryBookPageDTO>) riotBook.BookPages, (Func<MasteryBookPageDTO, double>) (page => page.PageId)), page => new
          {
            Id = page.PageId.ToString((IFormatProvider) CultureInfo.InvariantCulture),
            Name = page.Name,
            Masteries = Enumerable.Select((IEnumerable<TalentEntry>) page.TalentEntries, entry => new
            {
              Id = entry.TalentId,
              Points = entry.Rank
            })
          })
        };
      }
      return obj;
    }

    public static async Task SetMasterySetups(JObject jObject)
    {
      string activeId = jObject["activeId"].ToObject<string>();
      MasterySetup[] setups = Enumerable.ToArray<MasterySetup>(Enumerable.Select<JToken, MasterySetup>((IEnumerable<JToken>) jObject["setups"], new Func<JToken, MasterySetup>(InventoryHelper.ToMasterySetup)));
      MasteryBook book = new MasteryBook()
      {
        ActiveId = activeId,
        Setups = setups
      };
      await JsApiService.Client.Invoke("storage.set", new object[2]
      {
        (object) "masteries",
        (object) book
      });
      foreach (RiotAccount account in JsApiService.AccountBag.GetAll())
        InventoryHelper.ClearMasteryCache(account);
      InventoryHelper.SaveRiotMasteryBook(book);
    }

    private static void SaveRiotMasteryBook(MasteryBook book)
    {
      MasterySetup[] masterySetupArray = Enumerable.ToArray<MasterySetup>(Enumerable.Take<MasterySetup>((IEnumerable<MasterySetup>) (book.Setups ?? new MasterySetup[0]), 20));
      MasteryBookPageDTO[] masteryBookPageDtoArray = Enumerable.ToArray<MasteryBookPageDTO>(Enumerable.Select<MasterySetup, MasteryBookPageDTO>((IEnumerable<MasterySetup>) masterySetupArray, new Func<MasterySetup, MasteryBookPageDTO>(InventoryHelper.ToMasteryBookPage)));
      Dictionary<string, string[]> dictionary = Enumerable.ToDictionary<IGrouping<string, string>, string, string[]>((IEnumerable<IGrouping<string, string>>) Enumerable.ToLookup<string, string>(Enumerable.Select<MasteryBookPageDTO, string>((IEnumerable<MasteryBookPageDTO>) masteryBookPageDtoArray, (Func<MasteryBookPageDTO, string>) (x => x.Name)), (Func<string, string>) (x => x)), (Func<IGrouping<string, string>, string>) (x => x.Key), (Func<IGrouping<string, string>, string[]>) (x => Enumerable.ToArray<string>((IEnumerable<string>) x)));
      for (int index = 0; index < masteryBookPageDtoArray.Length; ++index)
      {
        MasteryBookPageDTO masteryBookPageDto = masteryBookPageDtoArray[index];
        masteryBookPageDto.PageId = (double) (index + 1);
        if (string.IsNullOrEmpty(masteryBookPageDto.Name) || dictionary[masteryBookPageDto.Name].Length > 1)
          masteryBookPageDto.Name = string.Format("#{0:00}. {1}", (object) index, (object) masteryBookPageDto.Name);
        if (Enumerable.All<MasteryBookPageDTO>((IEnumerable<MasteryBookPageDTO>) masteryBookPageDtoArray, (Func<MasteryBookPageDTO, bool>) (x => !x.Current)) && masterySetupArray[index].Id == book.ActiveId)
          masteryBookPageDto.Current = true;
      }
      if (masteryBookPageDtoArray.Length > 0 && Enumerable.All<MasteryBookPageDTO>((IEnumerable<MasteryBookPageDTO>) masteryBookPageDtoArray, (Func<MasteryBookPageDTO, bool>) (x => !x.Current)))
        masteryBookPageDtoArray[0].Current = true;
      foreach (RiotAccount account in JsApiService.AccountBag.GetAll())
      {
        RiotAccount riotAccount = account;
        string destination = "masteryBookService";
        string method = "saveMasteryBook";
        MasteryBookDTO masteryBookDto1 = new MasteryBookDTO();
        masteryBookDto1.SummonerId = (double) account.SummonerId;
        masteryBookDto1.BookPages = Enumerable.ToList<MasteryBookPageDTO>((IEnumerable<MasteryBookPageDTO>) masteryBookPageDtoArray);
        MasteryBookDTO masteryBookDto2 = masteryBookDto1;
        riotAccount.InvokeAsync<object>(destination, method, (object) masteryBookDto2);
        InventoryHelper.ClearMasteryCache(account);
      }
    }

    public static Task SetActiveMasterySetup(JObject jObject)
    {
      return InventoryHelper.SetActiveMasterySetup(InventoryHelper.ToMasterySetup((JToken) jObject));
    }

    public static async Task SetActiveMasterySetup(MasterySetup setup)
    {
      JsApiService.Client.Invoke("storage.masteries.active.set", (object) setup.Id);
      MasteryBook book = await JsApiService.Client.InvokeCached<MasteryBook>("storage.get", new object[1]
      {
        (object) "masteries"
      });
      IEnumerable<MasterySetup> normalSetups = Enumerable.Take<MasterySetup>((IEnumerable<MasterySetup>) book.Setups, 19);
      MasterySetup[] activateSetups = new MasterySetup[1]
      {
        setup
      };
      MasterySetup[] setups = Enumerable.ToArray<MasterySetup>(Enumerable.Concat<MasterySetup>(normalSetups, (IEnumerable<MasterySetup>) activateSetups));
      for (int index = 0; index < setups.Length; ++index)
        setups[index].Id = index.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      setup.Name = "[active]";
      book.Setups = setups;
      book.ActiveId = setup.Id;
      InventoryHelper.SaveRiotMasteryBook(book);
      foreach (RiotAccount account in JsApiService.AccountBag.GetAll())
        InventoryHelper.ClearMasteryCache(account);
    }

    private static MasterySetup ToMasterySetup(JToken setup)
    {
      foreach (JToken jtoken in Enumerable.AsEnumerable<JToken>((IEnumerable<JToken>) setup[(object) "masteries"]))
        jtoken[(object) "rank"] = jtoken[(object) "points"];
      return setup.ToObject<MasterySetup>();
    }

    private static MasteryBookPageDTO ToMasteryBookPage(MasterySetup setup)
    {
      IEnumerable<TalentEntry> source = Enumerable.Select<Mastery, TalentEntry>((IEnumerable<Mastery>) setup.Masteries, (Func<Mastery, TalentEntry>) (x => new TalentEntry()
      {
        TalentId = x.Id,
        Rank = x.Rank
      }));
      MasteryBookPageDTO masteryBookPageDto = new MasteryBookPageDTO();
      masteryBookPageDto.Name = setup.Name;
      masteryBookPageDto.TalentEntries = Enumerable.ToList<TalentEntry>(source);
      masteryBookPageDto.Current = false;
      return masteryBookPageDto;
    }

    private static void ClearMasteryCache(RiotAccount account)
    {
      InventoryHelper.ClearMasteryCache(account, account.SummonerId);
    }

    private static void ClearMasteryCache(RiotAccount account, long summonerId)
    {
      account.RemoveCached<MasteryBookDTO>("masteryBookService", "getMasteryBook", (object) summonerId);
      JsApiService.Client.Purge<MasteryBook>("storage.get", new object[1]
      {
        (object) "masteries"
      });
    }

    private static RiotAccount GetAccount(string realm, string summonerName)
    {
      return Enumerable.First<RiotAccount>((IEnumerable<RiotAccount>) JsApiService.AccountBag.GetAll(), (Func<RiotAccount, bool>) (x =>
      {
        if (string.Equals(x.RealmId, realm, StringComparison.OrdinalIgnoreCase))
          return string.Equals(x.SummonerName, summonerName, StringComparison.OrdinalIgnoreCase);
        else
          return false;
      }));
    }

    public class RuneSetup
    {
      public double Id;
      public string Name;
      public InventoryHelper.Rune[] Runes;
    }

    public class Rune
    {
      public int Id;
      public int Count;
    }
  }
}
