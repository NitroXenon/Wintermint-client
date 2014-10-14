// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.AccountSetupService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using RiotGames.Platform.Summoner.Masterybook;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintClient.Riot;
using WintermintData.Storage;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("accountSetup")]
  public class AccountSetupService : JsApiService
  {
    [MicroApiMethod("setup")]
    public async Task<AccountSetupService.SetupSummary> Setup(object args, JsApiService.JsResponse progress, JsApiService.JsResponse result)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      AccountSetupService.\u003C\u003Ec__DisplayClasse cDisplayClasse = new AccountSetupService.\u003C\u003Ec__DisplayClasse();
      // ISSUE: reference to a compiler-generated field
      cDisplayClasse.progress = progress;
      // ISSUE: reference to a compiler-generated field
      if (AccountSetupService.\u003CSetup\u003Eo__SiteContainer5.\u003C\u003Ep__Site6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountSetupService.\u003CSetup\u003Eo__SiteContainer5.\u003C\u003Ep__Site6 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (AccountSetupService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func = AccountSetupService.\u003CSetup\u003Eo__SiteContainer5.\u003C\u003Ep__Site6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite = AccountSetupService.\u003CSetup\u003Eo__SiteContainer5.\u003C\u003Ep__Site6;
      // ISSUE: reference to a compiler-generated field
      if (AccountSetupService.\u003CSetup\u003Eo__SiteContainer5.\u003C\u003Ep__Site7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountSetupService.\u003CSetup\u003Eo__SiteContainer5.\u003C\u003Ep__Site7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "handle", typeof (AccountSetupService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AccountSetupService.\u003CSetup\u003Eo__SiteContainer5.\u003C\u003Ep__Site7.Target((CallSite) AccountSetupService.\u003CSetup\u003Eo__SiteContainer5.\u003C\u003Ep__Site7, args);
      int handle = func((CallSite) callSite, obj);
      RiotAccount account = JsApiService.AccountBag.Get(handle);
      // ISSUE: reference to a compiler-generated field
      cDisplayClasse.items = new Dictionary<string, Task>()
      {
        {
          "runes",
          (Task) Task.FromResult<bool>(true)
        },
        {
          "masteries",
          this.ImportMasteries(account)
        },
        {
          "key-bindings",
          this.ImportKeyBindings()
        },
        {
          "game-settings",
          this.ImportGameSettings()
        }
      };
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      cDisplayClasse.getSetupSummary = new Func<AccountSetupService.SetupSummary>(cDisplayClasse.\u003CSetup\u003Eb__8);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      cDisplayClasse.publishStatus = new Action<Task>(cDisplayClasse.\u003CSetup\u003Eb__c);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      IEnumerable<Task> async = Enumerable.Select<Task, Task>((IEnumerable<Task>) cDisplayClasse.items.Values, new Func<Task, Task>(cDisplayClasse.\u003CSetup\u003Eb__d));
      try
      {
        await Task.WhenAll(async);
      }
      catch
      {
      }
      await JsApiService.Client.Invoke("mess.flags.firstRun.set");
      // ISSUE: reference to a compiler-generated field
      return cDisplayClasse.getSetupSummary();
    }

    private async Task ImportMasteries(RiotAccount account)
    {
      Func<MasteryBookPageDTO, string> getId = (Func<MasteryBookPageDTO, string>) (page => page.PageId.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      MasteryBookDTO book = await account.InvokeAsync<MasteryBookDTO>("masteryBookService", "getMasteryBook", (object) account.SummonerId);
      IEnumerable<MasterySetup> pages = Enumerable.Select(Enumerable.Select((IEnumerable<MasteryBookPageDTO>) Enumerable.OrderBy<MasteryBookPageDTO, double>((IEnumerable<MasteryBookPageDTO>) book.BookPages, (Func<MasteryBookPageDTO, double>) (page => page.PageId)), page => new
      {
        page = page,
        masteries = Enumerable.Select<TalentEntry, Mastery>((IEnumerable<TalentEntry>) page.TalentEntries, (Func<TalentEntry, Mastery>) (x => new Mastery()
        {
          Id = x.TalentId,
          Rank = x.Rank
        }))
      }), param0 => new MasterySetup()
      {
        Id = getId(param0.page),
        Name = param0.page.Name,
        Masteries = Enumerable.ToArray<Mastery>(param0.masteries)
      });
      MasteryBookPageDTO activePage = Enumerable.FirstOrDefault<MasteryBookPageDTO>((IEnumerable<MasteryBookPageDTO>) book.BookPages, (Func<MasteryBookPageDTO, bool>) (x => x.Current));
      string activeId = activePage != null ? getId(activePage) : (string) null;
      await JsApiService.Client.Invoke("storage.set", new object[2]
      {
        (object) "masteries",
        (object) new MasteryBook()
        {
          ActiveId = activeId,
          Setups = Enumerable.ToArray<MasterySetup>(pages)
        }
      });
    }

    private async Task ImportKeyBindings()
    {
      await Task.Delay(2000);
      throw new NotImplementedException();
    }

    private async Task ImportGameSettings()
    {
      await Task.Delay(5000);
      throw new NotImplementedException();
    }

    public class SetupSummary
    {
      public AccountSetupService.SetupTask[] Tasks;
    }

    public class SetupTask
    {
      public string Name;
      public bool Completed;
      public bool Faulted;
    }
  }
}
